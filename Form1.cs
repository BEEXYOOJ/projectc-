using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace House_of_rent
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox_UserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_Login_Click(object sender, EventArgs e)
        {
            if(textBox_UserName.Text =="bee"||textBox_Passord.Text=="123")
            {
                MessageBox.Show("wellcome");

            }
            else
            {
                MessageBox.Show("plese try again");
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            //Form1 io = new Form1();
           // io.Show();
        }
    }
}
