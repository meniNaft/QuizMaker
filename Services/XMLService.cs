using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace QuizMaker.Services
{
    internal class XMLService
    {
        private XmlDocument activeDoc;
        private string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string xmlPath;

        public XMLService(string[] XMLPathFromCurrentDir)
        {
            xmlPath = BuildPath(XMLPathFromCurrentDir);
            activeDoc = LoadXML();

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

        public XmlNode GetNodeByName(string name) 
        { 
            var res = activeDoc.SelectSingleNode(name);
            if(res == null)
            {
                throw new Exception($"Node '{name}' not found.");
            }
            else
            {
                return res;
            }
        }

        public void WriteXML()
        {

        }

    }
}
