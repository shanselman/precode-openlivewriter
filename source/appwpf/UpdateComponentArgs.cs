/************************************************************************************  
' Copyright (C) 2009 Anthony Bouch (http://www.58bits.com) under the terms of the
' Microsoft Public License (Ms-PL http://www.codeplex.com/precode/license)
' Update Processor courtesy of Alexander Groß at http://www.therightstuff.de/ 
'***********************************************************************************/
using System;

namespace FiftyEightBits.PreCode
{
    internal class UpdateComponentArgs
    {
        public UpdateableComponent Component;
        public UpdateInfo UpdateInfo;
        public object UserState;
        public Exception Error;
    }
}
