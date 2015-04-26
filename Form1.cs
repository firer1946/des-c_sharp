using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //button1.BackColor = System.Drawing.Color.Red;
            //MessageBox.Show("jfdkjfl");
            textBox3.Text = Program.jia_super(textBox2.Text, textBox1.Text);
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_MouseEnter(object sender, EventArgs e)
        {

        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = System.Drawing.SystemColors.Control                     ;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "123")
                textBox3.Text = textBox4.Text + "正确";
            else
                textBox3.Text = "false"; 
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label8.Text = DateTime.Now.ToLongTimeString();
            progressBar1.PerformStep();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox6.Text = Program.jie_super(textBox5.Text,textBox4.Text);
        }
    }
}
