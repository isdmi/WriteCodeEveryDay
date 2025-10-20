import json
import requests
import os

LINE_REPLY_ENDPOINT = "https://api.line.me/v2/bot/message/reply"
CHANNEL_ACCESS_TOKEN = os.environ['CHANNEL_ACCESS_TOKEN']

def lambda_handler(event, context):
    print(event)
    body = json.loads(event.get('body', '{}'))
    for e in body.get('events', []):
        reply_token = e['replyToken']

        if e['type'] == 'message':
            msg_type = e['message']['type']

            # 📍位置情報メッセージ
            if msg_type == 'location':
                lat = e['message']['latitude']
                lon = e['message']['longitude']
                address = e['message']['address']
                reply_text = f"位置情報を受け取りました！\n住所：{address}\n緯度：{lat}\n経度：{lon}"

                reply_message(reply_token, reply_text)

            # 💬 通常のテキストメッセージ
            elif msg_type == 'text':
                text = e['message']['text']
                reply_message(reply_token, f"あなたのメッセージ: {text}")

    return {"statusCode": 200, "body": "OK"}

def reply_message(token, text):
    headers = {
        "Content-Type": "application/json",
        "Authorization": f"Bearer {CHANNEL_ACCESS_TOKEN}"
    }
    data = {
        "replyToken": token,
        "messages": [{"type": "text", "text": text}]
    }
    requests.post(LINE_REPLY_ENDPOINT, headers=headers, data=json.dumps(data))