using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using System.Drawing.Imaging;


namespace CUST2095.WebTest.DTO
{
    public class Logger
    {
        private string _testName;
        private string _logfileFullPath;
        private string _screenShotsDir;
        private string _logFilesDir;

        private StreamWriter _writer;

        public Logger(string testName)
        {
            this._testName = testName;
			this._logFilesDir = System.Configuration.ConfigurationManager.AppSettings["AutomationLogFilePath"];
			this._screenShotsDir = System.Configuration.ConfigurationManager.AppSettings["AutomationScreenShotsPath"]; 
        }

        public void Setup()
        {
            //string directory = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
            //directory = directory.Replace("Debug", "Log");

            if (!Directory.Exists(this._logFilesDir))
            {
                DirectoryInfo di = Directory.CreateDirectory(this._logFilesDir);
            }

            if (!Directory.Exists(this._screenShotsDir))
            {
                DirectoryInfo di = Directory.CreateDirectory(this._screenShotsDir);
            }

            string currentTime = DateTime.Now.ToString("MM_yy_yyyy_hh_mm_ss");
            string name = this._testName.Replace(" ", "_");
            this._logfileFullPath = this._logFilesDir + "\\" + name + "_" + currentTime + ".txt";
        }

        public void Write(string line)
        {
            string time = DateTime.Now.ToString("MM/dd/yyyy - hh:mm:ss ");
            string toWrite = "[" + time + "]" + "[Test: " + _testName + "]" + " - " + line;
            this._writer.WriteLine(toWrite);
            this._writer.Flush();
        }

        public void Debug(string line)
        {
            string time = DateTime.Now.ToString("MM/dd/yyyy - hh:mm:ss ");
            string toWrite = "[" + time + "]" + line;
            this._writer.WriteLine(toWrite);
            this._writer.Flush();
        }

        public void Error(string errMsg, string cause)
        {
            string time = DateTime.Now.ToString("MM/dd/yyyy - hh:mm:ss ");
            string toWrite = "ERROR !" + "[" + time + "]" + errMsg;
            this._writer.WriteLine(toWrite);
            toWrite = "*************************" + cause;
            this._writer.WriteLine(toWrite);
            this._writer.Flush();
        }

        public void Open()
        {
            //if (_writer == null)
            //{
                this._writer = new System.IO.StreamWriter(this._logfileFullPath);
            //}
        }

        public void Close()
        {
            this._writer.Close();
        }

        public void CaptureScreen(IWebDriver driver)
        {
            Screenshot scrFile = ((ITakesScreenshot)driver).GetScreenshot();
            string fileName = this._testName.Replace(" ", "_") + "_" + DateTime.Now.ToString("MM_yy_yyyy_hh_mm_ss") + ".JPG";
            scrFile.SaveAsFile(this._screenShotsDir + "\\" + fileName, ImageFormat.Jpeg);
            Write("Saved Screenshot to : " + this._screenShotsDir + "\\" + fileName);
        }

    }
}
