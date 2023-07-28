using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static rbBeatDetect.VersionManager;

namespace rbBeatDetect
{
    public class MemoryReader
    {

        private int maxErrors = 25;


        private readonly int PROCESS_WM_READ = 0x10;

        private IntPtr currentHandle = IntPtr.Zero;
        private IntPtr moduleBase = IntPtr.Zero;

        public Int64 masterAddress = 0;
        public List<Int64> deckAdresses = new List<Int64> { 0, 0, 0, 0 };
        public int masterDeck = 0;
        public int currentBeatNr = 0;
        public bool isCrashed = false;
        private OscClient osc;
        private int errorCount = 0;

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(int hProcess, Int64 lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        public MemoryReader(OffsetData offsetData, OscClient oscClient)
        {
            Process process = Process.GetProcessesByName("rekordbox")[0];
            currentHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);
            FileManager.log("current handle = " + currentHandle);
            isCrashed = false;

            foreach (ProcessModule m in process.Modules)
            {
                if (m.ModuleName == "rekordbox.exe")
                {
                    moduleBase = m.BaseAddress;
                    FileManager.log("modulebase = 0x" + moduleBase.ToString("X"));
                    break;
                }
            }

            masterAddress = FindDMAAddy(IntPtr.Add(moduleBase, offsetData.masterPointer), offsetData.masterOffsets.ToArray());
            deckAdresses[0] = FindDMAAddy(IntPtr.Add(moduleBase, offsetData.deckPointer), offsetData.deckOffsets[0].ToArray()) + offsetData.endOffset;
            deckAdresses[1] = FindDMAAddy(IntPtr.Add(moduleBase, offsetData.deckPointer), offsetData.deckOffsets[1].ToArray()) + offsetData.endOffset;
            deckAdresses[2] = FindDMAAddy(IntPtr.Add(moduleBase, offsetData.deckPointer), offsetData.deckOffsets[2].ToArray()) + offsetData.endOffset;
            deckAdresses[3] = FindDMAAddy(IntPtr.Add(moduleBase, offsetData.deckPointer), offsetData.deckOffsets[3].ToArray()) + offsetData.endOffset;
            FileManager.log("master address: " + masterAddress.ToString());
            FileManager.log("deck addresses: " + deckAdresses.ToString());


            //todo: rekordbox v5 compatibilty (0xb0, 0x158, +0x1C9C deck offset (???))

            this.osc = oscClient;

        }

        private Int64 FindDMAAddy(IntPtr ptr, int[] offsets)
        {

            var buffer = new byte[IntPtr.Size];

            foreach (int i in offsets)
            {
                int read = 0;
                ReadProcessMemory(currentHandle.ToInt32(), ptr.ToInt64(), buffer, buffer.Length, ref read);
                ptr = (IntPtr.Size == 4) ? IntPtr.Add(new IntPtr(BitConverter.ToInt32(buffer, 0)), i) : ptr = IntPtr.Add(new IntPtr(BitConverter.ToInt64(buffer, 0)), i);
            }
            return ptr.ToInt64();
        }

        private int readByteFromMemory(Int64 offset)
        {
            byte[] buffer = new byte[1];
            int bytesRead = 0;
            ReadProcessMemory((int)currentHandle, offset, buffer, buffer.Length, ref bytesRead);
            return Convert.ToInt32(buffer[0]);

        }

        public void run()
        {
            int lastBeat = -1;

            while (true)
            {
                if (errorCount > maxErrors)
                {
                    FileManager.log("too many errors, self crashing...");
                    isCrashed = true;
                    return;
                }

                masterDeck = readByteFromMemory(masterAddress) + 1;

                if (masterDeck < 1 || masterDeck > 4)
                {
                    errorCount += 1;
                    FileManager.log("read error! masterdeck is out of valid range");

                }
                else
                {
                    currentBeatNr = readByteFromMemory(deckAdresses[masterDeck - 1]);

                }

                if (currentBeatNr > 4 || currentBeatNr < 1)
                {
                    errorCount += 1;
                    FileManager.log("read error! beat number is out of valid range");

                }

                if (lastBeat != currentBeatNr)
                {
                    lastBeat = currentBeatNr;
                    new Thread(() => {
                        Thread.CurrentThread.IsBackground = true;
                        osc.sendMsg();
                    }).Start();

                }

                Thread.Sleep(1);
            }
        }

    }
}