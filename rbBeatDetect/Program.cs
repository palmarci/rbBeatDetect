using Newtonsoft.Json;
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
using static rbBeatDetect.VersionManager;

namespace rbBeatDetect
{


    static class Program
    {

        [STAThread]



        static void fasz() {
            // 0xf0
            //  0xf8, 0x28, 0x0
            //  0x100, 0x28, 0x0 
            //   0x108, 0x28, 0x0 


            //List<Person> personList = JsonConvert.DeserializeObject<List<Person>>(jsonString);
            int[][] deckOffsets = new int[4][];
            deckOffsets[0] = new int[] { 0xf0, 0x28, 0x0 };
            deckOffsets[1] = new int[] { 0xf8, 0x28, 0x0 };
            deckOffsets[2] = new int[] { 0x100, 0x28, 0x0 };
            deckOffsets[3] = new int[] { 0x108, 0x28, 0x0 };


            OffsetData a = new OffsetData("6.5.1", 0x03ff44a8, deckOffsets, 0x04006EB0, new int[] { 0x200, 0x19C }, 0x245);
            OffsetData b = new OffsetData("6.6.4", 0x03f72180, deckOffsets, 0x03F85360, new int[] { 0x30, 0x19C }, 0x245);
            var asd = new List<OffsetData>();
            asd.Add(a);
            asd.Add(b);


            //6.5.1; 0x03ff44a8; 0x04006EB0; 0x200,0x19C
            //6.6.4; 0x03f72180; 0x03F85360; 0x30,0x19C   
            string jsonString = JsonConvert.SerializeObject(asd);
            Console.WriteLine(jsonString);
        }

        static void Main()
        {
            try
            {
                fasz();
           //    var asd = new int[] { 0xf0, 0x28, 0x0 }) +0x245c;
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