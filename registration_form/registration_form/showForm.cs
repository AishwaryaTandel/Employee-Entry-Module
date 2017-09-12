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
    public partial class showForm : Form
    {
        public string usr;
        public showForm()
        {
            InitializeComponent();
        }

        private void showForm_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Server=10.20.50.101;Database=A_Test;uid=sa;password=fin@dev;");
            SqlDataAdapter da = new SqlDataAdapter("select name,emp_id,department,gender,dob,tech from register where Name='" + usr + "';", conn);
            DataSet ds = new DataSet();
            conn.Open();
            da.Fill(ds, "Register_table");
            conn.Close();
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Register_table";
        }
    }
}
