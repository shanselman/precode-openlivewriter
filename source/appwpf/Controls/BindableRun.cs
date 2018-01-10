using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;

namespace FiftyEightBits.PreCode
{
    public class BindableRun : Run
    {
        
            public static readonly DependencyProperty BoundTextProperty =
              DependencyProperty.Register("BoundText", typeof(string),
              typeof(BindableRun), new PropertyMetadata(OnBoundTextChanged));

            private static void OnBoundTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((Run)d).Text = (string)e.NewValue;
            }

            public String BoundText
            {
                get { return (string)GetValue(BoundTextProperty); }
                set { SetValue(BoundTextProperty, value); }
            }
        
    }
}
