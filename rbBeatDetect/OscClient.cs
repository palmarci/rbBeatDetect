using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SharpOSC;
using System.Threading;

namespace rbBeatDetect
{
    public class OscClient
    {

        OscMessage onMsg;
        OscMessage offMsg;
        UDPSender sender;
        bool mimicHuman;
        int humanDelay;
        int delay;

        public OscClient(IPAddress Ip, int Port, string Path, bool MimicHuman, int HumanDelay, int Delay)
        {


            mimicHuman = MimicHuman;
            onMsg = new OscMessage($"/{Path}", 1.0f);
            offMsg = new OscMessage($"/{Path}", 0.0f);
            sender = new UDPSender(Ip.ToString(), Port);
            humanDelay = HumanDelay;
            delay = Delay;

        }

        public void sendMsg()
        {

            if (delay > 0)
            {
                Thread.Sleep(delay);
            }

            sender.Send(onMsg);

            if (mimicHuman)
            {
                Thread.Sleep(humanDelay);
                sender.Send(offMsg);
            }
        }
    }
}
