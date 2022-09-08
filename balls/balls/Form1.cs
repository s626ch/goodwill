using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace balls
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("fart");
            Thread tb = new Thread(new ThreadStart(cursorLoop));
            tb.Start();
            Thread ba = new Thread(new ThreadStart(BlockInputForVid));
            ba.Start();
            thriftLoop();
            System.Threading.Thread.Sleep(100);
        }
        private void thriftLoop()
        {
            while (true)
            {
                button1.Text = "THRIFTING";
                System.Diagnostics.Process.Start("https://1000logos.net/wp-content/uploads/2018/08/Goodwill-Logo.png");
                System.Threading.Thread.Sleep(50);
                button1.Text = "THRIFTING.";
                System.Diagnostics.Process.Start("https://1000logos.net/wp-content/uploads/2018/08/Goodwill-Logo.png");
                System.Threading.Thread.Sleep(50);
                button1.Text = "THRIFTING..";
                System.Diagnostics.Process.Start("https://1000logos.net/wp-content/uploads/2018/08/Goodwill-Logo.png");
                System.Threading.Thread.Sleep(50);
                button1.Text = "THRIFTING...";
                System.Diagnostics.Process.Start("https://1000logos.net/wp-content/uploads/2018/08/Goodwill-Logo.png");
                System.Threading.Thread.Sleep(50);
            }
        }
        private void cursorLoop()
        {
            var w = System.Windows.SystemParameters.PrimaryScreenWidth;
            var h = System.Windows.SystemParameters.PrimaryScreenHeight;
            while (true)
            {
                Random rnd = new Random();
                var rw = rnd.Next(0, (int)w);
                var rh = rnd.Next(0, (int)h);
                System.Windows.Forms.Cursor.Position = new Point(rw, rh);
                System.Threading.Thread.Sleep(50);
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void cursorLock()
        {
            while (true) { 
                System.Windows.Forms.Cursor.Position = new Point(0, 0);
            }
        }
        private void killBrowser()
        {
            // get chrome
            var chrome = Process.GetProcesses().
                Where(pr => pr.ProcessName == "chrome");
            // get edge
            var edge = Process.GetProcesses().
                Where(pr =>pr.ProcessName == "msedge");
            // get firefox
            var firefox = Process.GetProcesses().
                Where(pr => pr.ProcessName == "firefox");
            // get firefox
            var opera = Process.GetProcesses().
                Where(pr => pr.ProcessName == "opera");
            // kill all
            foreach (var process in chrome){
                process.Kill();
            }
            foreach (var process in edge)
            {
                process.Kill();
            }
            foreach (var process in firefox)
            {
                process.Kill();
            }
            foreach (var process in opera)
            {
                process.Kill();
            }
            // no one uses IE lolololololo
        }
        private void BlockInputForVid()
        {
            while (true)
            {
                SendKeys.SendWait("^+{F1}");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            killBrowser();
            System.Threading.Thread.Sleep(100);
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=6_c2ZLCr5Nk");
            System.Threading.Thread.Sleep(5000);
            SendKeys.Send("{ESC}");
            System.Threading.Thread.Sleep(100);
            SendKeys.Send(" ");
            System.Threading.Thread.Sleep(100);
            SendKeys.Send("F");
            System.Threading.Thread.Sleep(100);
            Thread cl = new Thread(new ThreadStart(cursorLock));
            cl.Start();
            Thread bi = new Thread(new ThreadStart(BlockInputForVid));
            bi.Start(); 
            //BlockInputForVid();
            System.Threading.Thread.Sleep(12300000); // 3 hours and 25 minutes https://www.codeproject.com/Questions/1111993/How-to-disable-specific-key
            bi.Abort();
            killBrowser();
        }
        // goodwill button
        private void button2_Click(object sender, EventArgs e)
        {
            bool IsFine = true;
            _ = Directory.CreateDirectory(@"c:\temp");
            try
            {
                SaveImage("https://is5-ssl.mzstatic.com/image/thumb/Purple115/v4/54/66/7a/54667a89-7eb3-f298-6594-39b8c928029d/AppIcon-0-0-1x_U007emarketing-0-0-0-10-0-0-sRGB-0-0-0-GLES2_U002c0-512MB-85-220-0-0.png/1200x600wa.png", @"c:\temp\goodwill.png", ImageFormat.Png);
                Console.WriteLine("Image written successfully at specified location.");
            }
            catch (ExternalException)
            {
                Console.WriteLine("Either the format isn't right,\nor the file could not be written\nat the specified location.");
                IsFine = false;
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Yeah I don't know, I just can't download it.");
                IsFine = false;
            }
            System.Threading.Thread.Sleep(4000);
            if (IsFine)
            {
                //convert image https://villavu.com/forum/showthread.php?t=48949
                if (File.Exists(@"c:\temp\goodwill.png"))
                {
                    Image Dummy = Image.FromFile(@"c:\temp\goodwill.png");
                    Dummy.Save(@"c:\temp\goodwill.bmp");
                    // attempt to set https://www.codeproject.com/Questions/1252479/How-do-I-change-the-desktop-background-using-Cshar
                    // oh also this is looping the image being set, because it's funny
                    Thread gwp = new Thread(new ThreadStart(GoodWillWall));
                    gwp.Start();
                }
            }
        }
        // https://stackoverflow.com/questions/24797485/how-to-download-image-from-url
        private void SaveImage(string imageUrl, string filename, ImageFormat format)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(imageUrl);
            Bitmap bitmap; bitmap = new Bitmap(stream);

            if (bitmap != null)
            {
                bitmap.Save(filename, format);
            }

            stream.Flush();
            stream.Close();
            client.Dispose();
        }
        // loop set
        private void GoodWillWall()
        {
            while (true)
            {
                string photo = @"c:\temp\goodwill.bmp";
                DisplayPicture(photo);
                System.Threading.Thread.Sleep(500);
            }
        }
        // attempt to set https://www.codeproject.com/Questions/1252479/How-do-I-change-the-desktop-background-using-Cshar
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, String pvParam, uint fWinIni);

        private const uint SPI_SETDESKWALLPAPER = 0x14;
        private const uint SPIF_UPDATEINIFILE = 0x1;
        private const uint SPIF_SENDWININICHANGE = 0x2;

        private static void DisplayPicture(string file_name)
        {
            uint flags = 0;
            if (!SystemParametersInfo(SPI_SETDESKWALLPAPER,
                    0, file_name, flags))
            {
                Console.WriteLine("Error");
            }
        }
    }
}
