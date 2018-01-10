/************************************************************************************  
' Copyright (C) 2009 Anthony Bouch (http://www.58bits.com) under the terms of the
' Microsoft Public License (Ms-PL http://www.codeplex.com/precode/license)
' Update Processor courtesy of Alexander Groß at http://www.therightstuff.de/ 
'***********************************************************************************/
using System;
using System.Diagnostics;

namespace FiftyEightBits.PreCode
{
    [DebuggerDisplay(
        "UpdateUrl={UpdateUrl}, Description={Description}, UpdateArticleUrl={UpdateArticleUrl}, Version={Version}")]
    public class UpdateInfo
    {
        string _description;
        string _updateArticleUrl;
        string _updateUrl;
        Version _version;

        public string UpdateUrl
        {
            [DebuggerStepThrough]
            get { return _updateUrl; }
            [DebuggerStepThrough]
            protected set { _updateUrl = value; }
        }

        public string Description
        {
            [DebuggerStepThrough]
            get { return _description; }
            [DebuggerStepThrough]
            protected set { _description = value; }
        }

        public string UpdateArticleUrl
        {
            [DebuggerStepThrough]
            get { return _updateArticleUrl; }
            [DebuggerStepThrough]
            protected set { _updateArticleUrl = value; }
        }

        public Version Version
        {
            [DebuggerStepThrough]
            get { return _version; }
            [DebuggerStepThrough]
            protected set { _version = value; }
        }

        public UpdateInfo(string updateUrl, string description, string updateArticleUrl, Version version)
        {
            UpdateUrl = updateUrl;
            Description = description;
            UpdateArticleUrl = updateArticleUrl;
            Version = version;
        }

        public override string ToString()
        {
            return
                String.Format("UpdateUrl={0}, Description={1}, UpdateArticleUrl={2}, Version={3}",
                              UpdateUrl,
                              Description,
                              UpdateArticleUrl,
                              Version);
        }
    }
}
