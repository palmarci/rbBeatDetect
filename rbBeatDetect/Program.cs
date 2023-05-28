using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rbBeatDetect
{


    static class Program
    {

        [STAThread]



        static void Main()
        {
            try
            {
                FileManager.initialize();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Menu());
            } catch (Exception e) { //this is a bit sketchy lol
                FileManager.log(e.ToString());
                throw;
            }
        }
    }
}