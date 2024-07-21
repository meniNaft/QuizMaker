namespace QuizMaker.Models
{
    internal class QuestionItem
    {
        public QuestionItem(string question, string answers)
        {

            Id = counter++;
            Question = question;
            Answers = answers;
        }
        static private int counter = 1;
        public int Id { get; set; }
        public string Question { get; set; }
        string Answers { get; set; }
    }
}
