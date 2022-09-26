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

namespace rbBeatDetect
{

    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        VersionManager vManager = new VersionManager();
        List<VersionManager.OffsetData> supportedVersions = new List<VersionManager.OffsetData>();
        VersionManager.OffsetData selectedVersion = new VersionManager.OffsetData();
        private bool weHaveRunningVersion = false;
      //  private string currentProgramVersion = "v2.0";
        Thread memoryThread = null;

        MemoryReader mReader;


        private static string intToHex(int num)
        {
            string hex = num.ToString("X");

            return "0x" + hex;
        }

        private void fillVersionBoxes(VersionManager.OffsetData oData)
        {
            selectedVersion = oData;
            deckPointerBox.Text = intToHex(oData.deckPointer);
            masterPointerBox.Text = intToHex(oData.masterPointer);
            var offsetString = "";
            foreach (var offset in oData.masterOffsets)
            {
                offsetString += intToHex(offset) + ", ";
            }
            offsetString = offsetString.TrimEnd(',', ' ');
            masterOffsetsBox.Text = offsetString;
        }

        private void updateAutoSelect()
        {
            try
            {
                var currentVer = new VersionManager.UpdateVersion(runningVersionLabel.Text);
                for (int i = 0; i < supportedVersions.Count(); i++)
                {
                    if (currentVer.Equals(supportedVersions[i].update))
                    {
                        versionBox.SelectedIndex = i + 1;

                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("preventing crash while auto selecting version: " + e.ToString());
            }

        }

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


            versionErrorLabel.Text = "";
            errorLabel.Text = "";

            supportedVersions = vManager.getOnlineOffsets();
            versionBox.Items.Add("Manual config");


            if (supportedVersions == null)
            {
                versionErrorLabel.Text = "Failed downloading offsets!\r\nCheck your internet connection?";
                autoSelectVersion.Enabled = false;
                supportedVersionsHelper.Enabled = false;
                versionBox.Enabled = false;
                deckPointerHelper.Enabled = true;
                masterPointerHelper.Enabled = true;
                masterOffsetsHelper.Enabled = true;

            }
            else
            {
                foreach (var ver in supportedVersions)
                {
                    versionBox.Items.Add(ver);
                }

            }


            versionBox.SelectedIndex = 0;
            runningVersionLabel.Text = "...";

            runningCheckTimer.Start();
            rbCheckTimer.Start();

        }

        private void autoSelectVersion_CheckedChanged(object sender, EventArgs e)
        {
            if (autoSelectVersion.Checked)
            {
                versionBox.Enabled = false;
                updateAutoSelect();
            }
            else
            {
                versionBox.Enabled = true;
                versionBox.SelectedIndex = 0;
            }
        }

        private void versionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (versionBox.SelectedIndex == 0)
            {
                masterOffsetsBox.Enabled = true;
                deckPointerBox.Enabled = true;
                masterPointerBox.Enabled = true;
                masterOffsetsBox.Text = "";
                deckPointerBox.Text = "";
                masterPointerBox.Text = "";
                selectedVersion = null;

            }
            else
            {
                masterOffsetsBox.Enabled = false;
                deckPointerBox.Enabled = false;
                masterPointerBox.Enabled = false;
                fillVersionBoxes(supportedVersions[versionBox.SelectedIndex - 1]);


            }



        }

        private void turnOffMemoryReader()
        {
            setBeatColor(0);
            masterDeckLabel.Text = "???";
            updateGuiTimer.Stop();
            memoryThread?.Abort();
            mReader = null;
            runningCheckbox.Checked = false;
            masterDeckHelper.Visible = false;
            masterDeckLabel.Visible = false;
            oscGroup.Enabled = true;

        }

        
        private OscClient validateOsc()
        {
            IPAddress parsedIp;
            if (!IPAddress.TryParse(oscIpAddr.Text, out parsedIp))
            {
                Console.WriteLine("Failed parsing ip");
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
                Console.WriteLine("port parsing failed");
                errorLabel.Text = "Failed parsint the port.";
                return null;
            }


            return new OscClient(parsedIp, port, Path, oscMimicHuman.Checked, Convert.ToInt32(oscHumanDelay.Value), Convert.ToInt32(oscDelay.Value));

        }


        private void runningCheckbox_CheckedChanged(object sender, EventArgs e)
        {

            if (runningCheckbox.Checked == true)
            {

                errorLabel.Text = "";

                var oscClient = validateOsc();

                if (oscClient == null)
                {
              //      errorLabel.Text = "Failed while parsing";
                    turnOffMemoryReader();
                    return;
                }

                oscGroup.Enabled = false;

                mReader = new MemoryReader(selectedVersion, oscClient);
       

                memoryThread = new Thread(mReader.run);
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

        private void runningCheckTimer_Tick(object sender, EventArgs e)
        {
            if (!weHaveRunningVersion || selectedVersion == null)
            {
                runningCheckbox.Checked = false;
                runningCheckbox.Enabled = false;
            }
            else
            {
                runningCheckbox.Enabled = true;
            }

        }

        private void rbCheckTimer_Tick(object sender, EventArgs e)
        {
            {
                Process[] pname = Process.GetProcessesByName("rekordbox");
                if (pname.Length != 0)
                {
                    if (!weHaveRunningVersion)
                    {
                        Console.WriteLine("checking for rb.exe...");

                        var path = pname.First().MainModule.FileName;
                        var result = vManager.getRunningVersion(path);

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

                            weHaveRunningVersion = true;

                            if (autoSelectVersion.Checked)
                            {
                                updateAutoSelect();
                            }

                        }

                    }
                }
                else
                {
                    runningCheckbox.Checked = false;
                    runningCheckbox.Enabled = false;
                    runningVersionLabel.ForeColor = Color.Red;
                    runningVersionLabel.Text = "rekordbox is not running...";
                    weHaveRunningVersion = false;
                }

            }
        }

        private void parseManualInput()
        {
            if (versionBox.SelectedIndex != 0)
            {
                return;
            }

            try
            {
                var str = "0.0.0;" + deckPointerBox.Text + ";" + masterPointerBox.Text + ";" + masterOffsetsBox.Text;
                var parsed = vManager.parseTextToOffsetDatas(str);

                if (parsed.Count > 0)
                {
                    Console.WriteLine("parsed manual input");
                    selectedVersion = parsed[0];

                }
                else
                {
                    selectedVersion = null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("failed parsing: " + e);
                selectedVersion = null;

            }
        }

        private void deckPointerBox_TextChanged(object sender, EventArgs e)
        {
            parseManualInput();
        }

        private void masterPointerBox_TextChanged(object sender, EventArgs e)
        {
            parseManualInput();
        }

        private void masterOffsetsBox_TextChanged(object sender, EventArgs e)
        {
            parseManualInput();
        }

        private void updateGuiTimer_Tick(object sender, EventArgs e)
        {
            masterDeckLabel.Text = $"{mReader.masterDeck}";
            setBeatColor(mReader.currentBeatNr);

            if (mReader.isCrashed)
            {
                errorLabel.Text = "Memory reader crashed!\r\nDid you set the master deck?\r\nAre the decks initialized?";
                turnOffMemoryReader();
            }
        }

        private void oscMimicHuman_CheckedChanged(object sender, EventArgs e)
        {
            if (oscMimicHuman.Checked == true)
            {
                oscHumanDelay.Enabled = true;
            } else
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