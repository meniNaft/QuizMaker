namespace QuizMaker.Models
{
    internal class QuestionItem(string question, string answer)
    {
        static private int counter = 1;
        public int Id { get; set; } = counter++;
        public string Question { get; set; } = question;
        public string Answer { get; set; } = answer;
    }
}
