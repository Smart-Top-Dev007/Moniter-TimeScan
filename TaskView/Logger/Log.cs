﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Monitor.TaskView.Globals;
using Monitor.TaskView.Utils;

namespace Monitor.TaskView.Logger
{
    public class Log
    {
        public enum LogType
        {
            Info,
            Error
        }

        private static object _syncLock;
        private static Log _instance;

        public string LogFilePath { get; private set; }
        public List<Tuple<LogType, string>> Logs { get; private set; }

        public static Log Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Log(Path.Combine(UserFolder, Constants.LoaderMainLogFileName), true);
                }

                return _instance;
            }
        }
        public Log(string logfile, bool overwriteFile = false)
        {
            //first starting LogFile delete
            try
            {
                if (overwriteFile && File.Exists(logfile))
                {
                    File.Delete(logfile);
                }
            }
            catch
            {

            }            

            _syncLock = new object();
            LogFilePath = logfile;
            Logs = new List<Tuple<LogType, string>>();
        }
        public void DoLog(string text, LogType logType = LogType.Info)
        {
            try
            {
                lock (_syncLock)
                {
                    var logString = string.Format("[{0}] <{1}>\r {2}\r\n", DateTime.Now, logType, text);
                    try
                    {
                        Logs.Add(new Tuple<LogType, string>(logType, logString));
                        File.AppendAllText(LogFilePath, CensorLog(logString));
                    }
                    catch
                    {

                    }
                    
                    //
                }

            }
            catch (Exception ex)
            {
                CustomMsg message = new CustomMsg(ex.ToString());
             //   MessageBox.Show(ex.ToString(), "Log Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private static string CensorLog(string log)
        {
            //hide username
            return log.Replace(UserFolder, Path.Combine(Directory.GetParent(UserFolder).FullName, "****"));
        }
        private static string UserFolder
        {
            get { return Path.GetPathRoot(Environment.SystemDirectory)+"Users\\Public"; }
        }
    }
}
