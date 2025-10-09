import http from 'http';
import OpenAI from 'openai';
import dotenv from 'dotenv';

dotenv.config();

const server = new http.createServer((req, res) => {
    res.writeHead(200, { 'Content-Type': 'text/html; charset=utf-8' });
    res.write(response.output_text);
    res.end();
});

const client = new OpenAI({
  apiKey: process.env['OPENAI_API_KEY'], // This is the default and can be omitted
});

const response = await client.responses.create({
  model: 'gpt-4o',
  instructions: '日本語で答えてください',
  input: '一分間スピーチの内容を考えてください',
});

const port = 8080;
server.listen(port);
console.log('Server listen on port ' + port);