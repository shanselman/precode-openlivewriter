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
using System.Windows.Controls.Primitives;

namespace FiftyEightBits.PreCode
{
    /// <company>FiftyEightBits</company>
    /// <author>Tony</author>
    /// <date>26/07/2007 01:14:11</date>
    /// 
    /// <summary>
    /// ToolBarButton Class with Animated Hover effects
    /// </summary>
    /// 
    /// <modified></modified>

    public class ToolBarToggle : ToggleButton
    {
        public ToolBarToggle()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(ToolBarToggle));

        public ImageSource Source
        {
            get { return base.GetValue(SourceProperty) as ImageSource; }
            set { base.SetValue(SourceProperty, value); }
        }


        public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.Register("ImageWidth", typeof(double), typeof(ToolBarToggle));

        public double ImageWidth
        {
            get { return (double)base.GetValue(ImageWidthProperty); }
            set { base.SetValue(ImageWidthProperty, value); }
        }

        public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register("ImageHeight", typeof(double), typeof(ToolBarToggle));

        public double ImageHeight
        {
            get { return (double)base.GetValue(ImageHeightProperty); }
            set { base.SetValue(ImageHeightProperty, value); }
        }


        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(String), typeof(ToolBarToggle));

        public string Text
        {
            get { return base.GetValue(TextProperty) as string; }
            set { base.SetValue(TextProperty, value); }
        }
    }
}
