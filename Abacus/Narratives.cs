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
            xstring += "Welcome to this Abacus application. It&#39;s purpose is to show how to perform calculations step by step on an Abacus.";
            xstring += "\nThere are different types of Abacuses, the objects above represent a type of abacus known as a Soroban.";
            xstring += "</comment1><More_info>";
            xstring += "\nThe circles are like the beads on an abacus, that can slide up or down a vertical stick running through them. ";
            
            xstring += "\nThe top bead represents a &#39;5&#39;, and the four beads below represent a &#39;1&#39;. ";
            xstring += "To display &#39;7&#39; in the last column, you would need a &#39;5&#39; above plus &#39;2&#39; below. ";
            xstring += "&#39;Above&#39; and &#39;below&#39; are sometimes referred to as &#39;Heaven&#39; and &#39;Earth&#39;.\nAt the moment, the Soroban is displaying the number zero. To see what the number 25 would look like, ";
            xstring += "type 25 into the #1 box and click the button labelled &#39;Display Number #1&#39; and it will display above. Alternatively, click one of the other buttons to see how to count or perform addition.";
            xstring += " </More_info></Introduction><Addition><Comment1>";
            xstring += "When adding numbers on a stick, if the result would be greater than 9, you must SUBTRACT a complementary number instead - and then add 1 to the stick on the left. [Complementary numbers: 1&amp;9, 2&amp;8, 3&amp;7, 4&amp;6, 5&amp;5, 6&amp;4, 7&amp;3, 8&amp;2, 9&amp;1 ]";
            xstring += "</Comment1><Comment2>";
            xstring += "is the larger number. We will display this first.";
            xstring += "</Comment2><Comment3>";
            xstring += "Now lets add the numbers together, right to left.";
            xstring += "</Comment3></Addition><Subtraction><Comment1>";
            xstring += "Subtraction is very similar to addition to perform. When you cannot subtract on a single column you must ADD the complimentary number, and subtract 1 from the left column.";
            xstring += "</Comment1><Comment2>";
            xstring += " is the larger value. If you want to subtract a bigger number from a smaller number, remember the result will be negative. If that is the case, enter the bigger VALUE first, subtract the smaller value from it. e.g. 2 minus 5 = -3 ; 5 minus 2 = 3. Same result VALUE, different +/- sign.";
            xstring += "</Comment2></Subtraction><Multiplication><Introduction>";
            xstring += "Multiplication is more complex, but makes repeated use of the addition method.";
            xstring += "</Introduction><Terminology>";
            xstring += "We need to introduce some terminology here. When you multiply two numbers, one number is referred to as the 'multiplicand' and the other is called the 'multiplier.' The result is called the 'product'.";
            xstring += "For example, if you want to multiply 22 by 3, then 22 is the multiplicand and 3 is the multiplier and 66 is the product. You could alternatively say 3 is the multiplicand and 22 is the multiplier - the result will be the same.";
            xstring += "It is easier to explain the method here by using these terms. I recommend you assign the smaller number as the multiplier.";
            xstring += "</Terminology></Multiplication></Main_Text_strings><Soroban_Text_Strings><MultiplyTwoLongs><Comment1>";
            xstring += "For now we are just working with integers, though the method will also work for numbers with decimals. The multiplier is placed at the far left of the soroban, the multiplicand goes on the right, but not fully to the right.";
            xstring += "We need to work out where to place the multiplicand and for this we need to look at how many whole digits are in the multiplier and multiplicand.";
            xstring += "If the multiplicand is 123 and the multiplier is 32, there are 5 whole digits in total. This means we enter the multiplicand 5 columns to the left of the rightmost column.";
            xstring += "</Comment1><Comment2>";
            xstring += "To multiply two numbers together, we multiply a single digit of the multiplier by a single digit of the multiplicand. We take the product and add it to the right of the multiplicand. We repeat this, going across the multiplier left to right and across the multiplicand left to right.";
            xstring += "The products should be dealt with as 2-digit numbers - i.e. 6 is 06, and each product is shifted to the right each time the digit of the multiplier changes.";
            xstring += "It sounds complicated but it isn't. Watch how your example is calculated step by step below. You do need to know your times tables from 2x2 to 9x9 to be able to perform this on a soroban for real :)";
            xstring += "</Comment2></MultiplyTwoLongs></Soroban_Text_Strings></All_Text_Strings>";

            xmlDoc.LoadXml(xstring);
            XmlNode titleNode = xmlDoc.SelectSingleNode(strXpath);
            string strNarr = titleNode.InnerText;
            return strNarr;
        }
    }
}
