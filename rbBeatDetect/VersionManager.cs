using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace rbBeatDetect
{
    public class VersionManager
    {
        private string onlineUpdateUrl = "https://raw.githubusercontent.com/palmarci/rbBeatDetect/main/offsets.txt";

        public List<OffsetData> parseOffsetData(string text)
        {
            List<OffsetData> toReturn = new List<OffsetData>();

            var lines = text.Split('\n');

            foreach (var line in lines)
            {
                var tags = line.Split(';');
                var data = new OffsetData();

                if (tags.Length != 4)
                {
                    FileManager.log("skipping line '" + line + "'");
                }
                else
                {
                    data.update = new UpdateVersion(tags[0]);
                    data.deckPointer = Convert.ToInt32(tags[1], 16);
                    data.masterPointer = Convert.ToInt32(tags[2], 16);

                    var offsets = tags[3].Split(',');

                    data.masterOffsets = new List<int>() { };

                    foreach (var offset in offsets)
                    {
                        data.masterOffsets.Add(Convert.ToInt32(offset, 16));
                    }
                    toReturn.Add(data);

                }
            }
            return toReturn;
        }
        public List<OffsetData> getOffsets()
        {

            try
            {
                var resp = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(onlineUpdateUrl);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    resp = reader.ReadToEnd();
                }

                FileManager.log("got online data, writing it to backup: {" + resp + "}");

                FileManager.writeBackupOffsets(resp);

                return parseOffsetData(resp);

            }
            catch (Exception e)
            {
                FileManager.log($"error downloading offsets: {e}, trying to read from backup");

                try
                {
                    return parseOffsetData(FileManager.readBackupOffsets());

                }
                catch (Exception e2)
                {
                    FileManager.log($"failed reading backup file: {e2}");
                }

                return null;
            }

        }

        /* public UpdateVersion getRekordboxVersion()
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
                 //     addDebugText(((System.Net.HttpWebResponse)response).StatusDescription);

                 var respStream = response.GetResponseStream();

                 var reader = new StreamReader(respStream);
                 string data = reader.ReadToEnd();
                 Console.WriteLine("got data from pioneer: " + data);

                 if (data.Contains("STATUS=0"))
                 {
                     Regex word = new Regex(@"UP_VER=([0-9]\.[0-9]\.[0-9]);");
                     Match m = word.Match(data);
                     string finalVersion = m.Groups[1].Value;

                     if (finalVersion.Length == 5)
                     {
                         return new UpdateVersion(finalVersion);
                     //    var asd = UpdateVersion(finalVersion);

                     }
                     else
                     {
                         Console.WriteLine("latest version length mismatch");
                         //          return (false, );
                     }

                 }
                 else
                 {
                     Console.WriteLine("server responded with an error");

                 }

                 return null;

             } catch ( Exception )
             {
                 return null;
             }
         }


         */

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

        public UpdateVersion getRunningVersion(string path)
        {

            FileManager.log("got running path: " + path);

            FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(path);
            var version = fileInfo.FileVersion.Split('.');

            return new UpdateVersion(extractNumbers(version[0]), extractNumbers(version[1]), extractNumbers(version[2]));


        }

        public class UpdateVersion
        {

            public UpdateVersion(int m, int s, int p)
            {
                mainVer = m;
                subVer = s;
                patchVer = p;
            }

            public UpdateVersion(string str)
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
                return obj is UpdateVersion version &&
                  mainVer == version.mainVer &&
                  subVer == version.subVer &&
                  patchVer == version.patchVer;
            }

            public override string ToString()
            {
                return mainVer + "." + subVer + "." + patchVer;
            }

            public override int GetHashCode() //automatically generated
            {
                int hashCode = -467722179;
                hashCode = hashCode * -1521134295 + mainVer.GetHashCode();
                hashCode = hashCode * -1521134295 + subVer.GetHashCode();
                hashCode = hashCode * -1521134295 + patchVer.GetHashCode();
                return hashCode;
            }
        }
        public class OffsetData
        {
            public UpdateVersion update;
            public int deckPointer;
            public int masterPointer;
            public List<int> masterOffsets;

            public OffsetData()
            {

            }

            public OffsetData(string supportedVersion, int dP, int mP, List<int> mOfs)
            {
                update = new UpdateVersion(supportedVersion);
                dP = deckPointer;
                mP = masterPointer;
                mOfs = masterOffsets;
            }

            public override bool Equals(object obj)
            {
                return obj is OffsetData data &&
                  deckPointer == data.deckPointer &&
                  masterPointer == data.masterPointer &&
                  EqualityComparer<List<int>>.Default.Equals(masterOffsets, data.masterOffsets);
            }

            public override string ToString()
            {
                return "v" + update.ToString();
            }
        }

    }
}
