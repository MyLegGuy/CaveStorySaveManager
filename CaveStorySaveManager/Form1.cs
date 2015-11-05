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

namespace CaveStorySaveManager
{
    public partial class Form1 : Form
    {

        OpenFileDialog ofd = new OpenFileDialog();

        public Form1()
        {
            InitializeComponent();
            Debug.WriteLine(Application.StartupPath);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ofd.ShowDialog();
            BinaryReader br = new BinaryReader(File.OpenRead(ofd.FileName));
            string files = "";

            //0x is for offset

            //for (int i = 0x1F020; i<= 0x1F021; i++)
            br.BaseStream.Position = 0x1f020;
            files = br.ReadByte().ToString("X2");

            switch(files)
            {
                case "00":
                    label1.Text = "You don't have any saves!";
                    break;
                case "01":
                    label1.Text = "You have a save in slot 1.";
                    break;
                case "07":
                    label1.Text = "You have a save in all the slots!";
                     break;
                case "02":
                    label1.Text = "You have a save in the second slot!";
                    break;
                case "03":
                    label1.Text = "You have saves is the first and second slots!";
                    break;
                case "04":
                    label1.Text = "You have a save in the third slot!";
                    break;
                case "05":
                    label1.Text = "You have saves in the first and third slots!";
                    break;
                case "06":
                    label1.Text = "You have saves in the second and third slots!";
                    break;
            }

                    br.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(textBox1.Text);
            ofd.ShowDialog();
            int happyint=0x0;
            int angryint = 0x1;
            switch (textBox1.Text)
            {

                case "1":
                    happyint = 0x0;
                    angryint = 0x610;
                    break;
                case "2":
                    happyint = 0x620;
                    angryint = 0xC30;
                    break;
                case "3":
                    happyint = 0xC40;
                    angryint = 0x1250;
                    break;
                default:
                    MessageBox.Show("Not a valid save file number.\nValid choices are 1, 2, and 3.");
                    return;
                    
            }

            BinaryReader br = new BinaryReader(File.OpenRead(ofd.FileName));
            //BinaryWriter bw = new BinaryWriter(File.OpenWrite(Application.StartupPath+"/"+textBox1.Text+"file.dat"));
            BinaryWriter bw = new BinaryWriter(File.Create(Application.StartupPath + "/" + textBox1.Text + "file.dat"));
            bw.BaseStream.Position = 0x0;
            br.BaseStream.Position = happyint;
            for (int i = happyint; i < angryint; i++)
            {
                //br.BaseStream.Position = i;
                bw.Write(br.ReadByte());
               // bw.BaseStream.Position += 0x1;
            }
            bw.Close();
            br.Close();

        }

    }
}
