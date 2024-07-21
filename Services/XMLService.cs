using QuizMaker.Models;
using System.Xml.Linq;

namespace QuizMaker.Services
{
    internal class XMLService
    {
        private readonly XDocument xDoc;
        private XElement Root { get; }
        private readonly string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private readonly string xmlPath;

        public XMLService(string[] XMLPathFromCurrentDir)
        {
            xmlPath = BuildPath(XMLPathFromCurrentDir);
            //ensure the path directory is exist
            if (!Directory.Exists(xmlPath)) Directory.CreateDirectory(xmlPath);
            xDoc = File.Exists(xmlPath) ? XDocument.Load(xmlPath): new XDocument(new XElement("Data"));
            Root = xDoc.Root ?? throw new Exception("no root element existing");
        }
        private string BuildPath(params string[] xmlPath)
        {
            string[] fullPath = new string[xmlPath.Length + 1];
            fullPath[0] = baseDirectory;
            Array.Copy(xmlPath, 0, fullPath, 1, xmlPath.Length);
            return Path.Combine(fullPath);
        }

        public void WriteXML(QuestionItem item)
        {
            var question = new XElement("question", item.Question);
            var answer = new XElement("answer", item.Answer);
            var newItem = new XElement("item", question, answer);
            Root.Add(newItem);
            xDoc.Save(xmlPath);
        }

        public List<QuestionItem> LoadCildren()
        {
            List<QuestionItem> temp = [];
            foreach (XElement item in Root.Elements())
            {
                string question = item.Element("question")!.Value;
                string answer = item.Element("answer")!.Value;
                temp.Add(new QuestionItem(question, answer));
            }
            return temp;
        }
    }
}
