// fetchNews.js（RSS対応版）
import axios from "axios";
import * as xml2js from "xml2js";

const RSS_URL = "https://news.yahoo.co.jp/rss/topics/it.xml";

export async function fetchNews() {
  const { data } = await axios.get(RSS_URL);
  const result = await xml2js.parseStringPromise(data);

  const items = result.rss.channel[0].item.map(item => ({
    title: item.title[0],
    link: item.link[0],
    pubDate: item.pubDate[0]
  }));

  return items.slice(0, 10);
}