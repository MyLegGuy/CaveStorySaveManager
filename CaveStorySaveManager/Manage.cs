using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaveStorySaveManager
{
    class Manage
    {
        
    	public static void injectSave(string profilefilepath, string injectfilepath, int filenumber)
    	{
            BinaryReader br = new BinaryReader(File.OpenRead(injectfilepath));
            BinaryWriter bw = new BinaryWriter(File.OpenWrite(profilefilepath));

            int happyint = 0x0;
            int angryint = 0x1;
            switch (filenumber)
            {

                case 1:
                    happyint = 0x0;
                    angryint = 0x610;
                    break;
                case 2:
                    happyint = 0x620;
                    angryint = 0xC30;
                    break;
                case 3:
                    happyint = 0xC40;
                    angryint = 0x1250;
                    break;
                default:
                    MessageBox.Show("Not a valid save file number.\nValid choices are 1, 2, and 3.\nThis shouldn't be possible for this to happen.\nYou're bad at making this better.");
                    return;
            }

            br.BaseStream.Position = 0x0;
            bw.BaseStream.Position = happyint;
            for (int i = happyint; i < angryint; i++)
            {
                bw.Write(br.ReadByte());
            }
            bw.Close();
            br.Close();

            MessageBox.Show("Done. Save injected into save slot "+filenumber+".");

            return;
        }

        static int getSaves(string filepath, int affected, bool gb)
        {
            bool[] hasSaves = new bool[5];

            BinaryReader br = new BinaryReader(File.OpenRead(filepath));
            string files = "";

            br.BaseStream.Position = 0x1f020;
            files = br.ReadByte().ToString("X2");

            hasSaves[1] = false;
            hasSaves[2] = false;
            hasSaves[3] = false;

            switch (files)
            {
                case "00":
                    break;
                case "01":
                    hasSaves[1] = true;
                    break;
                case "07":
                    hasSaves[1] = true;
                    hasSaves[2] = true;
                    hasSaves[3] = true;
                    break;
                case "02":
                    hasSaves[2] = true;
                    break;
                case "03":
                    hasSaves[1] = true;
                    hasSaves[2] = true;
                    break;
                case "04":
                    hasSaves[3] = true;
                    break;
                case "05":
                    hasSaves[1] = true;
                    hasSaves[3] = true;
                    break;
                case "06":
                    hasSaves[2] = true;
                    hasSaves[3] = true;
                    break;
            }

            br.Close();

            //Here, I'll just set some variables and do  and return 07 or something.
            // for  save values

            if (gb == false)
            {
                switch (affected)
                {
                    case 1:
                        hasSaves[1] = false;
                        break;
                    case 2:
                        hasSaves[2] = false;
                        break;
                    case 3:
                        hasSaves[3] = false;
                        break;
                }
            }
            else if (gb)
            {
                switch (affected)
                {
                    case 1:
                        hasSaves[1] = true;
                        break;
                    case 2:
                        hasSaves[2] = true;
                        break;
                    case 3:
                        hasSaves[3] = true;
                        break;
                }
            }


            int a = 0x00;

            if (hasSaves[1])
            {
                a = 0x01;
            }
            if (hasSaves[2])
            {
                a = 0x02;
            }
            if (hasSaves[3])
            {
                a = 0x04;
            }
            if (hasSaves[1] && hasSaves[2])
            {
                a = 0x03;
            }
            if (hasSaves[1] && hasSaves[3])
            {
                a = 0x05;
            }
            if (hasSaves[2] && hasSaves[3])
            {
                a = 0x06;
            }
            if (hasSaves[1] && hasSaves[2] && hasSaves[3])
            {
                a = 0x07;
            }

            return a;
        	
        }
        
        public static void ExtractSave(int filenumber, string filename, string filepath)
        {
            int happyint = 0x0;
            int angryint = 0x1;
            switch (filenumber)
            {

                case 1:
                    happyint = 0x0;
                    angryint = 0x610;
                    break;
                case 2:
                    happyint = 0x620;
                    angryint = 0xC30;
                    break;
                case 3:
                    happyint = 0xC40;
                    angryint = 0x1250;
                    break;
                default:
                    MessageBox.Show("Not a valid save file number.\nValid choices are 1, 2, and 3.\nThis shouldn't be possible for this to happen.\nYou're bad at making this better.");
                    return;
            }

            BinaryReader br = new BinaryReader(File.OpenRead(filepath));
            BinaryWriter bw = new BinaryWriter(File.Create(Application.StartupPath + "/"+filename+".dat"));
            bw.BaseStream.Position = 0x0;
            br.BaseStream.Position = happyint;
            for (int i = happyint; i < angryint; i++)
            {
                bw.Write(br.ReadByte());
            }
            br.Close();
            bw.Close();
            BinaryWriter bw2 = new BinaryWriter(File.OpenWrite(filepath));
            bw2.BaseStream.Position = happyint;
            System.Diagnostics.Debug.WriteLine(angryint);
            System.Diagnostics.Debug.WriteLine(happyint);
            for (int i = happyint; i < angryint; i++)
            {
                bw2.BaseStream.Position = i;
                bw2.Write(0x00);
            }
            bw2.Close();

            int happy=0x00;
            happy = getSaves(filepath, filenumber, false);

            bw = new BinaryWriter(File.OpenWrite(filepath));
            bw.BaseStream.Position = 0x1F020;
            bw.Write(happy);
            bw.Close();

            MessageBox.Show("Done. Exported the save file to " + Application.StartupPath + "\\" + filename + ".dat"+".");
        }


    }
}
