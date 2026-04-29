namespace Application.Features.Questions.Dtos
{
    public sealed class QuestionCategoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int TotalQuestions { get; set; }
        public IEnumerable<QuestionResponse> Questions { get; set; } = [];
    }
}
