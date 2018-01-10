/************************************************************************************  
' Copyright (C) 2009 Anthony Bouch (http://www.58bits.com) under the terms of the
' Microsoft Public License (Ms-PL http://www.codeplex.com/precode/license)
'***********************************************************************************/
using System;
using System.Windows;
using FiftyEightBits.PreCode.Properties;

namespace FiftyEightBits.PreCode
{
    public class Program : Application
    {
        [STAThread]
        public static void Main()
        {
            var app = new Program();
            app.Run();
        }


        protected override void OnStartup(StartupEventArgs args)
        {
            base.OnStartup(args);

            var window = new PreCodeWindow(new PreCodeSettings(), PreCodeWindow.Mode.StandAlone);
            window.ShowDialog();
            if (window.DialogResult.HasValue && window.DialogResult.Value)
            {
                System.Diagnostics.Debug.WriteLine("OK");
                Clipboard.SetText(window.Code);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Not OK");
            }

            AppSettings.Default.Save();
        }
    }
}
