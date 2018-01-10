
using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace FiftyEightBits.PreCode
{
    /// <company>FiftyEightBits</company>
    /// <author>Tony</author>
    /// <date>05/006/2008 01:14:11</date>
    /// 
    /// <summary>
    /// NumberBox
    /// </summary>
    /// 
    /// <modified></modified>

    public class NumberBox : TextBox
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public NumberBox()
        {
            this.TextChanged += new TextChangedEventHandler(NumberBox_TextChanged);
            this.Text = this.Min.ToString();
        }               

        public static readonly DependencyProperty MinProperty = DependencyProperty.Register("Min", typeof(int), typeof(NumberBox));

        public int Min
        {
            get { return (int)base.GetValue(MinProperty); }
            set { base.SetValue(MinProperty, value); }
        }

        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register("Max", typeof(int), typeof(NumberBox));

        public int Max
        {
            get { return (int)base.GetValue(MaxProperty); }
            set { base.SetValue(MaxProperty, value); }
        }


        void NumberBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.Text))
            {
                this.Text = this.Min.ToString();
            }
            else
            {
                char[] chars = this.Text.ToCharArray();
                int count = 0;
                for (int x = 0; x < chars.Length; x++)
                {
                    if (Char.IsDigit(chars[x]))
                    {
                        chars[count] = chars[x];
                        count++;
                    }
                }

                //Guaranteed to be a number now - so Int32.Parse is safe.
                String input = new String(chars, 0, count);
                int value = Int32.Parse(input);
                if (value <= this.Min)
                {
                    this.Text = this.Min.ToString();
                }
                else if (value >= this.Max)
                {
                    this.Text = this.Max.ToString();
                }
                else
                {
                    this.Text = value.ToString();
                }
            }
        }
    }
}
