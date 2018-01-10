/************************************************************************************  
' Copyright (C) 2009 Anthony Bouch (http://www.58bits.com) under the terms of the
' Microsoft Public License (Ms-PL http://www.codeplex.com/precode/license)
'***********************************************************************************/
using WindowsLive.Writer.Api;

namespace FiftyEightBits.PreCode
{
    public class PreCodeLiveWriterSettings : IPreCodeSettings
    {
        private readonly IProperties settings;

        private const string SHOW_OPTIONS = "SHOW_OPTIONS";
        private const string SURROUND_WITH = "SURROUND_WITH";
        private const string HTML_ENCODE = "HTML_ENCODE";
        private const string USE_BRS = "USE_BRS";
        
        private const string SYNTAX_CLASS_ATTRIBUTE = "SYNTAX_CLASS_ATTRIBUTE";
        private const string DO_NOT_DISPLAY_GUTTER = "DO_NOT_DISPLAY_GUTTER";
        private const string DO_NOT_SHOW_TOOLBAR = "DO_NOT_SHOW_TOOLBAR";
        private const string COLLAPSE_BOCK = "COLLAPSE_BOCK";
        private const string SHOW_RULER = "SHOW_RULER";
        private const string LINECOUNT_START = "LINECOUNT_START";
        private const string DO_NOT_AUTOLINK = "DO_NOT_AUTOLINK";
        private const string DO_NOT_USE_SMARTTABS = "DO_NOT_USE_SMARTTABS";

        public PreCodeLiveWriterSettings(IProperties properties)
        {
            settings = properties;
        }


        public bool ShowOptions
        {
            get { return settings.GetBoolean(SHOW_OPTIONS, true); }
            set { settings.SetBoolean(SHOW_OPTIONS, value); }
        }

        public int SurroundWith
        {
            get { return settings.GetInt(SURROUND_WITH, 0); }
            set { settings.SetInt(SURROUND_WITH, value); }
        }

        public bool HtmlEncode
        {
            get { return settings.GetBoolean(HTML_ENCODE, true); }
            set { settings.SetBoolean(HTML_ENCODE, value); }
        }

        public bool UseBrs
        {
            get { return settings.GetBoolean(USE_BRS, false); }
            set { settings.SetBoolean(USE_BRS, value); }
        }

        public string SyntaxClassAttribute
        {
            get { return settings.GetString(SYNTAX_CLASS_ATTRIBUTE, "none"); }
            set { settings.SetString(SYNTAX_CLASS_ATTRIBUTE, value); }
        }

        public bool DoNotDisplayGutter
        {
            get { return settings.GetBoolean(DO_NOT_DISPLAY_GUTTER, false); }
            set { settings.SetBoolean(DO_NOT_DISPLAY_GUTTER, value); }
        }

        public bool DoNotShowToolBar
        {
            get { return settings.GetBoolean(DO_NOT_SHOW_TOOLBAR, false); }
            set { settings.SetBoolean(DO_NOT_SHOW_TOOLBAR, value); }
        }
        public bool CollapseBlock
        {
            get { return settings.GetBoolean(COLLAPSE_BOCK, false); }
            set { settings.SetBoolean(COLLAPSE_BOCK, value); }
        }
        public bool ShowRuler
        {
            get { return settings.GetBoolean(SHOW_RULER, false); }
            set { settings.SetBoolean(SHOW_RULER, value); }
        }
        public int LineCountStart
        {
            get { return settings.GetInt(LINECOUNT_START, 1); }
            set { settings.SetInt(LINECOUNT_START, value); }
        }

        public bool DoNotUseSmartTabs
        {
            get { return settings.GetBoolean(DO_NOT_USE_SMARTTABS, false); }
            set { settings.SetBoolean(DO_NOT_USE_SMARTTABS, value); }
        }

        public bool DoNotAutoLink
        {
            get { return settings.GetBoolean(DO_NOT_AUTOLINK, false); }
            set { settings.SetBoolean(DO_NOT_AUTOLINK, value); }
        }
    }
}
