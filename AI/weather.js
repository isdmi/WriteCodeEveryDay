import axios from "axios";
import http from "http";
import OpenAI from "openai";
import dotenv from "dotenv";
import fs from "fs";
import path from "path";
import { marked } from "marked";

dotenv.config();

const url = "https://www.jma.go.jp/bosai/forecast/data/forecast/";
const area = "230000"; // 愛知県（名古屋を含む）
const port = 8080;

// キャッシュ保存フォルダ
const cacheDir = path.resolve("./cache");
if (!fs.existsSync(cacheDir)) fs.mkdirSync(cacheDir);

const client = new OpenAI({
  apiKey: process.env.OPENAI_API_KEY,
});

const server = http.createServer(async (req, res) => {
  res.writeHead(200, { "Content-Type": "text/html; charset=utf-8" });

  try {
    // 🌤 天気データ取得
    const response = await axios.get(`${url}${area}.json`);
    const data = response.data;

    // --- 天気（今日の名古屋・西部代表） ---
    const weather =
      data[0].timeSeries[0].areas.find((a) => a.area.name === "西部")
        ?.weathers?.[0] || "天気データなし";

    // --- 短期予報の気温 ---
    const shortTerm = data[0].timeSeries.find((ts) =>
      ts.areas.some((a) => a.area.name === "名古屋" && a.temps)
    );

    let tempsText = "気温データなし";
    if (shortTerm) {
      const nagoya = shortTerm.areas.find((a) => a.area.name === "名古屋");
      tempsText = nagoya.temps.map((t) => `${t}℃`).join(" / ");
    }

    // 🌡 GPTで服装アドバイス生成
    const aiResponse = await client.responses.create({
      model: "gpt-4o-mini",
      input: `今日の名古屋の天気は「${weather}」、気温は「${tempsText}」です。
      この条件に最適な服装を日本語で具体的に提案してください。
      例：トップス、ボトムス、靴、アクセサリーなども具体的に書いてください。`,
    });

    const clothingAdvice = aiResponse.output[0].content[0].text;
    const clothingHtml = marked.parse(clothingAdvice, { sanitize: true });


    // --- キャッシュキー生成 ---
    const cacheKey = `${weather}_${tempsText}`.replace(/[^\w一-龠ぁ-んァ-ンー０-９℃]/g, "_");
    const cachePath = path.join(cacheDir, `${cacheKey}.json`);

    let imageUrl;

    if (fs.existsSync(cachePath)) {
      // 💾 キャッシュ利用
      console.log(`✅ キャッシュ利用: ${cacheKey}`);
      const cachedData = JSON.parse(fs.readFileSync(cachePath, "utf8"));
      imageUrl = cachedData.imageUrl;
    } else {
      // 🖼 画像生成API呼び出し
      console.log(`🎨 新規生成: ${cacheKey}`);

      const imagePrompt = `日本人向けのカジュアルな全身服装。
      今日の名古屋の天気は「${weather}」、気温は「${tempsText}」。
      以下のアドバイスに合った服装を表現したリアルなイラストスタイルの人物画像:
      「${clothingAdvice}」`;

      const imageResponse = await client.images.generate({
        model: "gpt-image-1",
        prompt: imagePrompt,
        size: "1024x1024",
        quality: "medium",
      });

      const imageBase64 = imageResponse.data[0].b64_json;
      imageUrl = `data:image/png;base64,${imageBase64}`;

      // キャッシュ保存
      fs.writeFileSync(cachePath, JSON.stringify({ imageUrl }, null, 2), "utf8");
    }

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
            display: flex;
            flex-direction: column;
            align-items: center;
          }
          h1 { color: #2c3e50; }
          .card {
            background: white;
            padding: 20px;
            border-radius: 12px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.1);
            width: 600px;
            line-height: 1.6;
          }
          .info {
            margin-bottom: 15px;
          }
          .advice-container {
            display: flex;
            align-items: flex-start;
            gap: 20px;
            margin-top: 15px;
          }
          .advice {
            background: #eef6ff;
            padding: 10px;
            border-left: 4px solid #3498db;
            border-radius: 6px;
            flex: 1;
          }
          img {
            border-radius: 10px;
            box-shadow: 0 2px 6px rgba(0,0,0,0.2);
            width: 256px;
            height: 256px;
            object-fit: cover;
          }
        </style>
      </head>
      <body>
        <h1>☀ 名古屋の天気と服装アドバイス</h1>
        <div class="card">
          <div class="info">
            <p><strong>天気:</strong> ${weather}</p>
            <p><strong>今日の気温:</strong> ${tempsText}</p>
          </div>
          <div class="advice-container">
            <div class="advice">
              <strong>👕 服装アドバイス:</strong><br>
              ${clothingHtml}
            </div>
            <img src="${imageUrl}" alt="服装イメージ">
          </div>
        </div>
      </body>
      </html>
    `;

    res.write(html);
  } catch (error) {
    console.error(error);
    res.write("<h1>データ取得またはAI処理に失敗しました。</h1>");
    if (error.response?.data) {
      res.write(`<pre>${JSON.stringify(error.response.data, null, 2)}</pre>`);
    }
  }

  res.end();
});

server.listen(port);
console.log(`Server running → http://localhost:${port}`);