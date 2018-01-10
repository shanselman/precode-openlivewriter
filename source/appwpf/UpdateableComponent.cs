/************************************************************************************  
' Copyright (C) 2009 Anthony Bouch (http://www.58bits.com) under the terms of the
' Microsoft Public License (Ms-PL http://www.codeplex.com/precode/license)
' Update Processor courtesy of Alexander Groß at http://www.therightstuff.de/ 
'***********************************************************************************/
using System;
using System.Reflection;
using System.Diagnostics;

namespace FiftyEightBits.PreCode
{
    [DebuggerDisplay("Assembly={Assembly}, UpdateInfoUrl={UpdateInfoUrl}")]
    public class UpdateableComponent
    {
        Assembly _assembly;
        string _updateInfoUrl;

        public Assembly Assembly
        {
            [DebuggerStepThrough]
            get { return _assembly; }
            [DebuggerStepThrough]
            protected set { _assembly = value; }
        }

        public string UpdateInfoUrl
        {
            [DebuggerStepThrough]
            get { return _updateInfoUrl; }
            [DebuggerStepThrough]
            protected set { _updateInfoUrl = value; }
        }

        public string Name
        {
            get
            {
                AssemblyTitleAttribute[] title =
                    Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false) as AssemblyTitleAttribute[];
                if (title == null)
                {
                    // Fall back to assembly name.
                    return Assembly.GetName().Name;
                }

                return title[0].Title;
            }
        }

        public Version Version
        {
            get
            {
                AssemblyFileVersionAttribute[] fileVersion =
                    Assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false) as AssemblyFileVersionAttribute[];
                if (fileVersion != null)
                {
                    return new Version(fileVersion[0].Version);
                }

                return Assembly.GetName().Version;
            }
        }

        public UpdateableComponent(Assembly assembly, string updateInfoUrl)
        {
            Assembly = assembly;
            UpdateInfoUrl = updateInfoUrl;
        }

        public bool NeedsUpdate(UpdateInfo updateInfo)
        {
            return Version.CompareTo(updateInfo.Version) < 0;
        }
    }
}
