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
            string strResponse;

            void ShowValueOnSoroban(long pValue)
            {
                //declare variables
                string strMaxInput = pValue.ToString();
                int[] arrNumsLarge = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                string strBead;

                // Put each digit of pValue into the arrNumsLarge array.
                // The array represents each column of the soroban.
                for (int i = 0; i < strMaxInput.Length; i++)
                {
                    arrNumsLarge[i + (12 - strMaxInput.Length)] = int.Parse(strMaxInput.Substring(i, 1));
                }

                //Does heaven bead(5) appear in top row
                for (int i = 0; i < arrNumsLarge.Length; i++)
                {
                    // Shorthand IF-THEN-ELSE  = (condition) ? expressionTrue :  expressionFalse;
                    Console.Write($" " + ((arrNumsLarge[i] >= 5) ? "|" : "O"));

                    //This needs to be changed to toggle the elipse objects visibility

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
