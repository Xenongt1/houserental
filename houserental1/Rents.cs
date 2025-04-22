using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Bunifu.UI.WinForms.BunifuPictureBox;

namespace houserental1
{
    public partial class Rents : Form
    {
        public Rents()
        {
            InitializeComponent();
            GetAppart();
            GetTenant();
            ShowRents();
        }

        private void ShowRents()
        {
            Con.Open();
            string Query = "SELECT * FROM   RentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            RentsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void ResetData()
        {
            AmountTb.Text = "";
          
           
        }

        private void GetCost()
        {
            Con.Open();
            string Query = "SELECT * FROM ApartTbl WHERE ANum = " + ApartCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
           SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                AmountTb.Text = dr["ACost"].ToString();
            }
            Con.Close();
        }   

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Mubarak\Desktop\mbrk c#\houserental1\houserental1\houserental1.mdf"";Integrated Security=True;Connect Timeout=30");


        public void GetAppart()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ANum FROM ApartTbl", Con);
                SqlDataReader Rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(Rdr);
                ApartCb.ValueMember = "ANum";
                ApartCb.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        public void GetTenant()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT TenId, TenName FROM TenantTbl", Con);
                SqlDataReader Rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(Rdr);
                TenantCb.DisplayMember = "TenName";
                TenantCb.ValueMember = "TenId";
                TenantCb.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Rents_Load(object sender, EventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (ApartCb.SelectedIndex == -1 || TenantCb.SelectedIndex == -1 || AmountTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    string Period = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
                    Con.Open();
                    string Query = "INSERT INTO RentTbl(Apartment,Tenant,Period,Amount)values(@RA,@RT,@RP,@AC)";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.Parameters.AddWithValue("@RA", ApartCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@RT", TenantCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@RP", Period);
                    cmd.Parameters.AddWithValue("@AC", AmountTb.Text);
                    cmd.ExecuteNonQuery();

                    // Fetch the tenant name
                    string tenantNameQuery = "SELECT TenName FROM TenantTbl WHERE TenId = @TenId";
                    SqlCommand tenantNameCmd = new SqlCommand(tenantNameQuery, Con);
                    tenantNameCmd.Parameters.AddWithValue("@TenId", TenantCb.SelectedValue.ToString());
                    string tenantName = tenantNameCmd.ExecuteScalar().ToString();

                    // Generate the receipt information
                    string receiptInfo = $"Tenant ID: {TenantCb.SelectedValue}\n\n" +
                                         $"Tenant Name: {tenantName}\n\n" +   
                                         $"Apartment ID: {ApartCb.SelectedValue}\n\n" +
                                         $"Cost: {AmountTb.Text}\n\n" +
                                         $"Date of Payment: {Period}\n\n" +
                                         $"Note: Thank you for your payment.";

                    // Show the receipt in a new form
                    RecieptForm obj = new RecieptForm(receiptInfo);
                    obj.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Con.Close();
                    ShowRents();
                    ResetData();
                }
            }
        }

        private void ApartCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCost();
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
