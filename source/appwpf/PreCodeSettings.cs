/************************************************************************************  
' Copyright (C) 2009 Anthony Bouch (http://www.58bits.com) under the terms of the
' Microsoft Public License (Ms-PL http://www.codeplex.com/precode/license)
'***********************************************************************************/
using FiftyEightBits.PreCode.Properties;
using WindowsLive.Writer.Api;

namespace FiftyEightBits.PreCode
{
    public class PreCodeSettings : IPreCodeSettings
    {
        public PreCodeSettings()
        {

        }

        #region IPreCodeSettings Members

        public bool ShowOptions
        {
            get { return AppSettings.Default.ShowOptions; }
            set { AppSettings.Default.ShowOptions = value; }
        }

        public int SurroundWith
        {
            get { return AppSettings.Default.SurroundWith; }
            set { AppSettings.Default.SurroundWith = value; }
        }

        public bool HtmlEncode
        {
            get { return AppSettings.Default.HtmlEncode; }
            set { AppSettings.Default.HtmlEncode = value; }
        }

        public bool UseBrs
        {
            get { return AppSettings.Default.UseBrs; }
            set { AppSettings.Default.UseBrs = value; }
        }

        public string SyntaxClassAttribute
        {
            get { return AppSettings.Default.SyntaxClassAttribute; }
            set { AppSettings.Default.SyntaxClassAttribute = value; }
        }

        public bool DoNotDisplayGutter
        {
            get { return AppSettings.Default.DoNotDisplayGutter; }
            set { AppSettings.Default.DoNotDisplayGutter = value; }
        }
    
        public bool CollapseBlock
        {
            get { return AppSettings.Default.CollapseBlock; }
            set { AppSettings.Default.CollapseBlock = value; }
        }
       
        public bool ShowRuler
        {
            get { return AppSettings.Default.ShowRuler; }
            set { AppSettings.Default.ShowRuler = value; }
        }
        public int LineCountStart
        {
            get { return AppSettings.Default.LineCountStart; }
            set { AppSettings.Default.LineCountStart = value; }
        }

        public bool DoNotShowToolBar
        {
            get { return AppSettings.Default.DoNotShowToolBar; }
            set { AppSettings.Default.DoNotShowToolBar = value; }
        }

        public bool DoNotAutoLink
        {
            get { return AppSettings.Default.DoNotAutoLink; }
            set { AppSettings.Default.DoNotAutoLink = value; }
        }

     public bool DoNotUseSmartTabs
        {
            get { return AppSettings.Default.DoNotUseSmartTabs; }
            set { AppSettings.Default.DoNotUseSmartTabs = value; }
        }

        #endregion
    }
}
