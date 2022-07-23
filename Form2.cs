using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace House_of_rent
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            showForm2();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-D3DNJRL;Initial Catalog=myproject;Integrated Security=True");
        private void showForm2()

        {
            con.Open();
            string Query = "select*from tenantsdbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            DataSet ds = new DataSet();
            sda.Fill(ds);
            guna2DataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void ResetData()
        {
            textBox_name.Text = "";
            textbox_phone.Text = "";
            comboBox_gender.SelectedIndex = -1;
        }
       
        private void button_add_Click(object sender, EventArgs e)
        {
          if(textBox_name.Text ==""||textbox_phone.Text ==""||comboBox_gender.SelectedIndex ==-1)
            {
                MessageBox.Show("mising infomation");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into tenantsdbl (name,phone,gender) values (@name,@phone,@gender)", con);
                    cmd.Parameters.AddWithValue("@name", textBox_name.Text);
                    cmd.Parameters.AddWithValue("@phone", textbox_phone.Text);
                    cmd.Parameters.AddWithValue("@gender", comboBox_gender.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("tenants add");
                    con.Close();
                    ResetData();
                    showForm2();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }
       int Key = 0;

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox_name.Text = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textbox_phone.Text = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            comboBox_gender.Text = guna2DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            if(textBox_name.Text =="")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        private void button_delete_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("select a tenantsdbl");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from tenantsdbl where numID=@numKey", con);
                    cmd.Parameters.AddWithValue("numKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("tenants delete");
                    con.Close();
                    ResetData();
                    showForm2();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }
        int key = 0;

        private void label_member_Click(object sender, EventArgs e)
        {
            //Form2 obj = new Form2();
            //obj.Show();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            if (textBox_name.Text == "" || textbox_phone.Text == "" || comboBox_gender.SelectedIndex == -1)
            {
                MessageBox.Show("mising infomation");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update from tenantsdbl name=@ame,phone=@phone,gender=@gender where numID=@numkey", con);
                    cmd.Parameters.AddWithValue("@name", textBox_name.Text);
                    cmd.Parameters.AddWithValue("@phone", textbox_phone.Text);
                    cmd.Parameters.AddWithValue("@gender", comboBox_gender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@numkey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("tenants add");
                    con.Close();
                    ResetData();
                    showForm2();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}
