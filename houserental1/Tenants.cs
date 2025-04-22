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

namespace houserental1
{
    public partial class Tenants : Form
    {
        public Tenants()
        {
            InitializeComponent();
            PopulateGenderComboBox();
            ShowTenants();
        }

        private void PopulateGenderComboBox()
        {
            GenCb.Items.Add("Male");
            GenCb.Items.Add("Female");
            GenCb.Items.Add("Other"); // Add as needed
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Mubarak\Desktop\mbrk c#\houserental1\houserental1\houserental1.mdf"";Integrated Security=True;Connect Timeout=30");

        private void ShowTenants()
        {
            Con.Open();
            string Query = "SELECT * FROM TenantTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TenantsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void ResetData()
        {
            PhoneTb.Text = "";
            GenCb.SelectedIndex = -1;
            TNameTb.Text = "";
            Key = 0;
        }

        int Key = 0;

        private void TenantsDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < TenantsDGV.Rows.Count)
            {
                DataGridViewRow row = TenantsDGV.Rows[e.RowIndex];
                TNameTb.Text = row.Cells[1].Value.ToString();
                PhoneTb.Text = row.Cells[2].Value.ToString();
                GenCb.Text = row.Cells[3].Value.ToString();

                if (string.IsNullOrEmpty(TNameTb.Text))
                {
                    Key = 0;
                }
                else
                {
                    Key = Convert.ToInt32(row.Cells[0].Value);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TNameTb.Text) || GenCb.SelectedIndex == -1 || string.IsNullOrWhiteSpace(PhoneTb.Text))
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "UPDATE TenantTbl SET TenName=@TN, TenPhone=@TP, TenGen=@TG WHERE TenId=@TKey";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.Parameters.AddWithValue("@TN", TNameTb.Text.Trim());
                    cmd.Parameters.AddWithValue("@TP", PhoneTb.Text.Trim());
                    cmd.Parameters.AddWithValue("@TG", GenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@TKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tenant Updated Successfully");
                    Con.Close();
                    ShowTenants();
                    ResetData();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select the Tenant to Delete");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM TenantTbl WHERE TenId=@TKey", Con);
                    cmd.Parameters.AddWithValue("@TKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tenant Deleted Successfully");
                    Con.Close();
                    ShowTenants();
                    ResetData();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            // Trim the values and check if any are empty or GenCb is not selected
            string tenantName = TNameTb.Text.Trim();
            string phone = PhoneTb.Text.Trim();
            int genIndex = GenCb.SelectedIndex;

            if (string.IsNullOrEmpty(tenantName) || genIndex == -1 || string.IsNullOrEmpty(phone))
            {
                // Debugging messages
                string debugMessage = "Debug Info: \n" +
                                      $"Tenant Name: '{tenantName}' (Length: {tenantName.Length})\n" +
                                      $"Phone: '{phone}' (Length: {phone.Length})\n" +
                                      $"Gender Index: {genIndex}";
                MessageBox.Show("Missing Information\n" + debugMessage);
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "INSERT INTO TenantTbl(TenName, TenPhone, TenGen) VALUES (@TN, @TP, @TG)";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.Parameters.AddWithValue("@TN", tenantName);
                    cmd.Parameters.AddWithValue("@TP", phone);
                    cmd.Parameters.AddWithValue("@TG", GenCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tenant Added Successfully");
                    Con.Close();
                    ShowTenants();
                    ResetData();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }


        // Event handlers for labels and panels can be removed if not used.
        private void label2_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) {
            Rents Obj = new Rents();
            Obj.Show();
            this.Hide();
        }
        private void label9_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }

        private void label1_Click(object sender, EventArgs e)
        {
            Appartments Obj = new Appartments();
            Obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Landlords Obj = new Landlords();
            Obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Categories Obj = new Categories();
            Obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {

            // Show a message box with Yes and No buttons
            DialogResult result = MessageBox.Show("Do you want to Logout?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
