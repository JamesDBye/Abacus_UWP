using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //Variables
            long longAdd1 = 0, longAdd2 = 0;
            
            void ShowValueOnSoroban(long pValue)
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

                //Loop through each column, right to left

                for (int i = 0; i < strMaxInput.Length; i++)
                {
                    //H - Heaven bead, E - Earth bead
                    H1 = (Ellipse)this.FindName("RH2C" + (12-i).ToString());
                    H2 = (Ellipse)this.FindName("RH2C" + (12-i).ToString());
                    E1 = (Ellipse)this.FindName("R1C"  + (12-i).ToString());
                    E2 = (Ellipse)this.FindName("R2C"  + (12-i).ToString());
                    E3 = (Ellipse)this.FindName("R3C"  + (12-i).ToString());
                    E4 = (Ellipse)this.FindName("R4C"  + (12-i).ToString());
                    E5 = (Ellipse)this.FindName("R5C"  + (12-i).ToString());

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
                            E1.Visibility = E2.Visibility = E3.Visibility = E4.Visibility = Visibility.Visible;
                            E5.Visibility = Visibility.Collapsed;
                            break;
                        case 1:
                        case 6:
                            E1.Visibility = E2.Visibility = E3.Visibility = E5.Visibility = Visibility.Visible;
                            E4.Visibility = Visibility.Collapsed;
                            break;
                        case 2:
                        case 7:
                            E1.Visibility = E2.Visibility = E4.Visibility = E5.Visibility = Visibility.Visible;
                            E3.Visibility = Visibility.Collapsed;
                            break;
                        case 3:
                        case 8:
                            E1.Visibility = E3.Visibility = E4.Visibility = E5.Visibility = Visibility.Visible;
                            E2.Visibility = Visibility.Collapsed;
                            break;
                        case 4:
                        case 9:
                            E2.Visibility = E3.Visibility = E4.Visibility = E5.Visibility = Visibility.Visible;
                            E1.Visibility = Visibility.Collapsed;
                            break;
                        default:
                            E1.Visibility = E2.Visibility = E3.Visibility = E4.Visibility = E5.Visibility = Visibility.Collapsed;
                            break;
                    }
                }//loop ends
            }

            //Gather inputs
            longAdd1 = long.Parse(InputNumber1.Text);
            longAdd2 = long.Parse(InputNumber2.Text);

            //Determine the larger input value
            DisplayTextBox.Text = "";
            DisplayTextBox.Text = Math.Max(longAdd1, longAdd2) + " is the larger number. We will display this first.";
            ShowValueOnSoroban(Math.Max(longAdd1, longAdd2));

        }
    }

}
