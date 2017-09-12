using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace registration_form
{
    public partial class logoutForm : Form
    {
        public string usr;
        public logoutForm()
        {
            InitializeComponent();
        }

        private void logoutForm_Load(object sender, EventArgs e)
        {
            label1.Text = "Dear "+usr+"! You have been successfully logged out!!!";
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form_login fs = new Form_login();
            fs.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
