/************************************************************************************  
' Copyright (C) 2009 Anthony Bouch (http://www.58bits.com) under the terms of the
' Microsoft Public License (Ms-PL http://www.codeplex.com/precode/license)
'***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace FiftyEightBits.PreCode
{
    /// <summary>
    /// Interaction logic for UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {

        private List<UpdateableComponent> _updateableComponents;
        public List<UpdateableComponent> UpdateableComponents
        {
            get
            {
                if (_updateableComponents == null)
                {
                    _updateableComponents = new List<UpdateableComponent>();
                }
                return _updateableComponents;
            }
        }

        public UpdateProcessor Processor { get; protected set; }


        public UpdateWindow()
        {
            InitializeComponent();

            var assembly = Assembly.GetAssembly(GetType());
            var updateableAttribute =
                        assembly.GetCustomAttributes(typeof(UpdateableComponentAttribute), false) as UpdateableComponentAttribute[];
            if (updateableAttribute != null && updateableAttribute.Length > 0)
            {
                Trace.WriteLine(String.Format("{0} has update capabilities.", assembly.GetName().Name));
                UpdateableComponents.Add(new UpdateableComponent(assembly, updateableAttribute[0].UpdateInfoUrl));
            }

            Processor = new UpdateProcessor();
            Processor.UpdateComponentCompleted += Processor_UpdateComponentCompleted;
            WriteSatus("Checking for update...");
            foreach (UpdateableComponent component in UpdateableComponents)
            {
                Processor.UpdateComponentAsync(component, null);
            }
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

        /// <summary>
        /// Call back method for update check.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Processor_UpdateComponentCompleted(object sender, UpdateComponentCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                WriteSatus(String.Format("There was a problem finding update information. {0}. Please try again later, or visit http://www.codeplex.com/precode.", e.Error.Message));
            }
            else
            {
                if (e.Component.NeedsUpdate(e.UpdateInfo))
                {
                    WriteSatusFormat("A new version of the PreCode Code Snippet Plugin is available.{0}", Environment.NewLine);
                    WriteSatusFormat("The new version is {0}, and contains the following changes: {1}{2}", e.UpdateInfo.Version, e.UpdateInfo.Description, Environment.NewLine);
                    WriteSatusFormat("This updated is available for download at {0}", e.UpdateInfo.UpdateUrl);
                    WriteSatusFormat("You can find out more about this update by visiting {0}", e.UpdateInfo.UpdateArticleUrl);
                }
                else
                {
                    WriteSatus("You are using the latest version of PreCode Code Snippet Plugin for Windows Live Writer.");
                }
            }
        }

        void WriteSatusFormat(string message, params object[] args)
        {
            TextBox_UpdateStatus.Text += (String.Format(message, args) + Environment.NewLine);
        }

        void WriteSatus(string message)
        {
            TextBox_UpdateStatus.Text += (message + Environment.NewLine);
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
