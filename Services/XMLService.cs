using QuizMaker.Models;
using System.Xml;

namespace QuizMaker.Services
{
    internal class XMLService
    {
        private XmlDocument xmlDoc;
        private XmlNode root;
        private string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string xmlPath;

        public XMLService(string[] XMLPathFromCurrentDir)
        {
            xmlPath = BuildPath(XMLPathFromCurrentDir);
            xmlDoc = LoadXML();
            root = xmlDoc.DocumentElement;

        }
        private string BuildPath(params string[] xmlPath)
        {
            // Create an array that includes the base directory and the xmlPath elements
            string[] fullPath = new string[xmlPath.Length + 1];
            fullPath[0] = baseDirectory;
            Array.Copy(xmlPath, 0, fullPath, 1, xmlPath.Length);
            // Combine all the elements into a single path
            return Path.Combine(fullPath);
        }

        private XmlDocument LoadXML()
        {
            XmlDocument xmlDoc = new XmlDocument();
            using (FileStream fileStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            {
                xmlDoc.Load(fileStream);
            }
            return xmlDoc;
        }

        public XmlNode GetRootElement() => root;

        public void WriteXML(QuestionItem item)
        {
            var newItem = xmlDoc.CreateElement("item");
            var newQuestion = xmlDoc.CreateElement("question");
            newQuestion.InnerText = item.Question;
            var newAnswer = xmlDoc.CreateElement("answer");
            newAnswer.InnerText = item.Answer;
            newItem.AppendChild(newQuestion);
            newItem.AppendChild(newAnswer);
            root.AppendChild(newItem);
            xmlDoc.Save(xmlPath);
        }
    }
}
