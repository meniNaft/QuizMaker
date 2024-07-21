using QuizMaker.Models;
using QuizMaker.Services;

namespace QuizMaker
{
    internal class Program
    {
       static  XMLService xMLService;
        static QuestionItem[] questionItems;
        static void Main(string[] args)
        {
            xMLService = new XMLService(["Data","Data.XML"]);
            LoadCildren();

            Console.WriteLine("select action: \n1. Create Question\n2. Answer Question:");
            bool success =  int.TryParse(Console.ReadLine(), out int value);
            if (success)
            {
                if (value == 1)
                {
                    Console.Write("Type your Question:");
                    var question = Console.ReadLine();
                    Console.Write("Type your Answer:");
                    var answer = Console.ReadLine();
                    if (!String.IsNullOrWhiteSpace(question) && !String.IsNullOrWhiteSpace(answer))
                    {
                        var newObj = new QuestionItem(question, answer);
                        //write into xml
                    }
                }
                else if (value == 2)
                {
                    Console.WriteLine("list of existing questions:");
                    //list all question
                    bool success2 = int.TryParse(Console.ReadLine(), out int QuestionItem);
                    if (success2)
                    {

                    }
                }
                else
                {
                    Console.WriteLine("invalid seletion");
                }
            }
        }

        static void LoadCildren()
        {
           
             var res = xMLService.GetNodeByName("Data");
            questionItems = new QuestionItem[res.ChildNodes.Count];
            foreach (var item in res.ChildNodes)
            {
                questionItems.
            }
            
        }
    }
    
}
