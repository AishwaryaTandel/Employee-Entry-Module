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
    public partial class Display : Form
    {
        public string usrname,pass;
        public Display()
        {
            InitializeComponent();
            //this.parentForm = form;
        }

        private void Display_Load(object sender, EventArgs e)
        {
            label1.Text = "Hello " + usrname + "! Welcome to Accord Fintech!";
        }

        private void Exit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void updateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            update_regForm newForm = new update_regForm(this);
            newForm.MdiParent = this;
            newForm.usr = usrname;
            newForm.pass = pass;
            newForm.Show();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showForm sf = new showForm();
            sf.MdiParent = this;
            sf.usr = usrname;
            sf.Show();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please Click Logout form Menu at the top!");
        }

        private void logoutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            logoutForm lf = new logoutForm();
            //lf.MdiParent = this;
            lf.usr = usrname;
            lf.Show();
            this.Hide();
        }
    }
}
