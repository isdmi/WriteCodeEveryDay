namespace AiCodeReviewer
{
    public interface ICodeReviewer
    {
        Task<CodeReviewResult> ReviewAsync(string code, CodeReviewOptions? options = null);
    }
}
