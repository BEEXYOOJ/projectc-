using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace House_of_rent
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            showForm6();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-D3DNJRL;Initial Catalog=myproject;Integrated Security=True");
        private void showForm6()
        {
            con.Open();
            string query = "select* from Categorydbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            guna2DataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void ResetData()
        {
            textBox_name.Text = "";
            textBox_remarts.Text = "";
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            if(textBox_name.Text ==""||textBox_remarts.Text=="")
            {
                MessageBox.Show("missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Categorydbl(Name,remarts) values(@Name,@remarts)",con);
                    cmd.Parameters.AddWithValue("@Name", textBox_name.Text);
                    cmd.Parameters.AddWithValue("@remarts", textBox_remarts.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("uhds");
                    con.Close();
                    ResetData();
                    showForm6();

                }
                catch(Exception Ex)
                {
                    MessageBox.Show("jkd");
                }
            }
        }
    }
}
