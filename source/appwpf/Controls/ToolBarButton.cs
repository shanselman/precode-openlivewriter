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
    /// <date>26/07/2007 01:14:11</date>
    /// 
    /// <summary>
    /// ToolBarButton Class with Animated Hover effects
    /// </summary>
    /// 
    /// <modified></modified>

    public class ToolBarButton : Button
    {
        public ToolBarButton()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(ToolBarButton));

        public ImageSource Source
        {
            get { return base.GetValue(SourceProperty) as ImageSource; }
            set { base.SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(String), typeof(ToolBarButton));

        public string Text
        {
            get { return base.GetValue(TextProperty) as string; }
            set { base.SetValue(TextProperty, value); }
        }


        public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.Register("ImageWidth", typeof(double), typeof(ToolBarButton));

        public double ImageWidth
        {
            get { return (double)base.GetValue(ImageWidthProperty); }
            set { base.SetValue(ImageWidthProperty, value); }
        }

        public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register("ImageHeight", typeof(double), typeof(ToolBarButton));

        public double ImageHeight
        {
            get { return (double)base.GetValue(ImageHeightProperty); }
            set { base.SetValue(ImageHeightProperty, value); }
        }
    }
}
