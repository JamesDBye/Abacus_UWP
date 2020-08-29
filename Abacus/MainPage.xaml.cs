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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Abacus
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public static string StrNarrAdd(int intLarge, int intSmall)
        {
            string strTemp;
            strTemp = " (can't add " + intSmall + " to " + Convert.ToString(intLarge - intSmall);
            strTemp += " so minus " + (10 - intSmall) + " and add 1 to the left column)\n";
            return strTemp;
        }

        public static long ArrayToLong(int[] pArrInts)
        {
            string strCombined = "";
            for (int i = 0; i < 12; i++)
            {
                strCombined += pArrInts[i];
            }
            return long.Parse(strCombined);
        }

        public void ShowValueOnSoroban(long pValue)
        {
            //declare variables
            string strMaxInput = pValue.ToString();
            int[] arrNumsLarge = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Ellipse H1, H2, E1, E2, E3, E4, E5;

            // Put each digit of pValue into the arrNumsLarge array.
            // The array represents each column of the soroban.
            for (int i = 0; i < strMaxInput.Length; i++)
            {
                arrNumsLarge[i + (12 - strMaxInput.Length)] = int.Parse(strMaxInput.Substring(i, 1));
            }

            //Loop through each column, left to left

            for (int i = 0; i < 12; i++)
            {
                //H - Heaven bead, E - Earth bead
                //FindName allows you to identify a control by it's name, so you can then programmatically refer to it.
                H1 = (Ellipse)this.FindName("R7C" + i.ToString());
                H2 = (Ellipse)this.FindName("R6C" + i.ToString());

                E1 = (Ellipse)this.FindName("R5C" + i.ToString());
                E2 = (Ellipse)this.FindName("R4C" + i.ToString());
                E3 = (Ellipse)this.FindName("R3C" + i.ToString());
                E4 = (Ellipse)this.FindName("R2C" + i.ToString());
                E5 = (Ellipse)this.FindName("R1C" + i.ToString());

                // Heaven beads
                if (arrNumsLarge[i] >= 5)
                {
                    H1.Visibility = Visibility.Collapsed;
                    H2.Visibility = Visibility.Visible;
                }
                else
                {
                    H1.Visibility = Visibility.Visible;
                    H2.Visibility = Visibility.Collapsed;
                }
                // Earth beads
                switch (arrNumsLarge[i])
                {
                    case 0:
                    case 5:
                        E2.Visibility = E3.Visibility = E4.Visibility = E5.Visibility = Visibility.Visible;
                        E1.Visibility = Visibility.Collapsed;
                        break;
                    case 1:
                    case 6:
                        E1.Visibility = E3.Visibility = E4.Visibility = E5.Visibility = Visibility.Visible;
                        E2.Visibility = Visibility.Collapsed;
                        break;
                    case 2:
                    case 7:
                        E1.Visibility = E2.Visibility = E4.Visibility = E5.Visibility = Visibility.Visible;
                        E3.Visibility = Visibility.Collapsed;
                        //E2.Fill = "Black"; -- should be easy, it's not
                        break;
                    case 3:
                    case 8:
                        E1.Visibility = E2.Visibility = E3.Visibility = E5.Visibility = Visibility.Visible;
                        E4.Visibility = Visibility.Collapsed;
                        break;
                    case 4:
                    case 9:
                        E1.Visibility = E2.Visibility = E3.Visibility = E4.Visibility = Visibility.Visible;
                        E5.Visibility = Visibility.Collapsed;
                        break;
                    default:
                        E1.Visibility = E2.Visibility = E3.Visibility = E4.Visibility = E5.Visibility = Visibility.Collapsed;
                        break;
                }
            }//loop ends
        }

        //Read an integer into an array, eg. 12345 --> {0,0,0,0,0,0,0,1,2,3,4,5}
        public static int[] LongToArray(long pValue)
        {
            string strMaxInput = pValue.ToString();
            int[] arrNumsLarge = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            // Put each digit of intMaxInput into the arrNumsLarge array.
            // The array represents each column of the soroban.
            for (int i = 0; i < strMaxInput.Length; i++)
            {
                arrNumsLarge[i + (12 - strMaxInput.Length)] = int.Parse(strMaxInput.Substring(i, 1));
            }
            return arrNumsLarge;
        }

        public void AddTwoLongs(long longLarge, long longSmall) // removed static keyword, as it wouldn't work until removed.
        {
            string strPause = "";
            int[] pArrLarge = LongToArray(longLarge);
            int[] pArrSmall = LongToArray(longSmall); ;

            for (int i = 0; i < 12; i++)
            {
                //whether or not to display this calculation
                bool boolSmallIsZero = (pArrSmall[i] == 0);

                //main logic
                pArrLarge[i] = pArrLarge[i] + pArrSmall[i];

                //Carry left bead check
                if (pArrLarge[i] > 9)
                {
                    switch (pArrSmall[i])
                    {
                        case 1:
                            strPause = StrNarrAdd(pArrLarge[i], pArrSmall[i]);
                            pArrLarge[i] = pArrLarge[i] - pArrSmall[i] - 9;
                            break;
                        case 2:
                            strPause = StrNarrAdd(pArrLarge[i], pArrSmall[i]);
                            pArrLarge[i] = pArrLarge[i] - pArrSmall[i] - 8;
                            break;
                        case 3:
                            strPause = StrNarrAdd(pArrLarge[i], pArrSmall[i]);
                            pArrLarge[i] = pArrLarge[i] - pArrSmall[i] - 7;
                            break;
                        case 4:
                            strPause = StrNarrAdd(pArrLarge[i], pArrSmall[i]);
                            pArrLarge[i] = pArrLarge[i] - pArrSmall[i] - 6;
                            break;
                        case 5:
                            strPause = StrNarrAdd(pArrLarge[i], pArrSmall[i]);
                            pArrLarge[i] = pArrLarge[i] - pArrSmall[i] - 5;
                            break;
                        case 6:
                            strPause = StrNarrAdd(pArrLarge[i], pArrSmall[i]);
                            pArrLarge[i] = pArrLarge[i] - pArrSmall[i] - 4;
                            break;
                        case 7:
                            strPause = StrNarrAdd(pArrLarge[i], pArrSmall[i]);
                            pArrLarge[i] = pArrLarge[i] - pArrSmall[i] - 3;
                            break;
                        case 8:
                            strPause = StrNarrAdd(pArrLarge[i], pArrSmall[i]);
                            pArrLarge[i] = pArrLarge[i] - pArrSmall[i] - 2;
                            break;
                        case 9:
                            strPause = StrNarrAdd(pArrLarge[i], pArrSmall[i]);
                            pArrLarge[i] = pArrLarge[i] - pArrSmall[i] - 1;
                            break;
                    }
                    //Carry the 10
                    pArrLarge[i - 1] = pArrLarge[i - 1] + 1;
                }

                // Similar to first bead carry check above. 
                // Looped to work back across all columns to the left.
                if (i > 0) // (i>0) stops out-of-bounds exception when i=0, ie first iteration of loop.
                {
                    for (int j = 1; j < 11; j++)
                    {
                        if (pArrLarge[i - j] > 9)
                        {
                            strPause = strPause + " ...adding 1 to the left column has made that column greater than 9,\n"
                                                + " ...so we need to add another 1 to the next left column\n";
                            pArrLarge[i - j] = 0;
                            pArrLarge[i - (j + 1)] = pArrLarge[i - (j + 1)] + 1;
                            //then need to keep checking to the left, all columns
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                
                // Show each iteration, right to left.
                if (boolSmallIsZero == false)
                {
                    //StorageFile file = await fp.PickSingleFileAsync();
                    DisplayTextBox.Text += ($"\nAdd the " + pArrSmall[i] + " on the #" + (12 - i) + " column");
                    DisplayTextBox.Text += strPause;
                    //_ = Console.Read().ToString();// need some sort of pause here, or better, for the user to click something to continue.
                    ShowValueOnSoroban(ArrayToLong(pArrLarge)); 
                }
            }
            DisplayTextBox.Text += ($"Final answer: " + ArrayToLong(pArrLarge));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Variables
            long longAdd1 = 0, longAdd2 = 0;
            

            //Gather inputs
            longAdd1 = long.Parse(InputNumber1.Text);
            longAdd2 = long.Parse(InputNumber2.Text);

            //Determine the larger input value
            DisplayTextBox.Text = "";
            DisplayTextBox.Text = Math.Max(longAdd1, longAdd2) + " is the larger number. We will display this first.";
            ShowValueOnSoroban(Math.Max(longAdd1, longAdd2));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //Variables
            long longAdd1 = 0, longAdd2 = 0;

            //Gather inputs
            longAdd1 = long.Parse(InputNumber1.Text);
            longAdd2 = long.Parse(InputNumber2.Text);

            //Determine the larger input value
            DisplayTextBox.Text = "";
            DisplayTextBox.Text = Math.Max(longAdd1, longAdd2) + " is the larger number. We display this first, and add the smaller number to it.";

            AddTwoLongs(Math.Max(longAdd1, longAdd2), Math.Min(longAdd1, longAdd2));
        }
    }

}
