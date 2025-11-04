using AiCodeReviewer;

namespace AiUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            var codeReviewResult = new CodeReviewResult()
            {
                Summary = "問題があります",
            };
            codeReviewResult.Issues.Add(new CodeIssue("Null チェック不足", "null のチェックが必要です", "High"));
            
            var chatClient = new MockChatClient(_ => codeReviewResult);

            var reviewer = new SimpleCodeReviewer(chatClient);

            var json = await reviewer.ReviewAsync("dummy code");
            Assert.Contains("Null チェック不足", json.Summary);
        }
    }
}