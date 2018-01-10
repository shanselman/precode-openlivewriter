/************************************************************************************  
' Copyright (C) 2009 Anthony Bouch (http://www.58bits.com) under the terms of the
' Microsoft Public License (Ms-PL http://www.codeplex.com/precode/license)
'***********************************************************************************/
using System;
using System.Windows.Forms;
using WindowsLive.Writer.Api;
using MessageBox=System.Windows.MessageBox;

namespace FiftyEightBits.PreCode
{

    [WriterPlugin("BA2399CE-F110-4df5-B6D5-7C034A84B31D", "PreCode Snippet", PublisherUrl = "http://www.58bits.com", Description = "Simple plugin for creating pre, textarea, blockquote code blocks for Windows Live Writer with support for Syntaxhighlighter.", ImagePath = "PreCodeForWLW.ico")]
    [InsertableContentSource("PreCode Snippet", SidebarText="PreCode Snippet")]
    public class PreCodePlugin : ContentSource
    {
        PreCodeLiveWriterSettings settings;
        
        public override void Initialize(IProperties pluginOptions)
        {
            base.Initialize(pluginOptions);
            settings = new PreCodeLiveWriterSettings(pluginOptions);
        }

        public override DialogResult CreateContent(System.Windows.Forms.IWin32Window dialogOwner, ref string content)
        {
            try
            {
                var window = new PreCodeWindow(settings, PreCodeWindow.Mode.WLW) {ShowInTaskbar = false};
                window.ShowDialog();
                if (window.DialogResult.HasValue && window.DialogResult.Value)
                {
                    content = window.Code;
                }

                return DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("An error occured in PreCodeWindow: {0}", ex.Message));
                return DialogResult.Cancel;
            }
        }
    }    
}
