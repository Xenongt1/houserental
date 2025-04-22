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
    public partial class RecieptForm : Form
    {
        private string receiptInfo;

        public RecieptForm(string receipt)
        {
            InitializeComponent();
            receiptInfo = receipt;
       
        }

        private void RecieptForm_Load(object sender, EventArgs e)
        {
            
           
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Show a message box with Yes and No buttons
            DialogResult result = MessageBox.Show("Do you want to close this page?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Check the user's response
            if (result == DialogResult.Yes)
            {
                // If the user clicked Yes, close the application
                Rents Obj = new Rents();
                Obj.Show();
                this.Hide();
            }
            else
            {
                // If the user clicked No, do nothing (abort)
            }
        }

        private void RecieptForm_Load_1(object sender, EventArgs e)
        {
            RecieptLabel.Text = receiptInfo;
        }
    }
}
