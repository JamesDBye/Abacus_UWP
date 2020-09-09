using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Xml;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.Popups;

namespace Abacus
{
    class Narratives
    {
        public static string GetXMLNarrs(string strXpath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string xstring = "<All_Text_Strings><Main_Text_strings><Introduction><comment1>";

            //Build up the xml document
            xstring += "Welcome to this Abacus application. Its purpose is to show how to perform calculations step by step on an Abacus. There are different types of Abacuses, we will be using the type known as a Soroban.";
            xstring += "</comment1></Introduction></Main_Text_strings></All_Text_Strings>";

            xmlDoc.LoadXml(xstring);

            XmlNode titleNode = xmlDoc.SelectSingleNode(strXpath);
            string strNarr = titleNode.InnerText;
            return strNarr;
        }
    }
}
