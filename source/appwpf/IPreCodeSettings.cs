/************************************************************************************  
' Copyright (C) 2009 Anthony Bouch (http://www.58bits.com) under the terms of the
' Microsoft Public License (Ms-PL http://www.codeplex.com/precode/license)
'***********************************************************************************/
namespace FiftyEightBits.PreCode
{
    public interface IPreCodeSettings
    {
        bool ShowOptions { get; set; }
        int SurroundWith { get; set; }
        bool HtmlEncode { get; set; }
        bool UseBrs { get; set; }
        string SyntaxClassAttribute { get; set; }
        bool DoNotShowToolBar { get; set; }
        bool DoNotDisplayGutter { get; set; }
        bool CollapseBlock { get; set; }
        bool DoNotAutoLink { get; set; }
        bool ShowRuler { get; set; }
        bool DoNotUseSmartTabs { get; set; }
        int LineCountStart { get; set; }
    }
}
