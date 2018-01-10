/************************************************************************************  
' Copyright (C) 2009 Anthony Bouch (http://www.58bits.com) under the terms of the
' Microsoft Public License (Ms-PL http://www.codeplex.com/precode/license)
' Update Processor courtesy of Alexander Groß at http://www.therightstuff.de/ 
'***********************************************************************************/
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Xml.XPath;

namespace FiftyEightBits.PreCode
{
    public sealed class UpdateProcessor
    {
        #region Events
        public event EventHandler<UpdateComponentCompletedEventArgs> UpdateComponentCompleted;
        #endregion
        
        #region Component Update
        public void UpdateComponentAsync(UpdateableComponent component, object userState)
        {
            UpdateComponentArgs args = new UpdateComponentArgs();
            args.Component = component;
            args.UserState = userState;

            Trace.WriteLine(String.Format("Starting update of component {0}, UserState={1}.",
                                          args.Component.Assembly,
                                          args.UserState ?? "(null)"));

            BackgroundWorker bgw = new BackgroundWorker();
            bgw.WorkerSupportsCancellation = false;
            bgw.WorkerReportsProgress = false;
            bgw.DoWork += UpdateComponent_DoWork;
            bgw.RunWorkerCompleted += UpdateComponent_RunWorkerCompleted;
            bgw.RunWorkerAsync(args);
        }

        static void UpdateComponent_DoWork(object sender, DoWorkEventArgs e)
        {
            UpdateComponentArgs args = e.Argument as UpdateComponentArgs;
            if (args == null)
            {
                throw new ArgumentException("e");
            }

            try
            {
                Trace.WriteLine(String.Format("Updating component {0} at {1}.",
                                              args.Component.Assembly,
                                              args.Component.UpdateInfoUrl));
                WebRequest request = WebRequest.CreateDefault(new Uri(args.Component.UpdateInfoUrl));
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        XPathDocument doc = new XPathDocument(stream);
                        XPathNavigator nav = doc.CreateNavigator();

                        XPathNavigator updateUrl = nav.SelectSingleNode("/updateInfo/updateUrl/text()");
                        XPathNavigator description = nav.SelectSingleNode("/updateInfo/description/text()");
                        XPathNavigator updateArticle = nav.SelectSingleNode("/updateInfo/updateArticle/text()");
                        XPathNavigator version = nav.SelectSingleNode("/updateInfo/version/text()");

                        args.UpdateInfo = new UpdateInfo(updateUrl != null ? updateUrl.Value.Trim() : null,
                                                         description != null ? description.Value.Trim() : null,
                                                         updateArticle != null ? updateArticle.Value.Trim() : null,
                                                         version != null ? new Version(version.Value) : null);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(String.Format("Error updating component {0} at {1}: {2}",
                                              args.Component.Assembly,
                                              args.Component.UpdateInfoUrl,
                                              ex));
                args.Error = ex;
            }

            e.Result = args;
        }

        void UpdateComponent_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // e.Error is always null to enable passing back the UserState to the caller.
            UpdateComponentArgs result = (UpdateComponentArgs)e.Result;

            UpdateComponentCompletedEventArgs updateComponentResult;
            if (result.Error != null)
            {
                updateComponentResult = new UpdateComponentCompletedEventArgs(result.Component,
                                                                              null,
                                                                              result.UserState,
                                                                              result.Error);
            }
            else
            {
                updateComponentResult = new UpdateComponentCompletedEventArgs(result.Component,
                                                                              result.UpdateInfo,
                                                                              result.UserState,
                                                                              null);
            }

            // Signal async callers.
            if (UpdateComponentCompleted != null)
            {
                UpdateComponentCompleted(this, updateComponentResult);
            }
        }
        #endregion
    }
}
