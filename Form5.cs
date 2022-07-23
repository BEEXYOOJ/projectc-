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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            showForm2();
            showForm3();
            showForm5();
            getCost();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-D3DNJRL;Initial Catalog=myproject;Integrated Security=True");
        private void showForm2()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select numID from tenantsdbl", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("numID", typeof(int));
            dt.Load(Rdr);
            comboBox_ren.ValueMember = "numID";
            comboBox_ren.DataSource = dt;
            con.Close();
        }
        private void showForm3()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select numID from Apartments", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("numID", typeof(int));
            dt.Load(Rdr);
            comboBox_hongthoe.ValueMember = "numID";
            comboBox_hongthoe.DataSource = dt;
            con.Close();
        }
        private void resetData()
        {
            //comboBox_hongthoe.SelectedIndex = -1;
        }
        private void showForm5()
        {
            con.Open();
            string Query = "select* from rent";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            DataSet ds = new DataSet();
            sda.Fill(ds);
            guna2DataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void ResetData()
        {
            comboBox_ren.SelectedIndex = -1;
            comboBox_hongthoe.SelectedIndex = -1;
        }
        private void getCost()
        {
            con.Open();
            string query = "select * from Apartments where numID="+comboBox_hongthoe.SelectedValue.ToString()+"";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                textBox_jugnuag.Text = dr["cost"].ToString();
            }
            con.Close();
        }
        private void button_add3_Click(object sender, EventArgs e)
        {
            if (comboBox_hongthoe.SelectedIndex==-1 || textBox_jugnuag.Text == "" || comboBox_ren.SelectedIndex == -1)
            {
                MessageBox.Show("mising infomation");
            }
            else
            {
                try
                {
                    string Peroid = Rdate.Value.Date.Month + "-" + Rdate.Value.Date.Year;
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into rent (Apartments,amount,tennant,period) values (@Apartments,@amount,@tennant,@period)", con);
                    cmd.Parameters.AddWithValue("@Apartments", comboBox_hongthoe.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@amount", textBox_jugnuag.Text);
                    cmd.Parameters.AddWithValue("@tennant", comboBox_ren.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@period", Peroid);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("rent add");
                    con.Close();
                    ResetData();
                    showForm5();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

    }
}
