using Microsoft.Extensions.AI;
using System.Text;
using System.Text.Json;

namespace AiCustomControl
{
    public class AiChatClient
    {
        private readonly IChatClient _chatClient;
        public AiChatClient(IChatClient chatClient)
        {
            _chatClient = chatClient;
        }

        public async Task<String> ReviewAsync(List<string> textList)
        {
            var prompt = BuildPrompt(textList);

            var message = new ChatMessage(ChatRole.User, prompt);
            var response = await _chatClient.GetResponseAsync(message);

            var text = response.Text ?? string.Empty;

            try
            {
                return text;
            }
            catch
            {
                return "Error";
            }
        }

        private string BuildPrompt(List<string> textBoxList)
        {

            var sb = new StringBuilder();

            sb.AppendLine("あなたは「入力文字列を各テキストボックスに自動分類するアシスタント」です。");
            sb.AppendLine("ユーザーから「フィールド名：値」の形式で複数の情報が渡されます。");
            sb.AppendLine("あなたの仕事は、各情報を以下の TextBox に正しく分類し、JSON 形式で返すことです。");
            sb.AppendLine("【TextBox 一覧】");
            sb.AppendLine("- NameTextBox      … 名前を入れるテキストボックス");
            sb.AppendLine("- PhoneTextBox     … 電話番号を入れるテキストボックス");
            sb.AppendLine("- EmailTextBox     … メールアドレスを入れるテキストボックス");
            sb.AppendLine("【ルール】");
            sb.AppendLine("- フィールド名（例：「名前」「電話番号」「メールアドレス」など）から判断する");
            sb.AppendLine("- 形式（数値・ハイフン・@ を含む等）も補助的に利用してよい");
            sb.AppendLine("- 未知の項目は無視する");
            sb.AppendLine("- 必ずすべての TextBox を返す（空の場合は ''）");
            sb.AppendLine("【出力形式（JSON）】");
            sb.AppendLine("{");
            sb.AppendLine("  \"NameTextBox: \"\",  ");
            sb.AppendLine("  \"PhoneTextBox:\"\", ");
            sb.AppendLine("  \"EmailTextBox:\"\", ");
            sb.AppendLine("}");
            sb.AppendLine("");
            sb.AppendLine("分類結果のみ返してください。");

            return sb.ToString();
        }

    }





}