using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace registration_form
{
    public partial class update_regForm : Form
    {
        public string usr, pass;
        public update_regForm(Form form)
        {
            InitializeComponent();
        }

        private void displayinfo(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Server=10.20.50.101;Database=A_Test;uid=sa;password=fin@dev;");
            conn.Open();
            SqlCommand sc = new SqlCommand("select * from register where name='" + usr +"';", conn);
            //sc.ExecuteNonQuery();
            using(SqlDataReader reader=sc.ExecuteReader())
             {
                //DataTable dt = new DataTable();
                //dt.Load(reader);

                if(reader.Read())
                {
                   
                    textBox1.Text = reader.GetValue(0).ToString();
                    maskedTextBox1.Text = reader.GetValue(1).ToString();
                    comboBox1.SelectedItem=(reader.GetValue(2).ToString());
                    if (reader.GetValue(3).ToString() != "Female")
                    {
                        radioButton1.Checked = true;
                    }
                    else
                        radioButton2.Checked = true;
                    dateTimePicker1.Value=Convert.ToDateTime(reader.GetValue(4).ToString());
                    if (reader.GetValue(5).ToString().Contains("C#"))
                        checkBox1.Checked = true;
                    if (reader.GetValue(5).ToString().Contains("ASP.NET"))
                        checkBox2.Checked = true;
                    if (reader.GetValue(5).ToString().Contains("SQL"))
                        checkBox3.Checked = true;
                    if (reader.GetValue(5).ToString().Contains("MongoDB"))
                        checkBox4.Checked = true;
                    textBox3.Text = reader.GetValue(6).ToString();
                }
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Boolean noValue = true;
            Boolean yearCheck = true;
            Boolean deptCheck = true;
            Boolean checkGender = false;
            if (maskedTextBox1.Text == "" || string.IsNullOrEmpty(textBox1.Text))
            {
                noValue = false;
                MessageBox.Show("Please enter username or employee ID");
                return;
            }
            if (comboBox1.SelectedIndex < 0)
            {
                deptCheck = false;
                MessageBox.Show("Please select department!");
                return;
            }
            if (radioButton1.Checked == true || radioButton2.Checked == true)
            {
                checkGender = true;
            }
            else
            {
                MessageBox.Show("Please select Gender!");
                return;
            }
            int year = Convert.ToInt32(dateTimePicker1.Value.Year);
            if (year > (DateTime.Now.Year-18))
            {
                yearCheck = false;
                MessageBox.Show("You have not completed 18.You are not allowed to register!");
                return;
            }

            string pass = textBox3.Text;
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            Boolean isValidated = hasNumber.IsMatch(pass) && hasUpperChar.IsMatch(pass) && hasMinimum8Chars.IsMatch(pass);
            if (!isValidated)
            {
                MessageBox.Show("Please enter valid password!\n Password should contain atleast 1 uppercase character, 1 digit and atleast 8 characters!");
                return;
            }
            Boolean matchPass = pass.Equals(textBox4.Text);
            if (!matchPass)
            {
                MessageBox.Show("Password and Confirm password are not matching!");
                return;
            }



            if (isValidated && matchPass && noValue && yearCheck && deptCheck && checkGender)
            {
                string _SelectedCource = "";

                if (checkBox1.Checked)
                    _SelectedCource += "C#,";

                if (checkBox2.Checked)
                    _SelectedCource += "ASP.NET,";

                if (checkBox3.Checked)
                    _SelectedCource += "SQL,";

                if (checkBox4.Checked)
                    _SelectedCource += "MongoDB,";

                if (_SelectedCource.Length > 0)
                    _SelectedCource = _SelectedCource.Substring(0, _SelectedCource.Length - 1);
                SqlConnection conn = new SqlConnection("Server=10.20.50.101;Database=A_Test;uid=sa;password=fin@dev;");
                conn.Open();
                SqlCommand sc = new SqlCommand("update register set emp_id='" + maskedTextBox1.Text + "', department='" + comboBox1.SelectedItem.ToString() + "', gender='" + (radioButton1.Checked ? "Male" : "Female") + "', dob='" + Convert.ToDateTime(dateTimePicker1.Text).ToShortDateString() + "', tech='" + _SelectedCource + "', password='" + textBox3.Text + "' where name='" + usr + "';", conn);
                SqlCommand sc1 = new SqlCommand("update login set Password='" + textBox3.Text + "' where Username='" + usr + "';", conn);
                sc1.ExecuteNonQuery();
                
                if (sc.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Record updated successfully in database!");
                }
            }
            else
                MessageBox.Show("Record could not be updated!");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("www.accordfintech.com");
        }
    }
}
