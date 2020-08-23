﻿using System;
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

                //This needs to be changed to toggle the elipse objects visibility
                //Just start with a single digit number first, eg.7

                Ellipse H1, H2, E1, E2, E3, E4, E5;
                H1 = (Ellipse)this.FindName("RH1C1");
                H2 = (Ellipse)this.FindName("RH2C1");
                E1 = (Ellipse)this.FindName("R1C1");
                E2 = (Ellipse)this.FindName("R2C1");
                E3 = (Ellipse)this.FindName("R3C1");
                E4 = (Ellipse)this.FindName("R4C1");
                E5 = (Ellipse)this.FindName("R5C1");

                // Heaven beads
                if (pValue >= 5)
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
                switch (pValue)
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
