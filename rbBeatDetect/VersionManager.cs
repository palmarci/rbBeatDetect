using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace rbBeatDetect
{
    public class VersionManager
    {
        private string onlineOffsetPath = "https://raw.githubusercontent.com/palmarci/rbBeatDetect/main/offsets.json";

        public List<OffsetData> parseOffsets(string text) {
            List<OffsetData> data = JsonConvert.DeserializeObject<List<OffsetData>>(text);
            return data;
        }

        public static bool IsValidJson(string jsonString)
        {
            try
            {
                JToken.Parse(jsonString);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //tries to download from github repo, if it fails: reads from backup file
        public string getOffsetText()
        {

            try
            {
                var resp = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(onlineOffsetPath);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    resp = reader.ReadToEnd();
                }

  
                if (!IsValidJson(resp))
                {
                    throw new Exception("Invalid JSON from github repo");
                }
                else
                {
                    var minified = Regex.Replace(resp, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1"); //lol
                    FileManager.log("got ok json data, writing it to backup: {" + minified + "}");
                    FileManager.writeBackupOffsets(resp);
                    return resp;

                }

            }
            catch (Exception e)
            {
                FileManager.log($"error downloading offsets: {e.ToString()}, \r\ntrying to read from backup");

                try
                {
                    return FileManager.readBackupOffsets();

                }
                catch (Exception e2)
                {
                    FileManager.log($"failed reading backup file: {e2.ToString()}");
                }

                return null;
            }

        }
        public AppVersion getLatestOnlineVersion() //not used currently, but interesting
         {
             try
             {
                 var url = "https://rekordbox.com/hidden2/rb6_release/apl/check_updater.php";

                 var request = System.Net.WebRequest.Create(url);
                 request.Method = "POST";


                 var postData = "UP_NAME=rekordbox4&UP_VER=6.6.0&UP_LANG=en&OS_TYPE=Windows&OS_VERSION=10-64-64";
                 byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                 request.ContentType = "application/x-www-form-urlencoded";
                 request.ContentLength = byteArray.Length;

                 var reqStream = request.GetRequestStream();
                 reqStream.Write(byteArray, 0, byteArray.Length);

                 var response = request.GetResponse();

                var respStream = response.GetResponseStream();

                 var reader = new StreamReader(respStream);
                 string data = reader.ReadToEnd();
                 FileManager.log("got data from rekordbox.com: " + data);

                 if (data.Contains("STATUS=0"))
                 {
                     Regex word = new Regex(@"UP_VER=([0-9]\.[0-9]\.[0-9]);");
                     Match m = word.Match(data);
                     string finalVersion = m.Groups[1].Value;

                     if (finalVersion.Length == 5)
                     {
                         return new AppVersion(finalVersion);

                     }
                     else
                     {
                         FileManager.log("latest version length mismatch");
                     }

                 }
                 else
                 {
                    FileManager.log("server responded with an error");

                 }

                 return null;

             } catch ( Exception )
             {
                 return null;
             }
         }
        private int extractNumbers(String InputString)
        {
            String Result = "";
            string Numbers = "0123456789";
            int i = 0;

            for (i = 0; i < InputString.Length; i++)
            {
                if (Numbers.Contains(InputString.ElementAt(i)))
                {
                    Result += InputString.ElementAt(i);
                }
            }
            return Convert.ToInt32(Result);
        }
        public AppVersion getRunningVersion(string path)
        {

            FileManager.log("got running path: " + path);

            FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(path);
            var version = fileInfo.FileVersion.Split('.');

            return new AppVersion(extractNumbers(version[0]), extractNumbers(version[1]), extractNumbers(version[2]));


        }
        public class AppVersion
        {
            public AppVersion()
            {

            }

            public AppVersion(int m, int s, int p)
            {
                mainVer = m;
                subVer = s;
                patchVer = p;
            }

            public AppVersion(string str)
            {
                var splits = str.Split('.');
                mainVer = Convert.ToInt32(splits[0]);
                subVer = Convert.ToInt32(splits[1]);
                patchVer = Convert.ToInt32(splits[2]);
            }

            public int mainVer;
            public int subVer;
            public int patchVer;

            public override bool Equals(object obj)
            {
                return obj is AppVersion version &&
                  mainVer == version.mainVer &&
                  subVer == version.subVer &&
                  patchVer == version.patchVer;
            }

            public override string ToString()
            {
                return mainVer + "." + subVer + "." + patchVer;
            }

        }
        public class OffsetData
        {
            public AppVersion version;
            public int deckPointer;
            public int[][] deckOffsets;
            public int masterPointer;
            public int[] masterOffsets;
            public int endOffset;

            public OffsetData()
            {

            }

            public OffsetData(string version, int deckPointer, int[][] deckOffsets, int masterPointer, int[] masterOffsets, int endOffset)
            {
                this.version = new AppVersion(version);
                this.deckPointer = deckPointer;
                this.deckOffsets = deckOffsets;
                this.masterPointer = masterPointer;
                this.masterOffsets = masterOffsets;
                this.endOffset = endOffset;
            }

      

            public override string ToString()
            {
                string str =  "v" + version.ToString() + ", deck pointer: " + this.deckPointer + ", master pointer:" + this.masterPointer + ", master offsets: (";
                foreach (int i in masterOffsets)
                {
                    str += i + ", ";
                }
                str += "), endOffset: " + this.endOffset;
                return str;
            }
        }

    }
}
