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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            showForm4();
            ResetData();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-D3DNJRL;Initial Catalog=myproject;Integrated Security=True");

        private void showForm4()
        {
            con.Open();
            string Query = "select* from landlorddbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            DataSet ds = new DataSet();
            sda.Fill(ds);
            guna2DataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void ResetData()
        {
            textBox_name1.Text = "";
            textBox_phone1.Text = "";
            comboBox_gender1.SelectedIndex = -1;
        }

        private void button_delete1_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("select a landlord");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from landlorddbl where numID=@numKey", con);
                    cmd.Parameters.AddWithValue("numKey",key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("landlord delete");
                    con.Close();
                    ResetData();
                    showForm4();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void button_add1_Click(object sender, EventArgs e)
        {
            if (textBox_name1.Text == "" || textBox_phone1.Text == "" || comboBox_gender1.SelectedIndex == -1)
            {
                MessageBox.Show("mising infomation");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into landlorddbl (LName,Lphone,LGen) values (@LName,@LPhone,@LGen)", con);
                    cmd.Parameters.AddWithValue("@LName", textBox_name1.Text);
                    cmd.Parameters.AddWithValue("@LPhone", textBox_phone1.Text);
                    cmd.Parameters.AddWithValue("@LGen", comboBox_gender1.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("landlord add");
                    con.Close();
                    ResetData();
                    showForm4();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int key = 0;

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox_name1.Text = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox_phone1.Text = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            comboBox_gender1.Text = guna2DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            if (textBox_name1.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button_cancel1_Click(object sender, EventArgs e)
        {
            if (textBox_name1.Text == "" || textBox_phone1.Text == "" || comboBox_gender1.SelectedIndex == -1)
            {
                MessageBox.Show("mising infomation");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update landlorddbl set LName=@LName, LPhone=@LPhone, LGen=@LGen where numID=@numKey ", con);
                    cmd.Parameters.AddWithValue("@LName", textBox_name1.Text);
                    cmd.Parameters.AddWithValue("@LPhone", textBox_phone1.Text);
                    cmd.Parameters.AddWithValue("@LGen", comboBox_gender1.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@numkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("landlord  update");
                    con.Close();
                    ResetData();
                    showForm4();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}
