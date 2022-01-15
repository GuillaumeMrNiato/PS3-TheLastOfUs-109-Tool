using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PS3Lib;

namespace Perplex_Theme
{
    public partial class Form1 : Form
    {
        public static PS3API PS3 = new PS3API();
        public Form1()
        {
            InitializeComponent();
        }

        private void perplexButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void perplexButton7_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = ".ELF Files|*.ELF";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.SafeFileName != "EBOOT.ELF")
                {
                    MessageBox.Show("Your .elf needs to be named EBOOT.elf!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    textBox2.Text = openFileDialog1.FileName;
                    MessageBox.Show(openFileDialog1.SafeFileName + " successfully loaded!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    perplexButton8.Enabled = true;
                }
            }
            else
            {
            }
        }

        private void perplexButton8_Click(object sender, EventArgs e)
        {
            if (!File.Exists("EBOOT.ELF"))
            {
                MessageBox.Show("EBOOT.ELF not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                File.Copy("EBOOT.ELF", "EBOOT.BIN");
            }

            if (File.Exists("EBOOT.BIN"))
            {
                if (File.Exists("EBOOT.BIN.bak"))
                {
                    File.Delete("EBOOT.BIN.bak");
                }
                else
                {
                    File.Copy("EBOOT.BIN", "EBOOT.BIN.bak");
                }
            }
            if (File.Exists("Debug.bat"))
            {
                System.Threading.Thread loading = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));
                Process p = new Process();
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.FileName = "Debug.bat";
                p.Start();
                MessageBox.Show("EBOOT successfully created!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("You're missing the targeted file! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void ThreadProc()
        {
        }

        private void perplexCheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (perplexCheckBox8.Checked)
            {
                numericUpDown1.Enabled = true;
                perplexButton4.Enabled = true;
            }
            else
            {
                numericUpDown1.Enabled = false;
                perplexButton4.Enabled = false;
            }
        }

        private void perplexButton5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void perplexButton6_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            perplexProgressBar1.Value = Convert.ToInt32(perplexProgressBar1.Value) + 50;
            if (Convert.ToInt32(perplexProgressBar1.Value) > 0)
            {
                timer1.Enabled = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            perplexProgressBar1.Value = Convert.ToInt32(perplexProgressBar1.Value) + 50;
            if (Convert.ToInt32(perplexProgressBar1.Value) > 0)
            {
                timer2.Enabled = false;
            }
        }

        private void perplexButton2_Click(object sender, EventArgs e)
        {
            if (PS3.ConnectTarget())
            {
                string Message = "You are now connected with this API : " + PS3.GetCurrentAPIName();
                MessageBox.Show(Message, "Connected!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                timer1.Start();
            }

            else
            {
                string Message = "Impossible to connect :/";
                MessageBox.Show(Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void perplexButton3_Click(object sender, EventArgs e)
        {
            if (PS3.AttachProcess())
            {
                MessageBox.Show("Current game is attached successfully.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                timer2.Start();
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
            }
            else
            {
                MessageBox.Show("No game process found!", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void perplexRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            PS3.ChangeAPI(SelectAPI.TargetManager);
        }

        private void perplexRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            PS3.ChangeAPI(SelectAPI.ControlConsole);
        }

        private void perplexCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (perplexCheckBox1.Checked)
            {
                byte[] buffer = new byte[] { 0xFF, };
                PS3.SetMemory((uint)0x4c91c0, buffer);
            }
            else
            {
                byte[] buffer = new byte[] { 0x00, };
                PS3.SetMemory((uint)0x4c91c0, buffer);
            }
        }

        private void perplexCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (perplexCheckBox2.Checked)
            {
                byte[] buffer = new byte[] { 60, 0, 0x40, 0x40 };
                PS3.SetMemory((uint)0x6bf374, buffer);
            }
            else
            {
                byte[] buffer2 = new byte[] { 60, 0, 0x3f, 0x80 };
                PS3.SetMemory((uint)0x6bf374, buffer2);
            }
        }

        private void perplexCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (perplexCheckBox4.Checked)
            {
                byte[] buffer3 = new byte[4];
                buffer3[0] = 0x60;
                byte[] buffer = buffer3;
                PS3.SetMemory((uint)0x6a9530, buffer);
            }
            else
            {
                byte[] buffer2 = new byte[] { 0xb1, 60, 5, 0x74 };
                PS3.SetMemory((uint)0x6a9530, buffer2);
            }
        }

        private void perplexCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (perplexCheckBox3.Checked)
            {
                byte[] buffer3 = new byte[4];
                buffer3[0] = 0x60;
                byte[] buffer = buffer3;
                PS3.SetMemory((uint)0x336b0, buffer);
            }
            else
            {
                byte[] buffer2 = new byte[] { 0x7c, 11, 0x4b, 0x2e };
                PS3.SetMemory((uint)0x336b0, buffer2);
            }
        }

        private void perplexCheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (perplexCheckBox7.Checked)
            {
                byte[] buffer = new byte[] { 0xb1, 0x3f, 5, 0x74 };
                PS3.SetMemory((uint)0x6b0864, buffer);
                byte[] buffer2 = new byte[] { 0xb1, 0x3f, 5, 0x74 };
                PS3.SetMemory((uint)0x6b0864, buffer2);
            }
            else
            {
                byte[] buffer3 = new byte[] { 0xb1, 0x3f, 5, 0x74 };
                PS3.SetMemory((uint)0x6b0864, buffer3);
                byte[] buffer4 = new byte[] { 0xb1, 0x3f, 5, 0x74 };
                PS3.SetMemory((uint)0x6b0864, buffer4);
            }
        }

        private void perplexCheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (perplexCheckBox6.Checked)
            {
                byte[] buffer = new byte[] { 0x60, };
                PS3.SetMemory((uint)0x5328f47, buffer);
            }
            else
            {
                byte[] buffer3 = new byte[] { 0xB1, 60, 5, 0x74 };
                PS3.SetMemory((uint)0x5328f47, buffer3);
            }
        }

        private void perplexCheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (perplexCheckBox5.Checked)
            {
                byte[] buffer3 = new byte[4];
                buffer3[0] = 60;
                byte[] buffer = buffer3;
                PS3.SetMemory((uint)0xa7d768, buffer);
            }
            else
            {
                byte[] buffer2 = new byte[] { 60, 0, 0x3f, 0x80 };
                PS3.SetMemory((uint)0xa7d768, buffer2);
            }
        }

        private void perplexButton4_Click(object sender, EventArgs e)
        {
            PS3.SetMemory(0x2f7c20, BitConverter.GetBytes((int)numericUpDown1.Value));
        }

        private void perplexButton9_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://b0.ww.np.dl.playstation.net/tppkg/np/BCES01584/BCES01584_T10/6025c8e8b0aa7c4f/EP9000-BCES01584_00-LASTOFUSPATCH109-A0109-V0100-PE.pkg");
        }

        private void perplexButton10_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://b0.ww.np.dl.playstation.net/tppkg/np/BCES01584/BCUS01584_T10/6025c8e8b0aa7c4f/EP9000-BCUS01584_00-LASTOFUSPATCH109-A0109-V0100-PE.pkg");
        }

        private void perplexButton12_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.mediafire.com/?z2od9c54lgnhodk");
        }

        private void perplexButton11_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.mediafire.com/?pl97ds4t2z9bm97");
        }

        private void perplexTheme1_Click(object sender, EventArgs e)
        {

        }

        private void helloToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void perplexGroupBox2_Click(object sender, EventArgs e)
        {

        }

        private void perplexButton5_Click_1(object sender, EventArgs e)
        {
            PS3.SetMemory(0x43FDDEDC, BitConverter.GetBytes((int)numericUpDown2.Value));
        }
    }
}
    
        
