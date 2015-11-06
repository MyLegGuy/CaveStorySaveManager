using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CaveStorySaveManager;

namespace CaveStorySaveManager
{
    public partial class Main : Form
    {
        
    	OpenFileDialog ofd = new OpenFileDialog();
    	OpenFileDialog ofd2 = new OpenFileDialog();
    	
    	public Main()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch!= 8)
            {
                e.Handled = true;
                MessageBox.Show("Son, u can't put wordz in here!\nYou ned put number! Ned be 1 or 2 or 3!\n(You could enter other numbers too, but they wouldn't work...)");
            }
        }
		
        private void button3_Click(object sender, EventArgs e)
        {
            ofd2.ShowDialog();
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("You want to inject a save file.....into\nEh, I ran out of stupid things to say.\nYou need to put the save file number you want to inject this sae into.");
                return;
            }
            if (Int32.Parse(textBox1.Text) > 3 || int.Parse(textBox1.Text) < 1)
            {
                MessageBox.Show("You're an idiot. Sorry to break it to you.");
                return;
            }
            if (String.IsNullOrEmpty(ofd.FileName))
            {
                MessageBox.Show("You want to inject a save into nothing? Wierd.");
                return;
            }
            if (String.IsNullOrEmpty(ofd2.FileName))
            {
                MessageBox.Show("You want to inject nothing? Wierd.");
                return;
            }

            Manage.injectSave(ofd.FileName, ofd2.FileName, Int32.Parse(textBox1.Text));
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
			ofd.ShowDialog();
            if (String.IsNullOrEmpty(ofd.FileName)){
            	MessageBox.Show("So...it was a lie? You never wanted to manage your saves?\nAll you  wanted to do was open the diolauge.");
                return;
            }        	
        }
        
        
        private void button1_Click(object sender, EventArgs e)
        {
        	if (textBox2.Text == "")
            {
                MessageBox.Show("You must enter what you want you want the file name to be for the\nexported save.");
                return;
            }
            if (textBox1.Text == "")
            {
            	MessageBox.Show("You must enter the save file number you want.");
                return;
            }
            if (Int32.Parse(textBox1.Text) > 3 || int.Parse(textBox1.Text) < 1)
            {
                MessageBox.Show("You know...it's not that hard.\nAll you had to do was enter that's 1 through 3.\nBut somehow you managed to mess it up.");
                return;
            }
			if (String.IsNullOrEmpty(ofd.FileName)){
            	MessageBox.Show("You must open your Profile.dat main save file using\nThe open Profile.dat button.");
                return;
            }
            
			Manage.ExtractSave(Int32.Parse(textBox1.Text), textBox2.Text, ofd.FileName);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
