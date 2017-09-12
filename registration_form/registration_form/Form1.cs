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
    public partial class Form1 : Form
    {
        Form parentForm;
        public Form1(Form form)
        {
            InitializeComponent();
            this.parentForm = form;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("www.accordfintech.com");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Boolean noValue = true;
            Boolean yearCheck=true;
            Boolean deptCheck=true;
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
            if (year > (DateTime.Now.Year - 18))
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

                string Date = Convert.ToDateTime(dateTimePicker1.Text).ToShortDateString();
                SqlConnection conn = new SqlConnection("Server=10.20.50.101;Database=A_Test;uid=sa;password=fin@dev;");
                conn.Open();
                SqlCommand sc2 = new SqlCommand("insert into login values('" + textBox1.Text + "','" + textBox3.Text + "');", conn);
                int n = sc2.ExecuteNonQuery();
                SqlCommand sc = new SqlCommand("insert into register values('" + textBox1.Text + "','" + maskedTextBox1.Text + "','" + comboBox1.SelectedItem.ToString() + "','" + (radioButton1.Checked ? "Male" : "Female") + "','" + Date + "','" + _SelectedCource + "','" + textBox3.Text + "');", conn);
                int o = sc.ExecuteNonQuery();
                MessageBox.Show(o + ":Record has been inserted");
                conn.Close();
            }
            else
                MessageBox.Show("Record cannot be inserted");


        }
        /*public static void main(string[] args)
        {
            Application.Run(new Form1());
        }*/

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            maskedTextBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            if ((radioButton1.Checked ? "Male" : "Female") == "Male")
                radioButton1.Checked = false;
            else
                radioButton2.Checked = false;
            if (checkBox1.Checked)
                checkBox1.Checked = false;
            if (checkBox2.Checked)
                checkBox2.Checked = false;
            if (checkBox3.Checked)
                checkBox3.Checked = false;
            if (checkBox4.Checked)
                checkBox4.Checked = false;

        }

        private void label10_Click(object sender, EventArgs e)
        {
            Form_login newForm = new Form_login();
            newForm.Show();
            this.Hide();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            int a=0;
            SqlConnection conn = new SqlConnection("Server=10.20.50.101;Database=A_Test;uid=sa;password=fin@dev;");
            conn.Open();
            SqlCommand sc = new SqlCommand("select count(*) from register where name='" + textBox1.Text+ "';", conn);
            a=Convert.ToInt32(sc.ExecuteScalar());
            if(a>0)
            {
                MessageBox.Show("The username already exists! Please enter another username!");
                return;
            }
        }
    }
}
