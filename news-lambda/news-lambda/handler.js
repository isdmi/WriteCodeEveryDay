import axios from "axios";
import xml2js from "xml2js";

const RSS_URL = "https://news.yahoo.co.jp/rss/topics/it.xml";

// ✅ ニュース取得API
export const getNews = async (event) => {
  try {
    const { data } = await axios.get(RSS_URL);
    const result = await xml2js.parseStringPromise(data);

    const items = result.rss.channel[0].item.map((item) => ({
      title: item.title[0],
      link: item.link[0],
      pubDate: item.pubDate[0],
    }));

    return {
      statusCode: 200,
      headers: {
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "*",
        "Access-Control-Allow-Headers": "*",
        "Access-Control-Allow-Methods": "GET",
      },
      body: JSON.stringify({
        count: items.length,
        news: items.slice(0, 10),
      }),
    };
  } catch (error) {
    console.error(error);
    return {
      statusCode: 500,
      headers: {
        "Access-Control-Allow-Origin": "*",
        "Access-Control-Allow-Headers": "*",
        "Access-Control-Allow-Methods": "GET",
      },
      body: JSON.stringify({ error: "ニュースの取得に失敗しました" }),
    };
  }
};