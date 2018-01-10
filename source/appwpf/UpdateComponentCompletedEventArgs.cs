/************************************************************************************  
' Copyright (C) 2009 Anthony Bouch (http://www.58bits.com) under the terms of the
' Microsoft Public License (Ms-PL http://www.codeplex.com/precode/license)
' Update Processor courtesy of Alexander Groß at http://www.therightstuff.de/ 
'***********************************************************************************/
using System;
using System.Diagnostics;

namespace FiftyEightBits.PreCode
{
    public class UpdateComponentCompletedEventArgs : EventArgs
    {
        UpdateableComponent _component;
        Exception _error;
        UpdateInfo _updateInfo;
        object _userState;

        public UpdateableComponent Component
        {
            [DebuggerStepThrough]
            get { return _component; }
            [DebuggerStepThrough]
            protected set { _component = value; }
        }

        public UpdateInfo UpdateInfo
        {
            [DebuggerStepThrough]
            get { return _updateInfo; }
            [DebuggerStepThrough]
            protected set { _updateInfo = value; }
        }

        public object UserState
        {
            [DebuggerStepThrough]
            get { return _userState; }
            [DebuggerStepThrough]
            protected set { _userState = value; }
        }

        public Exception Error
        {
            [DebuggerStepThrough]
            get { return _error; }
            [DebuggerStepThrough]
            protected set { _error = value; }
        }

        public UpdateComponentCompletedEventArgs(UpdateableComponent component,
                                                 UpdateInfo updateInfo,
                                                 object userState,
                                                 Exception error)
        {
            Component = component;
            UpdateInfo = updateInfo;
            UserState = userState;
            Error = error;
        }
    }
}
