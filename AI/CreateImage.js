import OpenAI from 'openai';
import fs from 'fs';
import dotenv from 'dotenv';

dotenv.config();

// OpenAI クライアントを初期化
const openai = new OpenAI({
  apiKey: process.env['OPENAI_API_KEY'],
});

async function generateImage() {
  try {
    // 画像を生成
    const response = await openai.images.generate({
      model: "gpt-image-1",
      prompt: "美しい桜の木の下で読書する猫、水彩画風",
      size: "1024x1024",
      quality: "medium",
      n: 1
    });

    // BASE64エンコードされた画像データを取得
    const imageData = response.data[0].b64_json;
    
    // ファイルに保存
    fs.writeFileSync(
      'generated_image.jpg', 
      Buffer.from(imageData, 'base64')
    );
    
    console.log('画像が正常に生成されました！');
  } catch (error) {
    console.error('エラーが発生しました:', error);
  }
}

generateImage();