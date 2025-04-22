using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace houserental1
{   
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Reset()
        {
            UNameTb.Text = "";
            PasswordTb.Text = "";
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(UNameTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Enter The Username and Password");
                Reset();
            }
            else if (UNameTb.Text == "Admin" && PasswordTb.Text == "Admin")
            {
                Tenants Obj = new Tenants();
                Obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Username or Password");
                Reset();
            }   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Show a message box with Yes and No buttons
            DialogResult result = MessageBox.Show("Do you want to close the application?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Check the user's response
            if (result == DialogResult.Yes)
            {
                // If the user clicked Yes, close the application
                Application.Exit();
            }
            else
            {
                // If the user clicked No, do nothing (abort)
            }
        }
    }
}
