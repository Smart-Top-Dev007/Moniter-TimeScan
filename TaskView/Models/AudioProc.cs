using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using CSCore;
using CSCore.Codecs.WAV;
using CSCore.CoreAudioAPI;
using CSCore.MediaFoundation;
using CSCore.SoundIn;
using CSCore.Streams;
using Monitor.TaskView.Globals;
using Monitor.TaskView.Utils;
using Monitor.TaskView.View;

namespace Monitor.TaskView.Models
{
    //extern alias CSAudio;
    public class AudioProc
    {
        static bool bAudio;
        static bool bRecordStart;
        //private CSAudio::CSAudioRecorder.AudioRecorder audioRecorder1;
        private WasapiLoopbackCapture wasapiCapture = null;
        private MediaFoundationEncoder audioWriter = null;
        private SoundInSource wasapiCaptureSource = null;
        private byte[] audioBuffer = null;


        // WaveFileWriter RecordedAudioWriter = null;
        //private CSAudio.CSAudioRecorder.AudioVisualization audioVisualization1;
        //private CSAudio.CSAudioRecorder.AudioMeter audioMeter1;
        //private System.Windows.Forms.Timer tmrMeterIn;
        string strAudioPathName = "";
        string strAudioFileName = "";
        string strProcessName = ""; string strProcessWindow = ""; string strProcessPath = "";
        DateTime StartTime = new DateTime(); DateTime endTime = new DateTime();
        static bool bFirst = true;

    public AudioProc()
        {
            bAudio = false;
            bRecordStart = true;
            //AudioInit();
            LoadAudioInfo(Settings.Instance.Directories.TodayDirectory);
            //audioRecorder1 = new CSAudio.CSAudioRecorder.AudioRecorder();
            //audioVisualization1 = new CSAudio.CSAudioRecorder.AudioVisualization();
            //audioMeter1 = new CSAudio.CSAudioRecorder.AudioMeter();
            //tmrMeterIn = new System.Windows.Forms.Timer();
        }

        //private void AudioInit()
        //{
        //    audioRecorder1 = null;
        //    audioRecorder1 = new CSAudio.CSAudioRecorder.AudioRecorder();
            
        //    //audioVisualization1 = new CSAudio.CSAudioRecorder.AudioVisualization();
        //    //audioMeter1 = new CSAudio.CSAudioRecorder.AudioMeter();
        //}

        public void CheckAudioLevels()
        {
            DateTime localDate = DateTime.Now;
            

            if (bAudio == true)
            {
                try
                {
                    if ((int)localDate.Subtract(endTime).TotalSeconds > Constants.nAudioSession + 1)
                    {
                        //audioRecorder1.Stop();
                        wasapiCapture.Stop();
                        Thread.Sleep(1000);
                        wasapiCapture.Dispose();
                        audioWriter.Dispose();
                        wasapiCaptureSource.Dispose();
                        //while (audioRecorder1.RecordingState != CSAudio.CSAudioRecorder.RecordingState.Ready)
                        //{
                        //    Application.DoEvents();

                        //    Thread.Sleep(100);
                        //}

                        System.TimeSpan diff1 = endTime.Subtract(StartTime);
                        if (diff1.Seconds < 4 && diff1.Minutes == 0)
                        {
                            try
                            {
                                if (File.Exists(strAudioPathName))
                                {
                                    bFirst = true;
                                    bAudio = false;
                                    bRecordStart = true;
                                    File.Delete(strAudioPathName);
                                }
                            }
                            catch { }
                            return;
                        }

                        byte[] bytes = File.ReadAllBytes(strAudioPathName);
                        if (bytes.Length == 0)
                        {
                            try
                            {
                                if (File.Exists(strAudioPathName))
                                {
                                    bFirst = true;
                                    bAudio = false;
                                    bRecordStart = true;
                                    File.Delete(strAudioPathName);
                                }
                            }
                            catch { }
                            return;
                        }

                        string strSize = "";
                        long nSize = long.Parse(bytes.Length.ToString());
                        if ((nSize / 1024) < 1000)
                        {
                            strSize = (nSize / 1024f).ToString("0.00") + "KB";
                        }
                        else
                        {
                            strSize = ((nSize / 1024f) / 1024f).ToString("0.00") + "MB";
                        }

                        string sendData = "N" + strProcessName + Constants.filePattern + "W" + strProcessWindow + Constants.filePattern + "P" + strProcessPath + Constants.filePattern + "S" + StartTime.ToString() + Constants.filePattern + "E" + endTime.ToString() + Constants.filePattern + "F" + strAudioFileName + Constants.filePattern + "Z" + strSize;

                        byte[] processBytes = Encoding.UTF8.GetBytes(sendData);
                        CommProc.Instance.SendDataAnalysis(Constants.Re_DataAudio, processBytes, processBytes.Length);
                        Thread.Sleep(500);

                        string strDbPath = Settings.Instance.Directories.TodayDirectory;
                        string strData = strProcessName + Constants.filePattern + strProcessWindow + Constants.filePattern + strProcessPath + Constants.filePattern + StartTime.ToString() + Constants.filePattern + endTime.ToString() + Constants.filePattern + strAudioFileName + Constants.filePattern + strSize;
                        Md5Crypto.WriteCryptoFileAppend(strDbPath, Constants.AudioFileName, strData);

                        byte[] AudioBytes = Encoding.UTF8.GetBytes(strProcessWindow);
                        CommProc.Instance.SendDataAnalysis(Constants.Re_MsgAudio, AudioBytes, AudioBytes.Length);
                        Thread.Sleep(500);

                        byte[] fileBytes = Encoding.UTF8.GetBytes(strAudioFileName);
                        byte[] totalByte = new byte[bytes.Length + 30];
                        Array.Copy(fileBytes, 0, totalByte, 0, fileBytes.Length);
                        Array.Copy(bytes, 0, totalByte, 30, bytes.Length);

                        //fileBytes.CopyTo(totalByte, 0);
                        //bytes.CopyTo(totalByte, 12);

                        CommProc.Instance.SendDataAnalysis(Constants.Re_AudioData, totalByte, totalByte.Length);
                        bRecordStart = true;
                        bFirst = true;
                        bAudio = false;
                    }
                }
                catch (Exception ex)
                {
                    bFirst = true;
                    bAudio = false;
                    bRecordStart = true;
                    if (File.Exists(strAudioPathName))
                    {
                        File.Delete(strAudioPathName);
                    }
                }
            }

            try
            {
                lock (this)
                {
                    using (var sessionManager = GetDefaultAudioSessionManager2(DataFlow.Render))
                    {
                        using (var sessionEnumerator = sessionManager.GetSessionEnumerator())
                        {
                            foreach (var session in sessionEnumerator)
                            {
                                using (var audioSessionControl2 = session.QueryInterface<AudioSessionControl2>())
                                {
                                    var process = audioSessionControl2.Process;

                                    using (var audioMeterInformation = session.QueryInterface<AudioMeterInformation>())
                                    {
                                        var value1 = audioMeterInformation.GetPeakValue();
                                        if (value1 > 1E-05)
                                        {
                                            if (bRecordStart == true)
                                            {
                                                //if (audioRecorder1.RecordingState == CSAudio.CSAudioRecorder.RecordingState.Recording)
                                                //{
                                                //    audioRecorder1.Stop();

                                                //    while (audioRecorder1.RecordingState != CSAudio.CSAudioRecorder.RecordingState.Ready)
                                                //    {
                                                //        Application.DoEvents();

                                                //        Thread.Sleep(100);
                                                //    }
                                                //}

                                                if (bFirst == true)
                                                {
                                                    bFirst = false;
                                                    var procID = audioSessionControl2.ProcessID;
                                                    strProcessName = process.ProcessName;
                                                    strProcessWindow = process.MainWindowTitle;
                                                    if (strProcessWindow == "")
                                                    {
                                                        strProcessWindow = Constants.Unknown;
                                                    }
                                                    var buffer = new StringBuilder(1024);
                                                    try
                                                    {
                                                        IntPtr hprocess = NativeImports.OpenProcess(ProcessAccessFlags.QueryLimitedInformation, false, (int)procID);
                                                        if (hprocess != IntPtr.Zero)
                                                        {
                                                            try
                                                            {
                                                                int size = buffer.Capacity;
                                                                if (NativeImports.QueryFullProcessImageName(hprocess, 0, buffer, out size))
                                                                {
                                                                    strProcessPath = buffer.ToString();
                                                                }
                                                            }
                                                            finally
                                                            {
                                                                NativeImports.CloseHandle(hprocess);
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                    }
                                                    if (strProcessPath == "")
                                                    {
                                                        strProcessPath = Constants.Unknown;
                                                    }
                                                    strAudioPathName = SetDestinationFileName();
                                                    wasapiCapture = new WasapiLoopbackCapture();
                                                    wasapiCapture.Initialize();
                                                    //wasapiCapture = new WasapiLoopbackCapture(100, new WaveFormat(8000, 8, 2));
                                                    
                                                    wasapiCaptureSource = new SoundInSource(wasapiCapture);
                                                    //var stereoSource = wasapiCaptureSource.ToStereo();
                                                    //wasapiCaptureSource.WaveFormat.Channels = Constants.CaptureTime;
                                                    try
                                                    {
                                                        audioWriter = MediaFoundationEncoder.CreateWMAEncoder(wasapiCaptureSource.WaveFormat, strAudioPathName, 8);
                                                    }
                                                    catch
                                                    {
                                                        //Logger.Log.Instance.DoLog("audio exception.");
                                                        return;
                                                    }
                                                    //audioWriter = MediaFoundationEncoder.CreateWMAEncoder(new WaveFormat(32000, 32, 2), strAudioPathName, 8);
                                                    audioBuffer = new byte[wasapiCaptureSource.ToStereo().WaveFormat.BytesPerSecond / 8];
                                                    //audioBuffer = new byte[65000];
                                                    wasapiCapture.Start();

                                                    //AudioInit();

                                                    //strAudioPathName = SetDestinationFileName();
                                                    //audioRecorder1.FileName = strAudioPathName;
                                                    //audioRecorder1.AudioSource = Constants.AudioSource;
                                                    //audioRecorder1.DeviceIndex = Constants.DeviceIndex;
                                                    //audioRecorder1.Format = (CSAudio.CSAudioRecorder.Format)Enum.Parse(typeof(CSAudio.CSAudioRecorder.Format), Constants.Format);

                                                    //#region MoreOptionalProperties

                                                    //audioRecorder1.Bitrate = (CSAudio.CSAudioRecorder.Bitrate)Enum.Parse(typeof(CSAudio.CSAudioRecorder.Bitrate), Constants.Bitrate);

                                                    //audioRecorder1.Samplerate = (CSAudio.CSAudioRecorder.Samplerate)Enum.Parse(typeof(CSAudio.CSAudioRecorder.Samplerate), Constants.Samplerate);

                                                    //audioRecorder1.Bits = (CSAudio.CSAudioRecorder.Bits)Enum.Parse(typeof(CSAudio.CSAudioRecorder.Bits), Constants.Bits);

                                                    //audioRecorder1.Channels = (CSAudio.CSAudioRecorder.Channels)Enum.Parse(typeof(CSAudio.CSAudioRecorder.Channels), Constants.Channels);

                                                    //audioRecorder1.Mode = (CSAudio.CSAudioRecorder.Mode)Enum.Parse(typeof(CSAudio.CSAudioRecorder.Mode), Constants.Mode);

                                                    //audioRecorder1.Latency = Constants.Latency;

                                                    ////audioRecorder1.AudioVisualization = audioVisualization1;

                                                    ////audioRecorder1.AudioMeter = audioMeter1;

                                                    //audioRecorder1.StartOnNoise = Constants.StartOnNoise;

                                                    //audioRecorder1.StartOnNoiseVal = float.Parse(Constants.StartOnNoiseVal);
                                                    //audioRecorder1.StopOnSilence = Constants.StopOnSilence;

                                                    //audioRecorder1.StopOnSilenceVal = float.Parse(Constants.StopOnSilenceVal);

                                                    //audioRecorder1.StopOnSilenceSeconds = int.Parse(Constants.StopOnSilenceSeconds);

                                                    //#endregion

                                                    //audioRecorder1.Start();

                                                }

                                                Thread.Sleep(Constants.nAudioCheckTime);
                                                var value2 = audioMeterInformation.GetPeakValue();
                                                if (value2 > 1E-05)
                                                {
                                                    Thread.Sleep(Constants.nAudioCheckTime);
                                                    var value3 = audioMeterInformation.GetPeakValue();
                                                    if (value3 > 1E-05)
                                                    {
                                                        Thread.Sleep(Constants.nAudioCheckTime);
                                                        var value4 = audioMeterInformation.GetPeakValue();
                                                        if (value4 > 1E-05)
                                                        {
                                                            if (value1 == value2 && value2 == value3 && value3 == value4)
                                                            {
                                                                wasapiCapture.Stop();
                                                                Thread.Sleep(500);
                                                                wasapiCapture.Dispose();
                                                                audioWriter.Dispose();
                                                                wasapiCaptureSource.Dispose();
                                                                bFirst = true;
                                                                bRecordStart = true;
                                                                bAudio = false;
                                                                try
                                                                {
                                                                    if (File.Exists(strAudioPathName))
                                                                    {
                                                                        File.Delete(strAudioPathName);
                                                                    }
                                                                }
                                                                catch { }
                                                            }
                                                            else
                                                            {
                                                                bRecordStart = false;

                                                                //App.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(
                                                                //() =>
                                                                //{
                                                                //    var notify = new NotificationWindow();
                                                                //    notify.Show(strProcessWindow, Constants.Se_MsgAudio);
                                                                //}));
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //audioRecorder1.Stop();
                                                            //while (audioRecorder1.RecordingState != CSAudio.CSAudioRecorder.RecordingState.Ready)
                                                            //{
                                                            //    Application.DoEvents();

                                                            //    Thread.Sleep(100);
                                                            //}
                                                            wasapiCapture.Stop();
                                                            Thread.Sleep(500);
                                                            wasapiCapture.Dispose();
                                                            audioWriter.Dispose();
                                                            wasapiCaptureSource.Dispose();
                                                            bFirst = true;
                                                            bRecordStart = true;
                                                            bAudio = false;
                                                            try
                                                            {
                                                                if (File.Exists(strAudioPathName))
                                                                {
                                                                    File.Delete(strAudioPathName);
                                                                }
                                                            }
                                                            catch { }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //audioRecorder1.Stop();
                                                        //while (audioRecorder1.RecordingState != CSAudio.CSAudioRecorder.RecordingState.Ready)
                                                        //{
                                                        //    Application.DoEvents();

                                                        //    Thread.Sleep(100);
                                                        //}
                                                        wasapiCapture.Stop();
                                                        Thread.Sleep(500);
                                                        wasapiCapture.Dispose();
                                                        audioWriter.Dispose();
                                                        wasapiCaptureSource.Dispose();
                                                        bFirst = true;
                                                        bRecordStart = true;
                                                        bAudio = false;
                                                        try
                                                        {
                                                            if (File.Exists(strAudioPathName))
                                                            {
                                                                File.Delete(strAudioPathName);
                                                            }
                                                        }
                                                        catch { }
                                                    }
                                                }
                                                else
                                                {
                                                    //audioRecorder1.Stop();
                                                    //while (audioRecorder1.RecordingState != CSAudio.CSAudioRecorder.RecordingState.Ready)
                                                    //{
                                                    //    Application.DoEvents();

                                                    //    Thread.Sleep(100);
                                                    //}
                                                    wasapiCapture.Stop();
                                                    Thread.Sleep(500);
                                                    wasapiCapture.Dispose();
                                                    audioWriter.Dispose();
                                                    wasapiCaptureSource.Dispose();
                                                    bFirst = true;
                                                    bRecordStart = true;
                                                    bAudio = false;
                                                    try
                                                    {
                                                        if (File.Exists(strAudioPathName))
                                                        {
                                                            File.Delete(strAudioPathName);
                                                        }
                                                    }
                                                    catch { }
                                                }
                                            }
                                            else
                                            {
                                                wasapiCaptureSource.DataAvailable += (s, e) =>
                                                {
                                                    int read = wasapiCaptureSource.ToStereo().Read(audioBuffer, 0, audioBuffer.Length);
                                                    audioWriter.Write(audioBuffer, 0, read);
                                                };
                                                bAudio = true;
                                                endTime = DateTime.Now;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                } // lock
            }
            catch (Exception ex)
            {
                bFirst = true;
                bRecordStart = true;
                bAudio = false;
                return;
            }
        }
        

        private static AudioSessionManager2 GetDefaultAudioSessionManager2(DataFlow dataFlow)
        {
            using (var enumerator = new MMDeviceEnumerator())
            {
                using (var device = enumerator.GetDefaultAudioEndpoint(dataFlow, Role.Multimedia))
                {
                    // Console.WriteLine("DefaultDevice: " + device.FriendlyName);
                    var sessionManager = AudioSessionManager2.FromMMDevice(device);
                    return sessionManager;
                }
            }
        }

        private string SetDestinationFileName()
        {
            string strPath = Settings.Instance.Directories.TodayDirectory + "\\Audio";
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }
            strAudioFileName = Settings.Instance.RegValue.UserName + "-" + DateTime.Now.ToString("HH-mm-ss").Replace(":", "-") + Constants.AudioFileExtension;
            //strAudioFileName = Md5Crypto.OnImageFileNameChange("D", DateTime.Now.ToString("HH-mm-ss").Replace(":", "-"));
            //strAudioFileName = strAudioFileName.Substring(2) + Constants.AudioFileExtension;
            strPath = strPath + "\\" + strAudioFileName;
            StartTime = DateTime.Now;
            return strPath;
        }

        public void LoadAudioInfo(string strPath)
        {
            strPath = strPath + "\\" + Constants.AudioFileName;
            List<string> strTempList = new List<string>();
            strTempList = Md5Crypto.ReadCryptoFile(strPath);
            if (strTempList.Count == 0)
            {
                return;
            }
            Settings.Instance.AudioList = new List<ListOfAudio>();
            String[] spearator = { Constants.filePattern };

            foreach (var line in strTempList)
            {
                try
                {
                    string[] strArray = line.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                    Settings.Instance.LOA.ProcessName = strArray[0];
                    Settings.Instance.LOA.ProcessWindow = strArray[1];
                    Settings.Instance.LOA.ProcessPath = strArray[2];
                    Settings.Instance.LOA.ProcessStartTime = DateTime.Parse(strArray[3]);
                    Settings.Instance.LOA.ProcessEndTime = DateTime.Parse(strArray[4]);
                    Settings.Instance.LOA.FileName = strArray[5];
                    Settings.Instance.LOA.FileSize = strArray[6];
                    Settings.Instance.AudioList.Add(Settings.Instance.LOA);
                }
                catch (Exception ex)
                {
                    CustomEx.DoExecption(Constants.exResume, ex);
                }
            }
        }
    }
}