using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Abucus
{
    public class Narratives
    {
        public static string GetXMLNarrs(XmlDocument xmlDoc, string strXpath)
        {
            XmlNode titleNode = xmlDoc.SelectSingleNode(strXpath);
            // Shorthand IF-THEN-ELSE  = (condition) ? expressionTrue :  expressionFalse;
            string strNarr = (titleNode != null) ? titleNode.InnerText : "";
            return strNarr;
        }

    }
}