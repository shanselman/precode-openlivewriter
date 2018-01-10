/************************************************************************************  
' Copyright (C) 2009 Anthony Bouch (http://www.58bits.com) under the terms of the
' Microsoft Public License (Ms-PL http://www.codeplex.com/precode/license)
' Update Processor courtesy of Alexander Groß at http://www.therightstuff.de/ 
'***********************************************************************************/
using System;
using System.Diagnostics;

namespace FiftyEightBits.PreCode
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class UpdateableComponentAttribute : Attribute
    {
        string _updateInfoUrl;

        public string UpdateInfoUrl
        {
            [DebuggerStepThrough]
            get { return _updateInfoUrl; }
            [DebuggerStepThrough]
            protected set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                _updateInfoUrl = value;
            }
        }

        public UpdateableComponentAttribute(string updateInfoUrl)
        {
            UpdateInfoUrl = updateInfoUrl;
        }
    }
}