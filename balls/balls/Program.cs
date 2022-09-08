using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace balls
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread cho = new Thread(new ThreadStart(checkLoopOne));
            cho.Start();
            Thread cht = new Thread(new ThreadStart(checkLoopTwo));
            cht.Start();
            System.Threading.Thread.Sleep(100);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            cho.Abort();
            cht.Abort();
        }
        static void checkLoopOne()
        {
            while (true)
            {
                // check if it's already running https://www.delftstack.com/howto/csharp/csharp-check-if-process-is-running/
                Process[] gwmonone = Process.GetProcessesByName("gwprocmon");
                if (gwmonone.Length == 0)
                {
                    var procMon = new Process();
                    string dirPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                    string targPath = Path.GetFullPath(Path.Combine(dirPath, @"..\"));
                    //targPath += @"\compat\gwprocmon.exe";
                    targPath += @"\gwprocmon.exe";
                    procMon.StartInfo.FileName = targPath;
                    //procMon.StartInfo.UseShellExecute = false;
                    //procMon.StartInfo.RedirectStandardOutput = true;
                    //procMon.StartInfo.RedirectStandardError = true;
                    procMon.Start();
                }
                System.Threading.Thread.Sleep(500);
            }
        }
        static void checkLoopTwo()
        {
            while (true)
            {
                Process[] gwmontwo = Process.GetProcessesByName("gwsupportproc");
                if (gwmontwo.Length == 0)
                {
                    var procMon2 = new Process();
                    string dirPath2 = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                    string targPath2 = Path.GetFullPath(Path.Combine(dirPath2, @"..\"));
                    //targPath2 += @"\compat\gwsupportproc.exe";
                    targPath2 += @"\gwsupportproc.exe";
                    procMon2.StartInfo.FileName = targPath2;
                    procMon2.Start();
                }
                System.Threading.Thread.Sleep(500);
            }
        }
    }
}
