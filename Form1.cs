using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.D

namespace last
{
    public partial class Form1 : Form
    {
        List<List<int>> ranges = new List<List<int>>();
        List<String> messages = new List<String>();
        public Form1()
        {
            InitializeComponent();
        }


     
        private void button1_Click(object sender, EventArgs e)
        {
            if (msgBox.Text != "" && rangeBox1.Text != "" && rangeBox2.Text != "")
            {
                if (Convert.ToInt32(rangeBox1.Text) < Convert.ToInt32(rangeBox2.Text))
                {
                    addValues();
                    msgBox.Clear();
                    rangeBox1.Clear();
                    rangeBox2.Clear();
                }    
            }
            else errorLabel.Text = "Complete all fileds";  
        }

       

        private void addValues()
        {
            string msg;
            int range1, range2;
            range1 = Convert.ToInt32(rangeBox1.Text);
            range2 = Convert.ToInt32(rangeBox2.Text);
            msg = msgBox.Text;

            if (ranges.Count != 0)
            {
                for (int i = 0; i < ranges.Count; i++)
                {
                    if ((range1 < ranges[i][0] && range2 < ranges[i][0]) || (range1 > ranges[i][1] && range2 > ranges[i][1]))
                    {
                        List<int> v = new List<int>();
                        v.Add(range1);
                        v.Add(range2);

                        ranges.Add(v);
                        messages.Add(msg);

                        errorLabel.Text = "Message added";

                        break;
                    }
                    else
                    {
                        errorLabel.Text = "Invalid interval";
                    }
                }
            }
            else
            {
                List<int> v = new List<int>();
                v.Add(range1);
                v.Add(range2);

                ranges.Add(v);
                messages.Add(msg);

                errorLabel.Text = "Message added";
            }
           
        }

        private void rangeBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
                e.Handled = true;
        }

        private void rangeBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
                e.Handled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            for(int i=0;i<messages.Count;i++)
            {
                richTextBox1.Text += messages[i] + " " + ranges[i][0] + " " + ranges[i][1] + '\n';
            }
        }

        private void msgBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (msgBox.Text != "" && rangeBox1.Text != "" && rangeBox2.Text != "")
                {
                    if (Convert.ToInt32(rangeBox1.Text) < Convert.ToInt32(rangeBox2.Text))
                    {
                        addValues();
                        msgBox.Clear();
                        rangeBox1.Clear();
                        rangeBox2.Clear();
                    }
                }
                else errorLabel.Text = "Complete all fileds";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2(ranges, messages);
            frm.Show();
            this.Close();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DesktopLocation = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2);
        }

        private void msgBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
