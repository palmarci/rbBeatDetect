using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static rbBeatDetect.VersionManager;

namespace rbBeatDetect
{

    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        VersionManager versionManager = new VersionManager();
        List<VersionManager.OffsetData> supportedOffsets = new List<VersionManager.OffsetData>();
        VersionManager.AppVersion runningVersion = null;

        Thread memoryThread = null;
        MemoryReader memoryReader = null;

      
        private void setBeatColor(int beatNumber)
        {
            if (beatNumber == 1)
            {
                beat1.BackColor = Color.Orange;
                beat2.BackColor = Color.White;
                beat3.BackColor = Color.White;
                beat4.BackColor = Color.White;

            }
            else if (beatNumber == 2)
            {
                beat1.BackColor = Color.White;
                beat2.BackColor = Color.Orange;
                beat3.BackColor = Color.White;
                beat4.BackColor = Color.White;
            }
            else if (beatNumber == 3)
            {
                beat1.BackColor = Color.White;
                beat2.BackColor = Color.White;
                beat3.BackColor = Color.Orange;
                beat4.BackColor = Color.White;
            }
            else if (beatNumber == 4)
            {
                beat1.BackColor = Color.White;
                beat2.BackColor = Color.White;
                beat3.BackColor = Color.White;
                beat4.BackColor = Color.Orange;
            }
            else
            {
                beat1.BackColor = Color.White;
                beat2.BackColor = Color.White;
                beat3.BackColor = Color.White;
                beat4.BackColor = Color.White;

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            masterDeckHelper.Visible = false;
            masterDeckLabel.Visible = false;

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            
            titleLabel.Text += " v" + version.Replace(".0", "");
            errorLabel.Text = "";

            string offsetText = versionManager.getOffsetText();
            
            if (offsetText != null) {
                supportedOffsets = versionManager.parseOffsets(offsetText);
            }

            if (supportedOffsets == null || supportedOffsets.Count > 0)
            {
                errorLabel.Text = "Failed downloading the offsets and could not read the local backup!\r\nPlease check your internet connection!\r\n";
                supportedVersionsHelper.Enabled = false;

            }
            else
            {
                foreach (var ver in supportedOffsets)
                {

                    versionsBox.Items.Add(ver.version.ToString());

                }

            }

            runningVersionLabel.Text = "...";
            runningCheckbox.Enabled = false;

            rbVersionCheckTimer.Start();

        }

        private void turnOffMemoryReader()
        {
            setBeatColor(0);
            masterDeckLabel.Text = "???";
            updateGuiTimer.Stop();
            memoryThread?.Abort();
            memoryReader = null;
            runningCheckbox.Checked = false;
            masterDeckHelper.Visible = false;
            masterDeckLabel.Visible = false;
            oscGroup.Enabled = true;

        }

        private OscClient setupOscClient()
        {
            IPAddress parsedIp;
            if (!IPAddress.TryParse(oscIpAddr.Text, out parsedIp))
            {
                FileManager.log("Failed parsing ip");
                errorLabel.Text = "Failed parsing the IP address.";
                return null;
            }

            var Path = oscPath.Text;
            if (Path[0] == '/')
            {
                Path = Path.Remove(0, 1);
            }

            Path = Path.Replace(" ", "");

            int port;
            if (!int.TryParse(oscPortBox.Text, out port))
            {
                FileManager.log("port parsing failed");
                errorLabel.Text = "Failed parsing the port number.";
                return null;
            }

            return new OscClient(parsedIp, port, Path, oscMimicHuman.Checked, Convert.ToInt32(oscHumanDelay.Value), Convert.ToInt32(oscDelay.Value));

        }

        private void runningCheckbox_CheckedChanged(object sender, EventArgs e)
        {

            if (runningCheckbox.Checked == true)
            {

                errorLabel.Text = "";

                var oscClient = setupOscClient();

                if (oscClient == null)
                {
                    turnOffMemoryReader();
                    return;
                }

                oscGroup.Enabled = false;

                foreach (var data in supportedOffsets)
                {
                    if (data.version.Equals(runningVersion))
                    {
                        memoryReader = new MemoryReader(data, oscClient);
                    }
                }

                if (memoryReader == null)
                {
                    errorLabel.Text = "The current version is not (yet) supported!";
                    turnOffMemoryReader();
                    return;
                }

                memoryThread = new Thread(memoryReader.run);
                memoryThread.IsBackground = true;
                memoryThread.Start();

                updateGuiTimer.Start();
                masterDeckLabel.Visible = true;
                masterDeckHelper.Visible = true;

            }
            else
            {
                turnOffMemoryReader();
            }

        }

        private void rbCheckTimer_Tick(object sender, EventArgs e)
        {
            {
                Process[] pname = Process.GetProcessesByName("rekordbox");
                if (pname.Length != 0)
                {
                    if (runningVersion == null)
                    {
                        FileManager.log("checking for rb.exe...");

                        try
                        {
                            var path = pname.First().MainModule.FileName;
                            var result = versionManager.getRunningVersion(path);
                            if (result == null)
                            {
                                runningCheckbox.Checked = false;
                                runningCheckbox.Enabled = false;
                                runningVersionLabel.Text = "FAIL";

                            }
                            else
                            {
                                runningCheckbox.Enabled = true;
                                runningVersionLabel.Text = result.ToString();
                                runningVersionLabel.ForeColor = Color.Green;

                                runningVersion = result;

                            }
                        }
                        catch (Exception ex)
                        {
                            FileManager.log("exception while getting rekordbox.exe version: " + ex.Message);
                        }

                    }
                }
                else
                {
                    runningCheckbox.Checked = false;
                    runningCheckbox.Enabled = false;
                    runningVersionLabel.ForeColor = Color.Red;
                    runningVersionLabel.Text = "rekordbox is not running...";
                    runningVersion = null;
                }

            }
        }

        private void updateGuiTimer_Tick(object sender, EventArgs e)
        {
            masterDeckLabel.Text = $"{memoryReader.masterDeck}";
            setBeatColor(memoryReader.currentBeatNr);

            if (memoryReader.isCrashed)
            {
                errorLabel.Text = "Memory reader crashed!\r\nDid you set the Master Deck?\r\nDid you load a track to EVERY deck?";
                turnOffMemoryReader();
            }
        }

        private void oscMimicHuman_CheckedChanged(object sender, EventArgs e)
        {
            if (oscMimicHuman.Checked == true)
            {
                oscHumanDelay.Enabled = true;

            }
            else
            {
                oscHumanDelay.Enabled = false;
            }
        }

        private void selfSponsor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/palmarci/rbBeatDetect");
        }

        private void oscDelay_ValueChanged(object sender, EventArgs e)
        {
            if (oscDelay.Value > 500)
            {
                oscDelay.Value = 500;
            }
        }

        private void oscHumanDelay_ValueChanged(object sender, EventArgs e)
        {
            if (oscHumanDelay.Value > 500)
            {
                oscHumanDelay.Value = 500;
            }
        }

    }

}
