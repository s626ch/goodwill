using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Timers;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

namespace gwsupportproc
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Process[] processes = Process.GetProcessesByName("gwprocmon");
                var notepadPlusPlus = new Process();
                if (processes.Length == 0)
                {
                    string dirPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                    //string targPath = Path.GetFullPath(Path.Combine(dirPath, @"..\"));
                    //dirPath += @"\goodwill.exe";
                    //targPath += @"\compat\gwprocmon.exe";
                    dirPath += @"\gwprocmon.exe";
                    notepadPlusPlus.StartInfo.FileName = dirPath;
                    //notepadPlusPlus.StartInfo.FileName = targPath;
                    notepadPlusPlus.Start();
                }
                System.Threading.Thread.Sleep(500);

                //new ManualResetEvent(false).WaitOne();
            }
        }
    }
}