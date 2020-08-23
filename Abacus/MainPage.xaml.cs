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

                //This needs to be changed to toggle the elipse objects visibility
                //Just start with a single digit number first, eg.7
                
                // Heaven beads
                if (pValue >= 5)
                {
                    RH2C1.Visibility = Visibility.Visible;
                    RH1C1.Visibility = Visibility.Collapsed;
                }
                else
                {
                    RH2C1.Visibility = Visibility.Collapsed;
                    RH1C1.Visibility = Visibility.Visible;
                }
                // Earth beads
                switch (pValue)
                {
                    case 0:
                    case 5:
                        R1C1.Visibility = Visibility.Visible;
                        R2C1.Visibility = Visibility.Visible;
                        R3C1.Visibility = Visibility.Visible;
                        R4C1.Visibility = Visibility.Visible;
                        R5C1.Visibility = Visibility.Collapsed;
                        break;
                    case 1:
                    case 6:
                        R1C1.Visibility = Visibility.Visible;
                        R2C1.Visibility = Visibility.Visible;
                        R3C1.Visibility = Visibility.Visible;
                        R4C1.Visibility = Visibility.Collapsed;
                        R5C1.Visibility = Visibility.Visible;
                        break;
                    case 2:
                    case 7:
                        R1C1.Visibility = Visibility.Visible;
                        R2C1.Visibility = Visibility.Visible;
                        R3C1.Visibility = Visibility.Collapsed;
                        R4C1.Visibility = Visibility.Visible;
                        R5C1.Visibility = Visibility.Visible;
                        break;
                    case 3:
                    case 8:
                        R1C1.Visibility = Visibility.Visible;
                        R2C1.Visibility = Visibility.Collapsed;
                        R3C1.Visibility = Visibility.Visible;
                        R4C1.Visibility = Visibility.Visible;
                        R5C1.Visibility = Visibility.Visible;
                        break;
                    case 4:
                    case 9:
                        R1C1.Visibility = Visibility.Collapsed;
                        R2C1.Visibility = Visibility.Visible;
                        R3C1.Visibility = Visibility.Visible;
                        R4C1.Visibility = Visibility.Visible;
                        R5C1.Visibility = Visibility.Visible;
                        break;
                    default:
                        R1C1.Visibility = Visibility.Collapsed;
                        R2C1.Visibility = Visibility.Collapsed;
                        R3C1.Visibility = Visibility.Collapsed;
                        R4C1.Visibility = Visibility.Collapsed;
                        R5C1.Visibility = Visibility.Collapsed;
                        break;
                }

            }

            //Gather inputs
            longAdd1 = long.Parse(InputNumber1.Text);
            longAdd2 = long.Parse(InputNumber2.Text);

            //Determine the larger input value
            DisplayTextBox.Text = "";
            DisplayTextBox.Text = Math.Max(longAdd1, longAdd2) + " is the larger number. We will display this first.";
            ShowValueOnSoroban(Math.Max(longAdd1, longAdd2));

            /*
            if (R1C1.Visibility == Visibility.Visible)
            {
                R1C1.Visibility = Visibility.Collapsed;
            }
            else
            {
                R1C1.Visibility = Visibility.Visible;
            }
            */
        }
    }

}
