import json
import os
import requests

CHANNEL_ACCESS_TOKEN = os.environ['CHANNEL_ACCESS_TOKEN']

def lambda_handler(event, context):
    # LINEからのイベントを取得
    body = json.loads(event['body'])
    print(f"Received body: {body}")

    # 応答対象がなければ終了
    if 'events' not in body or len(body['events']) == 0:
        return {'statusCode': 200, 'body': 'No event'}

    event_data = body['events'][0]
    reply_token = event_data.get('replyToken')
    user_message = event_data.get('message', {}).get('text', '')

    if not reply_token:
        return {'statusCode': 200, 'body': 'No reply token'}

    # 応答メッセージ作成
    reply_text = f'「{user_message}」と受け取りました！'

    # LINE Reply API呼び出し
    headers = {
        'Content-Type': 'application/json',
        'Authorization': f'Bearer {CHANNEL_ACCESS_TOKEN}'
    }
    data = {
        'replyToken': reply_token,
        'messages': [{'type': 'text', 'text': reply_text}]
    }

    response = requests.post(
        'https://api.line.me/v2/bot/message/reply',
        headers=headers,
        data=json.dumps(data)
    )

    print(f"LINE API response: {response.status_code}, {response.text}")

    return {'statusCode': 200, 'body': 'OK'}