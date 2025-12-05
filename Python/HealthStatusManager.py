import json
import boto3
from datetime import datetime
from decimal import Decimal

dynamodb = boto3.resource("dynamodb")
table = dynamodb.Table("HealthStatus")

def lambda_handler(event, context):

    if "body" in event:
        try:
            body = json.loads(event["body"])
        except:
            return {
                "statusCode": 400,
                "body": json.dumps({"error": "Invalid JSON"})
            }
    else:
        body = event

    # 必須項目チェック
    required = ["userId", "temperature", "condition"]
    for r in required:
        if r not in body:
            return {
                "statusCode": 400,
                "body": json.dumps({"error": f"Missing field: {r}"})
            }

    # 数値（float）を Decimal に変換
    temperature = Decimal(str(body["temperature"]))

    today = datetime.now().strftime("%Y-%m-%d")

    item = {
        "userId": body["userId"],
        "date": today,
        "temperature": temperature,
        "condition": body["condition"],
        "memo": body.get("memo", "")
    }

    table.put_item(Item=item)

    return {
        "statusCode": 200,
        "body": json.dumps({"message": "saved", "data": item}, default=str)
    }