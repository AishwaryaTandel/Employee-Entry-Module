using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace registration_form
{
    public partial class Form_login : Form
    {
        //public DataTable userinfo = null;
        public Form_login()
        {
            InitializeComponent();
            AcceptButton = loginButton;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usrnm = textBox1.Text;
            string pwd = textBox2.Text;

            SqlConnection conn = new SqlConnection("Server=10.20.50.101;Database=A_Test;uid=sa;password=fin@dev;");
            conn.Open();
            SqlCommand sc = new SqlCommand("select count(*) from login where Username='" + usrnm + "'and Password='" + pwd + "';", conn);
            if (Convert.ToInt32(sc.ExecuteScalar().ToString()) == 1)
            {

                //SqlCommand sc = new SqlCommand("insert into login values('" + textBox1.Text + "','" + textBox2.Text + "');",conn);
                //int o = sc.ExecuteNonQuery();
                //MessageBox.Show("Valid user!!!");
                Display newForm = new Display();
                newForm.usrname = usrnm;
                newForm.pass = pwd;
                newForm.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Invalid user!!!");
            conn.Close();
        }
        public static void main(string[] args)
        {
            Application.Run(new Form_login());
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1(this);
            newForm.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
