import express from "express";
import { fetchNews } from "./fetchNews.js";

const app = express();
const PORT = 3000;

app.get("/news", async (req, res) => {
  try {
    const news = await fetchNews();
    res.json({ count: news.length, news });
  } catch (error) {
    console.error(error);
    res.status(500).json({ error: "ニュースの取得に失敗しました" });
  }
});

app.listen(PORT, () => {
  console.log(`✅ Server is running: http://localhost:${PORT}`);
});