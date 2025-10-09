import axios from 'axios';
import http from 'http';
import OpenAI from 'openai';
import dotenv from 'dotenv';

dotenv.config();

const url = "https://www.jma.go.jp/bosai/forecast/data/forecast/";
const area = "230000"; // 愛知県（名古屋を含む）
const port = 8080;

const client = new OpenAI({
  apiKey: process.env['OPENAI_API_KEY'],
});

const server = http.createServer(async (req, res) => {
  res.writeHead(200, { 'Content-Type': 'text/html; charset=utf-8' });

  try {
    // 🌤 天気データ取得
    const response = await axios.get(`${url}${area}.json`);
    const data = response.data;

    // --- 天気（今日の名古屋・西部代表） ---
    const weather = data[0].timeSeries[0].areas.find(a => a.area.name === "西部")?.weathers?.[0]
      || "天気データなし";

    // --- 短期予報の気温 ---
    const shortTerm = data[0].timeSeries.find(ts =>
      ts.areas.some(a => a.area.name === "名古屋" && a.temps)
    );

    let tempsText = "気温データなし";
    if (shortTerm) {
      const nagoya = shortTerm.areas.find(a => a.area.name === "名古屋");
      tempsText = nagoya.temps.map(t => `${t}℃`).join(" / ");
    }

    // 🌡 GPTに服装を提案させる
    const aiResponse = await client.responses.create({
      model: "gpt-4o-mini",
      input: `今日の名古屋の天気は「${weather}」、気温は「${tempsText}」です。
      この条件に最適な服装を日本語で具体的にアドバイスしてください。`,
    });

    const clothingAdvice = aiResponse.output[0].content[0].text;

    // --- HTML出力 ---
    const html = `
      <html lang="ja">
      <head>
        <meta charset="UTF-8">
        <title>名古屋の天気と服装アドバイス</title>
        <style>
          body {
            font-family: 'Segoe UI', sans-serif;
            background: #f7f9fc;
            color: #333;
            padding: 30px;
          }
          h1 { color: #2c3e50; }
          .card {
            background: white;
            padding: 20px;
            border-radius: 12px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.1);
            width: 480px;
            line-height: 1.6;
          }
          .advice {
            background: #eef6ff;
            padding: 10px;
            border-left: 4px solid #3498db;
            margin-top: 15px;
            border-radius: 6px;
          }
        </style>
      </head>
      <body>
        <h1>☀ 名古屋の天気と服装アドバイス</h1>
        <div class="card">
          <p><strong>天気:</strong> ${weather}</p>
          <p><strong>今日の気温:</strong> ${tempsText}</p>
          <div class="advice">
            <strong>👕 服装アドバイス:</strong><br>
            ${clothingAdvice}
          </div>
        </div>
      </body>
      </html>
    `;

    res.write(html);

  } catch (error) {
    console.error(error);
    res.write("<h1>データ取得またはAI処理に失敗しました。</h1>");
  }

  res.end();
});

server.listen(port);
console.log(`Server running → http://localhost:${port}`);