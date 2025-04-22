using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace houserental1
{
    public partial class Landlords : Form
    {
        public Landlords()
        {
                InitializeComponent();
                PopulateGenderComboBox();
                ShowLandlords();
            }

            private void PopulateGenderComboBox()
            {
                GenCb.Items.Add("Male");
                GenCb.Items.Add("Female");
                GenCb.Items.Add("Other"); // Add as needed
            }

            SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Mubarak\Desktop\mbrk c#\houserental1\houserental1\houserental1.mdf"";Integrated Security=True;Connect Timeout=30");

            private void ShowLandlords()
            {
                Con.Open();
                string Query = "SELECT * FROM   LandLordTbl";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                LandlordsDGV.DataSource = ds.Tables[0];
                Con.Close();
            }

            private void ResetData()
            {
                PhoneTb.Text = "";
                GenCb.SelectedIndex = -1;
                LLnameTb.Text = "";
                Key = 0;
            }

            int Key = 0;

          

          



    private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void EditBtn_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LLnameTb.Text) || GenCb.SelectedIndex == -1 || string.IsNullOrWhiteSpace(PhoneTb.Text))
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "UPDATE LandLordTbl SET LLName=@LLN, LLPhone=@LLP, LLGen=@LLG WHERE LLId=@LLKey";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.Parameters.AddWithValue("@LLN", LLnameTb.Text.Trim());
                    cmd.Parameters.AddWithValue("@LLP", PhoneTb.Text.Trim());
                    cmd.Parameters.AddWithValue("@LLG", GenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@LLKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Landlord Updated Successfully");
                    Con.Close();
                    ShowLandlords();
                    ResetData();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DeleteBtn_Click_1(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select the Landlord to Delete");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM LandLordTbl WHERE LLId=@LLKey", Con);
                    cmd.Parameters.AddWithValue("@LLKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Lanlord Deleted Successfully");
                    Con.Close();
                    ShowLandlords();
                    ResetData();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void LandlordsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < LandlordsDGV.Rows.Count)
            {
                DataGridViewRow row = LandlordsDGV.Rows[e.RowIndex];
                LLnameTb.Text = row.Cells[1].Value.ToString();
                PhoneTb.Text = row.Cells[2].Value.ToString();
                GenCb.Text = row.Cells[3].Value.ToString();

                if (string.IsNullOrEmpty(LLnameTb.Text))
                {
                    Key = 0;
                }
                else
                {
                    Key = Convert.ToInt32(row.Cells[0].Value);
                }
            }
        }

        private void SaveBtn_Click_1(object sender, EventArgs e)
        {
            // Trim the values and check if any are empty or GenCb is not selected
            string landlordName = LLnameTb.Text.Trim();
            string phone = PhoneTb.Text.Trim();
            int genIndex = GenCb.SelectedIndex;

            if (string.IsNullOrEmpty(landlordName) || genIndex == -1 || string.IsNullOrEmpty(phone))
            {
                // Debugging messages
                string debugMessage = "Debug Info: \n" +
                                      $"Landlord Name: '{landlordName}' (Length: {landlordName.Length})\n" +
                                      $"Phone: '{phone}' (Length: {phone.Length})\n" +
                                      $"Gender Index: {genIndex}";
                MessageBox.Show("Missing Information\n" + debugMessage);
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "INSERT INTO LandLordTbl(LLName, LLPhone, LLGen) VALUES (@LLN, @LLP, @LLG)";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.Parameters.AddWithValue("@LLN", landlordName);
                    cmd.Parameters.AddWithValue("@LLP", phone);
                    cmd.Parameters.AddWithValue("@LLG", GenCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Landlord Added Successfully");
                    Con.Close();
                    ShowLandlords();
                    ResetData();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Categories Obj = new Categories();
            Obj.Show();
        }

        private void Landlords_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Tenants Obj = new Tenants();
            Obj.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Appartments Obj = new Appartments();
            Obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Rents Obj = new Rents();
            Obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {

            // Show a message box with Yes and No buttons
            DialogResult result = MessageBox.Show("Do you want to Logout? ", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Check the user's response
            if (result == DialogResult.Yes)
            {
                Login Obj = new Login();
                Obj.Show();
                this.Hide();
            }
            else
            {
                // If the user clicked No, do nothing (abort)
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {// Show a message box with Yes and No buttons
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
