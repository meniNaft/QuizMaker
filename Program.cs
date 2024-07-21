using QuizMaker.Models;
using QuizMaker.Services;
using System.Xml;

namespace QuizMaker
{
    internal class Program
    {
       static readonly XMLService xmlService = new(["Data", "Data.XML"]);
       static List<QuestionItem> questionItems = [];
        static void Main(string[] args)
        {
           questionItems = LoadCildren();
            while (true)
            {
                Console.WriteLine("select action: \n1. Create Question\n2. Answer Question\n0. to close the program");
                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    switch (value)
                    {
                        case 0:
                            return;
                        case 1:
                            CreateQuestion();
                            break;
                        case 2:
                            AnswerQuestion();
                            break;
                        default:
                            Console.WriteLine("invalid seletion");
                            break;
                    }
                }
                else Console.WriteLine("invalid selection");
            }
        }

        static void CreateQuestion()
        {
            Console.Write("Type your Question:");
            var question = Console.ReadLine();
            Console.Write("Type your Answer:");
            var answer = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(question) && !string.IsNullOrWhiteSpace(answer))
            {
                var newObj = new QuestionItem(question, answer);
                questionItems.Add(newObj);
                xmlService.WriteXML(newObj);
            }
        }
    
        static void AnswerQuestion()
        {
            while (true)
            {
                Console.WriteLine("select question or type 0 to back:");
                foreach (var q in questionItems)
                {
                    Console.WriteLine($"{q.Id}. {q.Question}");
                }

                if (int.TryParse(Console.ReadLine(), out int questionItem))
                {
                    if (questionItem == 0) break;
                    QuestionItem? found = questionItems.Find(q => q.Id == questionItem);
                    if (found == null)
                    {
                        Console.WriteLine("incorect selection, select one of above or type 0 to back");
                        continue;
                    }
                    if (found != null)
                    {
                        Console.Write("type your answer: ");
                        if (Console.ReadLine() == found.Answer)
                        {
                            Console.WriteLine("your answer is correct!");
                        }
                        else
                        {
                            Console.WriteLine("you are not correct");
                            Console.WriteLine($"the answer is: {found.Answer}");
                        }
                        break;
                    }

                }
                else Console.WriteLine("invaid value");
            }

        }
        static List<QuestionItem> LoadCildren()
        {
            List<QuestionItem> temp = [];
            var root = xmlService.GetRootElement();
            foreach (XmlNode item in root.ChildNodes)
            {
                string question = item["question"]!.InnerText;
                string answer = item["answer"]!.InnerText;
                temp.Add(new QuestionItem(question, answer));   
            }
            return temp;
        }
    }
    
}
