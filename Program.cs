using QuizMaker.Models;
using QuizMaker.Services;
using System.Xml;

namespace QuizMaker
{
    internal class Program
    {
       static  XMLService xMLService;
        static List<QuestionItem> questionItems;
        static void Main(string[] args)
        {
            xMLService = new XMLService(["Data","Data.XML"]);
            LoadCildren();

            Console.WriteLine("select action: \n1. Create Question\n2. Answer Question");
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                if (value == 1)
                {
                    Console.Write("Type your Question:");
                    var question = Console.ReadLine();
                    Console.Write("Type your Answer:");
                    var answer = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(question) && !string.IsNullOrWhiteSpace(answer))
                    {
                        var newObj = new QuestionItem(question, answer);
                        xMLService.WriteXML(newObj);
                    }
                }
                else if (value == 2)
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
                            QuestionItem? found = questionItems.Find(q => q.Id == questionItem);
                            if (found != null)
                            {
                                Console.WriteLine();
                                Console.WriteLine($"you select: {found.Question}");
                                Console.WriteLine("type your answer");
                                if (Console.ReadLine() == found.Answer)
                                {
                                    Console.WriteLine("your answer is correct!");
                                }
                                else
                                {
                                    Console.WriteLine("you are not correct");
                                    Console.WriteLine($"the answer is: {found.Answer}");
                                }
                            }
                        }
                    }

                }
                else
                {
                    Console.WriteLine("invalid seletion");
                }
            }
            else
            {
                Console.WriteLine();
            }
        }

        static void LoadCildren()
        {
            questionItems = new List<QuestionItem>();
            var res = xMLService.GetRootElement();
            foreach (XmlNode item in res.ChildNodes)
            {
                string question = item["question"].InnerText;
                string answer = item["answer"].InnerText;
                questionItems.Add(new QuestionItem(question, answer));   
            }
        }
    }
    
}
