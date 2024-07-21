namespace QuizMaker.Models
{
    internal class QuestionItem
    {
        public QuestionItem(string question, string answer)
        {

            Id = counter++;
            Question = question;
            Answer = answer;
        }
        static private int counter = 1;
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
