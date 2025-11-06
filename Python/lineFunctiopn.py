import os
import json
import base64
import uuid
import requests
import boto3
import re
import time
from openai import OpenAI
from datetime import datetime, timezone
from decimal import Decimal

LINE_REPLY_URL = "https://api.line.me/v2/bot/message/reply"
LINE_ACCESS_TOKEN = os.getenv("LINE_CHANNEL_ACCESS_TOKEN")
OPENAI_API_KEY = os.getenv("OPENAI_API_KEY")
BUCKET_NAME = os.getenv("BUCKET_NAME")  # ← Lambda環境変数で設定

client = OpenAI(api_key=OPENAI_API_KEY)
s3 = boto3.client("s3")
dynamodb = boto3.resource("dynamodb")
table = dynamodb.Table("LineGPS_New")

def lambda_handler(event, context):
    try:
        body = json.loads(event["body"])
        print("LINE Event:", json.dumps(body, ensure_ascii=False))

        for e in body["events"]:
            reply_token = e["replyToken"]
            msg_type = e["message"]["type"]

            if msg_type == "location":
                lat = e["message"]["latitude"]
                lon = e["message"]["longitude"]
                address = e["message"].get("address", "")
                user_id = e["source"]["userId"]

                # --- DynamoDBに保存 ---
                save_location(user_id, lat, lon, address)

                area_code, area_name = get_nearest_area(lat, lon)
                weather, temps_text = get_weather(area_code)

                prompt = f"""
                今日の{area_name}の天気は「{weather}」、気温は「{temps_text}」です。
                この条件に最適な服装を日本語で具体的に提案してください。
                トップス・ボトムス・靴・アクセサリーなども含めてください。
                """

                ai_response = client.responses.create(
                    model="gpt-4o-mini",
                    input=prompt
                )
                advice = ai_response.output[0].content[0].text.strip()
                formatted_advice = format_markdown_for_line(advice)

                # --- 画像生成 ---
                image_url = None
                try:
                    image_prompt = (
                        f"{area_name}の今日の天気は{weather}、気温は{temps_text}。"
                        f"{advice}をイメージした服装スタイルをリアルな人物で。"
                    )
                    image_response = client.images.generate(
                        model="gpt-image-1",
                        prompt=image_prompt,
                        size="1024x1024"
                    )

                    if image_response.data and image_response.data[0].b64_json:
                        print("✅ 画像生成成功")
                        # Base64 → バイナリ変換
                        image_data = base64.b64decode(image_response.data[0].b64_json)

                        # S3アップロード
                        file_name = f"{uuid.uuid4()}.png"
                        s3.put_object(
                            Bucket=BUCKET_NAME,
                            Key=file_name,
                            Body=image_data,
                            ContentType="image/png"
                        )

                        # 公開URLを生成
                        image_url = f"https://{BUCKET_NAME}.s3.amazonaws.com/{file_name}"
                        print("✅ S3アップロード完了:", image_url)
                    else:
                        print("⚠️ 画像生成失敗: dataが空です", image_response)
                except Exception as img_err:
                    print("⚠️ OpenAI画像生成エラー:", img_err)

                # --- LINEに返信 ---
                reply_message(
                    reply_token,
                    f"📍{area_name}の天気：{weather}\n🌡気温：{temps_text}\n👕{formatted_advice}",
                    image_url
                )

            else:
                reply_message(reply_token, "位置情報を送ってください📍")

        return {"statusCode": 200, "body": "OK"}

    except Exception as e:
        print("Error:", e)
        return {"statusCode": 500, "body": str(e)}


def get_weather(area_code):
    JMA_URL = f"https://www.jma.go.jp/bosai/forecast/data/forecast/{area_code}.json"
    res = requests.get(JMA_URL)
    data = res.json()

    weather = next(
        (a["weathers"][0] for a in data[0]["timeSeries"][0]["areas"]
         if "weathers" in a and a["area"]["name"].endswith("部")),
        "天気データなし"
    )

    temps_text = "気温データなし"
    for ts in data[0]["timeSeries"]:
        for a in ts["areas"]:
            if "temps" in a:
                temps_text = " / ".join([f"{t}℃" for t in a["temps"]])
                break
    return weather, temps_text


def get_nearest_area(lat, lon):
    AREAS = [
        {"name": "北海道", "lat": 43.06, "lon": 141.35, "code": "016000"},
        {"name": "東京", "lat": 35.68, "lon": 139.76, "code": "130000"},
        {"name": "愛知県", "lat": 35.18, "lon": 136.91, "code": "230000"},
        {"name": "大阪府", "lat": 34.69, "lon": 135.50, "code": "270000"},
        {"name": "福岡県", "lat": 33.59, "lon": 130.40, "code": "400000"},
    ]
    def distance(a, b):
        return (a["lat"] - lat)**2 + (a["lon"] - lon)**2
    nearest = min(AREAS, key=lambda x: distance(x, {"lat": lat, "lon": lon}))
    return nearest["code"], nearest["name"]


def reply_message(reply_token, text, image_url=None):
    headers = {
        "Content-Type": "application/json",
        "Authorization": f"Bearer {LINE_ACCESS_TOKEN}"
    }

    messages = [{"type": "text", "text": text}]
    if image_url:
        messages.append({
            "type": "image",
            "originalContentUrl": image_url,
            "previewImageUrl": image_url
        })
    else:
        print("⚠️ 画像URLなし。テキストのみ返信。")

    body = {"replyToken": reply_token, "messages": messages}
    res = requests.post(LINE_REPLY_URL, headers=headers, json=body)
    print("LINE reply status:", res.status_code, res.text)

def format_markdown_for_line(md_text: str) -> str:
    """Markdown形式のテキストをLINEで読みやすい形に整形"""
    text = md_text

    # 見出しを絵文字＋改行付きに変換
    text = re.sub(r"^###\s*(.+)$", r"\n👕 \1", text, flags=re.MULTILINE)
    text = re.sub(r"^##\s*(.+)$", r"\n📌 \1", text, flags=re.MULTILINE)
    text = re.sub(r"^#\s*(.+)$", r"\n⭐ \1", text, flags=re.MULTILINE)

    # 強調（**text**）を単純に削除（LINEでは太字が無効）
    text = re.sub(r"\*\*(.+?)\*\*", r"\1", text)

    # 箇条書きを「・」に置換
    text = re.sub(r"^- ", "・", text, flags=re.MULTILINE)

    # Markdownリンクをテキストだけに
    text = re.sub(r"\[(.*?)\]\(.*?\)", r"\1", text)

    # 多重改行を整える
    text = re.sub(r"\n{3,}", "\n\n", text).strip()

    # 読みやすくするため、見出し前後に改行を追加
    text = re.sub(r"(\n[👕📌⭐])", r"\n\1", text)

    return text

def save_location(user_id, lat, lon, address):
    now = Decimal(str(time.time()))

    table.put_item(
        Item={
            "userId": user_id,
            "timestamp": now,
            "latitude": Decimal(str(lat)),
            "longitude": Decimal(str(lon)),
            "address": address,
        }
    )