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
            DisplayTextBox.Text =  Narratives.GetXMLNarrs("//All_Text_Strings/Main_Text_strings/Introduction/comment1");
            DisplayTextBox.Text += Narratives.GetXMLNarrs("//All_Text_Strings/Main_Text_strings/Introduction/More_info");
        }

        public static string StrNarrAdd(int intLarge, int intSmall)
        {
            string strTemp;
            strTemp = " (can't add " + intSmall + " to " + Convert.ToString(intLarge - intSmall);
            strTemp += " so minus " + (10 - intSmall) + " and add 1 to the left column)\n";
            return strTemp;
        }

        private static string StrNarrSubtract(int intLarge, int intSmall)
        {
            string strTemp;
            strTemp = " - can't subtract " + intSmall + " from " + Convert.ToString(intLarge + intSmall);
            strTemp += ", so add " + (10 - intSmall) + " and minus 1 from the left column. ";
            return strTemp;
        }

        public static long ArrayToLong(int[] pArrInts)
        {
            string strCombined = "";
            for (int i = 0; i < 17; i++)
            {
                strCombined += pArrInts[i];
            }
            return long.Parse(strCombined);
        }

        public void ShowValueOnSoroban(long pValue)
        {
            try
            {
                if (pValue > 99999999999999999)
                {
                    throw new FormatException("Not enough columns to display answer. ");
                }
                //declare variables
                string strMaxInput = pValue.ToString();
                int[] arrNumsLarge = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                Ellipse H1, H2, E1, E2, E3, E4, E5;
                TextBlock TB;

                // Put each digit of pValue into the arrNumsLarge array.
                // The array represents each column of the soroban.
                for (int i = 0; i < strMaxInput.Length; i++)
                {
                    arrNumsLarge[i + (17 - strMaxInput.Length)] = int.Parse(strMaxInput.Substring(i, 1));
                }

                //Loop through each column, left to left

                for (int i = 0; i < 17; i++)
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

                    TB = (TextBlock)this.FindName("LC" + i.ToString());

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
                    //Display the number below the earth beads
                    TB.Text = arrNumsLarge[i].ToString();
                }//loop ends
            }
            catch (FormatException fEx)
            {
                DisplayTextBox.Text = "ShowValueOnSoroban error: " + fEx.Message + pValue.ToString();
                throw new Exception("Deliberate exception thrown");
            }
        }

        //Read an integer into an array, eg. 12345 --> {0,0,0,0,0,0,0,0,0,0,0,0,1,2,3,4,5}
        public static int[] LongToArray(long pValue)
        {
            string strMaxInput = pValue.ToString();
            int[] arrNumsLarge = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            // Put each digit of intMaxInput into the arrNumsLarge array.
            // The array represents each column of the soroban.
            for (int i = 0; i < strMaxInput.Length; i++)
            {
                arrNumsLarge[i + (17 - strMaxInput.Length)] = int.Parse(strMaxInput.Substring(i, 1));
            }
            return arrNumsLarge;
        }

        public async Task AddTwoLongsAsync(long longLarge, long longSmall) 
        {
            string strPause = "";
            int[] pArrLarge = LongToArray(longLarge);
            int[] pArrSmall = LongToArray(longSmall);
            ContinueButton.Visibility = Visibility.Visible;

            ShowValueOnSoroban(longLarge);

            // try to wait for the button click
            clickWaitTask = new TaskCompletionSource<bool>();
            await clickWaitTask.Task;

            for (int i = 0; i < 17; i++)
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
                    for (int j = 1; j < 16; j++)
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
                    DisplayTextBox.Text += ($"\nAdd the " + pArrSmall[i] + " on the #" + (17 - i) + " column");
                    DisplayTextBox.Text += strPause;
                    strPause = ""; // added to fix bug, caused incorrectly messages to appear.

                    // try to wait for the button click
                    clickWaitTask = new TaskCompletionSource<bool>();
                    await clickWaitTask.Task;
                    ShowValueOnSoroban(ArrayToLong(pArrLarge)); 
                }
            }
            DisplayTextBox.Text += ($"\nFinal answer: " + ArrayToLong(pArrLarge));
            ContinueButton.Visibility = Visibility.Collapsed;
        }

        public async Task SubtractTwoLongs(long longLarge, long longSmall, bool isNegative)
        {
            string strPause = "";
            int[] pArrLarge = LongToArray(longLarge);
            int[] pArrSmall = LongToArray(longSmall);
            ContinueButton.Visibility = Visibility.Visible;

            ShowValueOnSoroban(longLarge);

            // try to wait for the button click
            clickWaitTask = new TaskCompletionSource<bool>();
            await clickWaitTask.Task;

            for (int i = 0; i < 17; i++)
            {
                //whether or not to display this calculation
                bool boolSmallIsZero = (pArrSmall[i] == 0);

                //main logic
                pArrLarge[i] = pArrLarge[i] - pArrSmall[i];

                //Carry left bead check
                if (pArrLarge[i] < 0)
                {
                    switch (pArrSmall[i])
                    {
                        case 1:
                            strPause = StrNarrSubtract(pArrLarge[i], pArrSmall[i]);
                            pArrLarge[i] = pArrLarge[i] + pArrSmall[i] + 9;
                            break;
                        case 2:
                            strPause = StrNarrSubtract(pArrLarge[i], pArrSmall[i]);
                            pArrLarge[i] = pArrLarge[i] + pArrSmall[i] + 8;
                            break;
                        case 3:
                            strPause = StrNarrSubtract(pArrLarge[i], pArrSmall[i]);
                            pArrLarge[i] = pArrLarge[i] + pArrSmall[i] + 7;
                            break;
                        case 4:
                            strPause = StrNarrSubtract(pArrLarge[i], pArrSmall[i]);
                            pArrLarge[i] = pArrLarge[i] + pArrSmall[i] + 6;
                            break;
                        case 5:
                            strPause = StrNarrSubtract(pArrLarge[i], pArrSmall[i]);
                            pArrLarge[i] = pArrLarge[i] + pArrSmall[i] + 5;
                            break;
                        case 6:
                            strPause = StrNarrSubtract(pArrLarge[i], pArrSmall[i]);
                            pArrLarge[i] = pArrLarge[i] + pArrSmall[i] + 4;
                            break;
                        case 7:
                            strPause = StrNarrSubtract(pArrLarge[i], pArrSmall[i]);
                            pArrLarge[i] = pArrLarge[i] + pArrSmall[i] + 3;
                            break;
                        case 8:
                            strPause = StrNarrSubtract(pArrLarge[i], pArrSmall[i]);
                            pArrLarge[i] = pArrLarge[i] + pArrSmall[i] + 2;
                            break;
                        case 9:
                            strPause = StrNarrSubtract(pArrLarge[i], pArrSmall[i]);
                            pArrLarge[i] = pArrLarge[i] + pArrSmall[i] + 1;
                            break;
                    }
                    //Carry the 10
                    pArrLarge[i - 1] = pArrLarge[i - 1] - 1;
                }

                // Similar to first bead carry check above. 
                // Looped to work back across all columns to the left.
                if (i > 0) // (i>0) stops out-of-bounds exception when i=0, ie first iteration of loop.
                {
                    for (int j = 1; j < 16; j++)
                    {
                        if (pArrLarge[i - j] < 0)
                        {
                            strPause = strPause + "...subtracting 1 from the left column has made that column less than 0, "
                                                + "so we need to subtract another 1 from the next left column";
                            pArrLarge[i - j] = 9;
                            pArrLarge[i - (j + 1)] = pArrLarge[i - (j + 1)] - 1;
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
                    DisplayTextBox.Text += ($" \n...Minus the " + pArrSmall[i] + " from the #" + (17 - i) + " column");
                    DisplayTextBox.Text += (strPause);
                    strPause = ""; //fixed bug

                    // try to wait for the button click
                    clickWaitTask = new TaskCompletionSource<bool>();
                    await clickWaitTask.Task;

                    ShowValueOnSoroban(ArrayToLong(pArrLarge));
                }
            }
            DisplayTextBox.Text += ($"\nFinal answer: "  + (isNegative ? "-" : "+") + ArrayToLong(pArrLarge)); //notice the shorthand if-then-else on isNegative
            ContinueButton.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Variables
                long longAdd1 = 0;

                //Gather inputs
                longAdd1 = long.Parse(InputNumber1.Text);
                
                if (longAdd1 < 0 )
                {
                    throw new FormatException("Input less than zero");
                }
                if (longAdd1 > 99999999999999999 )
                {
                    throw new FormatException("Input exceeds Soroban maximum value");
                }
                //Determine the larger input value
                DisplayTextBox.Text += "\n";
                DisplayTextBox.Text += longAdd1 + " is displayed above.";
                ShowValueOnSoroban(longAdd1);

            }
            catch(FormatException fEx)
            {
                DisplayTextBox.Text = "Error: " + fEx.Message;
            }
            catch (Exception)
            {
                DisplayTextBox.Text = "Input needs to be an integer between zero and 999999999999\n";
            }
        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            //declare and Gather inputs
            try
            {
                long longAdd1 = long.Parse(InputNumber1.Text);
                long longAdd2 = long.Parse(InputNumber2.Text);

                if (longAdd1 < 0 || longAdd2 < 0)
                {
                    throw new FormatException("Input less than zero");
                }
                if (longAdd1 > 99999999999999999 || longAdd2 > 99999999999999999)
                {
                    throw new FormatException("Input exceeds Soroban maximum value");
                }

                //Determine the larger input value
                ShowValueOnSoroban(0);
                DisplayTextBox.Text = "";
                string introString = Math.Max(longAdd1, longAdd2) + " is the larger number. We display this first, and add the smaller number to it.";
                introString += "\nWe add by moving from left to right across the columns of the abacus.\n";
                introString += Narratives.GetXMLNarrs("//All_Text_Strings/Main_Text_strings/Addition/Comment1");

                ContentDialog msgLargest = new ContentDialog()
                {
                    Title = "Demonstration",
			        Content = introString,
			        CloseButtonText = "Ok"
                };
                await msgLargest.ShowAsync();

                DisplayTextBox.Text = Math.Max(longAdd1, longAdd2).ToString() + " is displayed on the Soroban above.";
                AddTwoLongsAsync(Math.Max(longAdd1, longAdd2), Math.Min(longAdd1, longAdd2));
            }
            catch (FormatException fEx)
            {
                DisplayTextBox.Text = "Error: " + fEx.Message;
            }
            catch (Exception)
            {
                DisplayTextBox.Text = "Input needs to be an integer between zero and 99999999999999999\n";
            }
        }

        private async void Subtract_Click(object sender, RoutedEventArgs e)
        {
            //declare and Gather inputs
            try
            {
                long longSubtract1 = long.Parse(InputNumber1.Text);
                long longSubtract2 = long.Parse(InputNumber2.Text);

                if (longSubtract1 < 0 || longSubtract2 < 0)
                {
                    throw new FormatException("Input less than zero");
                }
                if (longSubtract1 > 99999999999999999 || longSubtract2 > 99999999999999999)
                {
                    throw new FormatException("Input exceeds Soroban maximum value");
                }

                //Determine the larger input value
                ShowValueOnSoroban(0);
                DisplayTextBox.Text = "";
                string introString = Narratives.GetXMLNarrs("//All_Text_Strings/Main_Text_strings/Subtraction/Comment2");
                introString += Narratives.GetXMLNarrs("//All_Text_Strings/Main_Text_strings/Subtraction/Comment1");

                ContentDialog msgLargest = new ContentDialog() 
                {
                    Title = "Demonstration",
                    Content = introString,
                    CloseButtonText = "Ok"
                };
                await msgLargest.ShowAsync();

                DisplayTextBox.Text = Math.Max(longSubtract1, longSubtract2).ToString() + " is displayed on the Soroban above.";
                SubtractTwoLongs(Math.Max(longSubtract1, longSubtract2), Math.Min(longSubtract1, longSubtract2), (longSubtract1 < longSubtract2));
                
            }
            catch (FormatException fEx)
            {
                DisplayTextBox.Text = "Error: " + fEx.Message;
            }
            catch (Exception)
            {
                DisplayTextBox.Text = "Input needs to be an integer between zero and 99999999999999999\n";
            }
        }

        private void Count_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Variables
                long longAdd1 = 0;

                //Gather inputs
                longAdd1 = long.Parse(InputNumber1.Text);

                if (longAdd1 < 0)
                {
                    throw new FormatException("Input less than zero");
                }
                if (longAdd1 > 99999999999999999)
                {
                    throw new FormatException("Input exceeds Soroban maximum value");
                }
                longAdd1++;
                InputNumber1.Text = longAdd1.ToString();
                ShowValueOnSoroban(longAdd1);

            }
            catch (FormatException fEx)
            {
                DisplayTextBox.Text = "Error: " + fEx.Message;
            }
            catch (Exception)
            {
                DisplayTextBox.Text = "Input needs to be an integer between zero and 99999999999999999\n";
            }
        }

        public static decimal dcl10ToPower(int pPower)
        {
            //var v1 = Math.Pow(10, pPower);
            decimal result = (decimal)Math.Pow(10, pPower);
            return result;
        }

        public async Task MultiplyTwoLongs(long multiplicand, long multiplier, bool showPopUps)
        {
            int multiplicandStartPos = (multiplier.ToString() + multiplicand.ToString()).Length;
            ContinueButton.Visibility = Visibility.Visible;
            if (showPopUps)
            {
                ContentDialog msgLargest = new ContentDialog()
                {
                    Title = "Demonstration",
                    Content = Narratives.GetXMLNarrs("//All_Text_Strings/Soroban_Text_Strings/MultiplyTwoLongs/Comment1"),
                    CloseButtonText = "Ok"
                };
                await msgLargest.ShowAsync(); 
            }

            DisplayTextBox.Text = "In our example " + multiplier + " goes to the left and " + multiplicand + " starts " + multiplicandStartPos
                                + " to the left of the rightmost bar. To begin with it looks like as above.\n";

            long initialDisplay = long.Parse(multiplier.ToString().PadRight((16 - multiplicandStartPos), '0') + multiplicand.ToString().PadRight((1 + multiplicandStartPos), '0'));
            ShowValueOnSoroban(initialDisplay);

            int intLengthOfMultiplier = multiplier.ToString().Length;
            decimal dclToDisplay = (decimal)initialDisplay;
            long lngToDisplay = 0;
            decimal dclShifter = dcl10ToPower(intLengthOfMultiplier - 2);
            DisplayTextBox.Text += Narratives.GetXMLNarrs("//All_Text_Strings/Soroban_Text_Strings/MultiplyTwoLongs/Comment2");
            int targetColumn = intLengthOfMultiplier + 1;

            //main logic, loop - backwards through each digit of multiplicand
            //uses doubles rather than longs because at some points we have to mutiply results by 0.1
            int t = 1;  // t gets bigger, while i below gets smaller
            for (int i = multiplicand.ToString().Length; i > 0; i--)
            {
                //single digit of the multipliand ***
                decimal dclMultiplicandDigit = decimal.Parse((multiplicand.ToString()).Substring((i - 1), 1));

                //2nd loop - forwards through each digit of the multiplier 
                for (int j = 0; j < multiplier.ToString().Length; j++)
                {
                    //single digit of the multiplier ***
                    decimal dclMultiplerDigit = decimal.Parse(multiplier.ToString().Substring(j, 1));
                    decimal dclTens = dcl10ToPower(t - j);
                    decimal dclProduct = dclMultiplerDigit * dclMultiplicandDigit;
                    DisplayTextBox.Text += " ...add " + dclProduct.ToString().PadLeft(2, '0') + " (" + dclMultiplerDigit + " x " + dclMultiplicandDigit
                                        + ") to the columns #" + targetColumn + " and #" + (targetColumn - 1) + ",";

                    if (showPopUps)
                    {
                        // try to wait for the button click
                        clickWaitTask = new TaskCompletionSource<bool>();
                        await clickWaitTask.Task;
                    }

                    //adding the product back into the displayed number on Soroban
                    dclToDisplay += dclProduct * dclTens * dclShifter;
                    lngToDisplay = (long)dclToDisplay;
                    ShowValueOnSoroban(lngToDisplay);
                    targetColumn--;
                }
                DisplayTextBox.Text += "\nClear the end of multiplicand (" + dclMultiplicandDigit + ")... \n";
                targetColumn = intLengthOfMultiplier + 1 + t;
                dclToDisplay -= (dclMultiplicandDigit * dcl10ToPower(t + 2) * dclShifter);
                if (showPopUps)
                {
                    // try to wait for the button click
                    clickWaitTask = new TaskCompletionSource<bool>();
                    await clickWaitTask.Task;
                }

                lngToDisplay = (long)dclToDisplay;
                ShowValueOnSoroban(lngToDisplay);
                t++;
            }
            if (showPopUps)
            {
                ContentDialog msgEnd = new ContentDialog()
                {
                    Title = "Finished",
                    Content = "All individual products are now summed together.",
                    CloseButtonText = "Ok"
                };
                await msgEnd.ShowAsync(); 
            }
            //deduct the multipler from far left of the Soroban display
            ShowValueOnSoroban(lngToDisplay-(long.Parse(multiplier.ToString().PadRight(17,'0'))));
            DisplayTextBox.Text += "\nRemove multiplier, leaves final result: " + (multiplier * multiplicand);
            ContinueButton.Visibility = Visibility.Collapsed;
        }
        private async void Multiply_Click(object sender, RoutedEventArgs e)
        {
            //declare and Gather inputs
            try
            {
                long longAdd1 = long.Parse(InputNumber1.Text);
                long longAdd2 = long.Parse(InputNumber2.Text);

                if (longAdd1 < 0 || longAdd2 < 0)
                {
                    throw new FormatException("Input less than zero");
                }
                if (longAdd1 > 99999999999999999 || longAdd2 > 99999999999999999)
                {
                    throw new FormatException("Input exceeds Soroban maximum value");
                }
                if (longAdd1 * longAdd2 > 99999999999999999)
                {
                    throw new FormatException("Not enough columns to display answer");
                }
                if ((InputNumber1.Text.Length + 1 + (2 * InputNumber2.Text.Length) > 17) && (InputNumber2.Text.Length > InputNumber1.Text.Length))
                {
                    throw new FormatException("Not enough columns to calculate answer. Try switching #1 and #2 around.");
                }
                if (InputNumber1.Text.Length + 1 + (2 * InputNumber2.Text.Length) > 17)
                {
                    throw new FormatException("Not enough columns to calculate answer");
                }
                //Determine the larger input value
                ShowValueOnSoroban(0);
                DisplayTextBox.Text = "";
                string introString = Narratives.GetXMLNarrs("//All_Text_Strings/Main_Text_strings/Multiplication/Introduction");
                introString += Narratives.GetXMLNarrs("//All_Text_Strings/Main_Text_strings/Multiplication/Terminology");

                ContentDialog msgLargest = new ContentDialog()
                {
                    Title = "Demonstration",
                    Content = introString,
                    CloseButtonText = "Ok"
                };
                await msgLargest.ShowAsync();
                MultiplyTwoLongs(longAdd1, longAdd2, true);
            }
            catch (FormatException fEx)
            {
                DisplayTextBox.Text = "Error: " + fEx.Message;
            }
            //catch (Exception)
            //{
            //    DisplayTextBox.Text = "Input needs to be an integer between zero and 999999999999\n";
            //}

        }

        private TaskCompletionSource<bool> clickWaitTask;

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            clickWaitTask.TrySetResult(true);
        }
    }

}
