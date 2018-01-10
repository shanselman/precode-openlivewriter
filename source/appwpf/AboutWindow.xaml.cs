/************************************************************************************  
' Copyright (C) 2009 Anthony Bouch (http://www.58bits.com) under the terms of the
' Microsoft Public License (Ms-PL http://www.codeplex.com/precode/license)
'***********************************************************************************/
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace FiftyEightBits.PreCode
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {

        /// <summary>
        /// DependencyProperty for bindable Version
        /// </summary>
        public static readonly DependencyProperty VersionProperty =
        DependencyProperty.Register("Version",
                                        typeof(string),
                                        typeof(AboutWindow),
                                        new FrameworkPropertyMetadata(""));

        /// <summary>
        /// Version
        /// </summary>
        public string Version
        {
            get { return (string)this.GetValue(AboutWindow.VersionProperty); }
            set { this.SetValue(AboutWindow.VersionProperty, value); }
        }


        /// <summary>
        /// AboutWindow
        /// </summary>
        public AboutWindow()
        {
            InitializeComponent();
            
            var version = Assembly.GetAssembly(GetType()).GetName().Version;
            if (version != null)
                Version = String.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Build);
        }

        /// <summary>
        /// Enable drag for the Window (from locations other than the menu or borders). Useful
        /// for borderless and custom shape windows.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs args)
        {
            base.OnMouseLeftButtonDown(args);

            DragMove();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            try
            {
                Process.Start(e.Uri.ToString());
            }
            catch { }
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
