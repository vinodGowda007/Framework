using SeleniumAutoFramework.Base;
using System;
using System.IO;
using System.Threading;

namespace SeleniumAutoFramework.Helpers;

public class LogHelper
{
    //Create a file with unique name so store the log informations
    public static string dir = TestInitializeHook.dir;
    private static void LogFile1(string TestName, string LogMessage)
    {
        string fullpath = dir + TestName;
        if (Directory.Exists(fullpath))
            File.AppendAllText(fullpath + @"\" + TestName + ".log", DateTime.Now.ToString() + " " + LogMessage + "\n");
        else
        {
            Directory.CreateDirectory(fullpath);
            File.AppendAllText(fullpath + @"\" + TestName + ".log", DateTime.Now.ToString() + " " + LogMessage + "\n");
        }
    }
    public static void LogFile(string TestName, string LogMessage)
    {
        try
        {
            LogFile1(TestName, LogMessage);
        }
        catch (Exception)
        {
            Thread.Sleep(200);
            LogFile1(TestName, LogMessage);
        }
    }
}
