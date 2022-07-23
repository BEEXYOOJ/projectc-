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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            form6();
            getowner();
            showForm3();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-D3DNJRL;Initial Catalog=myproject;Integrated Security=True");
        private void form6()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select numID from Categorydbl", conn);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("numID", typeof(int));
            dt.Load(Rdr);
            comboBox_type1.ValueMember = "numID";
            comboBox_type1.DataSource = dt;
            conn.Close();
        }
        private void getowner()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select numID from landlorddbl", conn);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("numID", typeof(int));
            dt.Load(Rdr);
            comboBox1.ValueMember = "numID";
            comboBox1.DataSource = dt;
            conn.Close();
        }
        private void showForm3()
        {
            conn.Open();
            string Query = "select* from Apartments";
            SqlDataAdapter sda = new SqlDataAdapter(Query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            DataSet ds = new DataSet();
            sda.Fill(ds);
            guna2DataGridView1.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void ResetData()
        {
            textBox_name2.Text = "";
            textBox_dress.Text = "";
            comboBox_type1.SelectedIndex = -1;
            textBox_cost.Text = "";
            comboBox1.SelectedIndex = -1;
        }

        private void textBox_name2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_add2_Click(object sender, EventArgs e)
        {
            if (textBox_name2.Text == "" || textBox_dress.Text == "" || comboBox_type1.SelectedIndex == -1||textBox_cost.Text ==""||comboBox1.SelectedIndex==-1)
            {
                MessageBox.Show("mising infomation");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into Apartments (name,adress,type,cost,owner) values (@name,@adress,@type,@cost,@owner)", conn);
                    cmd.Parameters.AddWithValue("@name", textBox_name2.Text);
                    cmd.Parameters.AddWithValue("@adress", textBox_dress.Text);
                    cmd.Parameters.AddWithValue("@type", comboBox_type1.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@cost", textBox_cost.Text);
                    cmd.Parameters.AddWithValue("@owner", comboBox1.SelectedValue.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("apartments add");
                    conn.Close();
                    ResetData();
                    showForm3();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int key = 0;

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox_name2.Text = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox_dress.Text = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            comboBox_type1.Text = guna2DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox_cost.Text = guna2DataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            comboBox1.Text = guna2DataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            if (textBox_name2.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button_cancel2_Click(object sender, EventArgs e)
        {
            if (textBox_name2.Text == "" || textBox_dress.Text == "" || comboBox_type1.SelectedIndex == -1 || textBox_cost.Text == "" || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("mising infomation");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update Apartments set name=@name,adress=@adress,type=@type,cost=@cost,owner=@owner where numID=@numKey", conn);
                    cmd.Parameters.AddWithValue("@name", textBox_name2.Text);
                    cmd.Parameters.AddWithValue("@adress", textBox_dress.Text);
                    cmd.Parameters.AddWithValue("@type", comboBox_type1.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@cost", textBox_cost.Text);
                    cmd.Parameters.AddWithValue("@owner", comboBox1.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@numKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("apartments update");
                    conn.Close();
                    ResetData();
                    showForm3();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button_delete2_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("select an Apartments!!!");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from Apartments where numID=@numKey", conn);
                    cmd.Parameters.AddWithValue("numKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Apartments deleted!!!");
                    conn.Close();
                    ResetData();
                    showForm3();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}
