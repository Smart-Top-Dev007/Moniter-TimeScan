using Monitor.TaskView.View;
using System;
using System.Windows;

namespace Monitor.TaskView.myEvents
{
    public static class Events
    {
        

        public delegate void OnStartUpDelegate(StartupEventArgs e);
        public static event OnStartUpDelegate OnStartUp;

        public delegate void OnExitDelegate(ExitEventArgs e);
        public static event OnExitDelegate OnExit;

        public delegate void OnRegisterDelegate(EventArgs e);
        public static event OnRegisterDelegate OnRegister;

        public delegate void OnConnectionFinishedDelegate(EventArgs args);
        public static event OnConnectionFinishedDelegate OnConnectedFinished;

        public delegate void OnMainProcDelegate();
        public static event OnMainProcDelegate OnMainProc;

        public delegate void OnPasswordCheckDelegate();
        public static event OnPasswordCheckDelegate OnPasswordCheck;

        public delegate void OnRecvDataDelegate(byte[] buff, int len);
        public static event OnRecvDataDelegate OnReceiveData;


        public delegate void OnChangeServerDelegate();
        public static event OnChangeServerDelegate OnChangeServer;

        static Events()
        {
            EventHandlers.Initialize();
        }
        
        public static void RaiseOnChangeServer()
        {
            if (OnChangeServer != null)
            {
                OnChangeServer();
            }
        }
        public static void RaiseOnReveiveData(byte[] buff, int len)
        {
            if ( OnReceiveData != null )
            {
                OnReceiveData(buff, len);
            }
        }
        public static void RaiseOnPassword()
        {
            if (OnPasswordCheck != null)
            {
                OnPasswordCheck();
            }
        }
        public static void RaiseOnMainProc()
        {
            if (OnMainProc != null)
            {
                OnMainProc();
            }
        }
        public static void RaiseOnStartUp(StartupEventArgs e)
        {
            if (OnStartUp != null)
            {
                OnStartUp(e);
            }
        }
        public static void RaiseOnExit(ExitEventArgs e)
        {
            if (OnExit != null)
            {
                OnExit(e);
            }
        }
        public static void RaiseOnRegister(EventArgs args)
        {
            if (OnRegister != null)
            {
                OnRegister(args);
            }
        }
 
        public static void RaiseOnConnectedFinished(EventArgs args)
        {
            if (OnConnectedFinished != null)
            {
                OnConnectedFinished(args);
            }
        }


    }
}
