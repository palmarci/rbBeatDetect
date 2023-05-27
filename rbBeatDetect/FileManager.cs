using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static rbBeatDetect.VersionManager;

namespace rbBeatDetect
{
    class FileManager
    {

        public static String dataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\rbBeatDetect\";
        public static String offsetBackupPath = dataFolderPath + "offsets.bak";
        public static String logFilePath = dataFolderPath + "log.txt";

        public static void initialize()
        {
            if (!Directory.Exists(dataFolderPath))
            {
                Directory.CreateDirectory(dataFolderPath);
            }

            if (File.Exists(logFilePath))
            {
                File.Delete(logFilePath);
            }

            File.Create(logFilePath).Dispose();
            log("program started at " + DateTime.Now);

        }

        public static void log(String msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(msg + "\r\n");
            File.AppendAllText(logFilePath, sb.ToString());
            sb.Clear();
        }

        public static string readBackupOffsets()
        {
            return File.ReadAllText(offsetBackupPath);

        }

        public static void writeBackupOffsets(string data)
        {

            using (StreamWriter sW = new StreamWriter(offsetBackupPath, false))
            {
                sW.Write(data);
            }

        }
    }

}
