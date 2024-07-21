using QuizMaker.Models;
using System.Xml;
using System.Xml.Linq;

namespace QuizMaker.Services
{
    internal class XMLService
    {
        private readonly XmlDocument xmlDoc;
        private readonly XmlNode root;
        private readonly string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private readonly string xmlPath;

        public XMLService(string[] XMLPathFromCurrentDir)
        {
            xmlPath = BuildPath(XMLPathFromCurrentDir);
            xmlDoc = LoadXML();
            root = xmlDoc.DocumentElement ?? throw new Exception("no root element existing");

        }
        private string BuildPath(params string[] xmlPath)
        {
            string[] fullPath = new string[xmlPath.Length + 1];
            fullPath[0] = baseDirectory;
            Array.Copy(xmlPath, 0, fullPath, 1, xmlPath.Length);
            return Path.Combine(fullPath);
        }

        private XmlDocument LoadXML()
        {
            XmlDocument xmlDoc = new();
            using (FileStream fileStream = new (xmlPath, FileMode.Open, FileAccess.Read))
            {
                xmlDoc.Load(fileStream);
            }
            return xmlDoc;
        }

        public XmlNode GetRootElement() => root;

        public void WriteXML(QuestionItem item)
        {
            var question = new XElement("question", item.Question);
            var answer = new XElement("answer", item.Answer);
            var newItem = new XElement("item", question, answer);
            root.AppendChild(xmlDoc.ReadNode(newItem.CreateReader())!);
            xmlDoc.Save(xmlPath);
        }
    }
}
