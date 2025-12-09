import json
import boto3
from datetime import datetime
from decimal import Decimal

dynamodb = boto3.resource("dynamodb")
table = dynamodb.Table("HealthStatus")

def to_decimal(value):
    return Decimal(str(value))

def lambda_handler(event, context):

    body = json.loads(event["body"]) if "body" in event else event

    item = {
        "userId": body["userId"],
        "date": body["date"],  # YYYY-MM-DD

        "conditionCurrent": body["conditionCurrent"],
        "conditionChange": body["conditionChange"],

        "sleep": body["sleep"],
        "breakfast": body["breakfast"],

        "symptoms": {
            "fatigue": to_decimal(body["symptoms"]["fatigue"]),
            "headache": to_decimal(body["symptoms"]["headache"]),
            "fever": to_decimal(body["symptoms"]["fever"]),
            "cough": to_decimal(body["symptoms"]["cough"]),
            "stomachache": to_decimal(body["symptoms"]["stomachache"]),
            "nausea": to_decimal(body["symptoms"]["nausea"]),
            "dizziness": to_decimal(body["symptoms"]["dizziness"]),
            "otherScore": to_decimal(body["symptoms"].get("otherScore", 0)),
            "otherText": body["symptoms"].get("otherText", "")
        },

        "workImpact": {
            "performance": to_decimal(body["workImpact"]["performance"]),
            "restriction": body["workImpact"].get("restriction", "")
        },

        "freeNote": body.get("freeNote", ""),
        "createdAt": datetime.now().isoformat()
    }

    table.put_item(Item=item)

    return {
        "statusCode": 200,
        "body": json.dumps({"message": "saved"}, ensure_ascii=False)
    }