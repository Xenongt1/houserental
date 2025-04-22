using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace houserental1
{
    public partial class Appartments : Form
    {
        public Appartments()
        {
            InitializeComponent();
            GetCategories();
            GetOwner();
            ShowApparts();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Mubarak\Desktop\mbrk c#\houserental1\houserental1\houserental1.mdf"";Integrated Security=True;Connect Timeout=30");

        private void ShowApparts()
        {
            try
            {
                Con.Open();
                string Query = "SELECT * FROM ApartTbl";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                AppartmentsDGV.DataSource = ds.Tables[0];
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

        public void GetCategories()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT CNum FROM CategoryTbl", Con);
                SqlDataReader Rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(Rdr);
                TypeCb.ValueMember = "CNum";
                TypeCb.DataSource = dt;
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

        public void GetOwner()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT LLid FROM LandLordTbl", Con);
                SqlDataReader Rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(Rdr);
                LLcb.ValueMember = "LLid";
                LLcb.DataSource = dt;
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

        private void ResetData()
        {
            ApNameTb.Text = "";
            LLcb.SelectedIndex = -1;
            CostTb.Text = "";
            AddressTb.Text = "";
            TypeCb.SelectedIndex = -1;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (ApNameTb.Text == "" || LLcb.SelectedIndex == -1 || CostTb.Text == "" || TypeCb.SelectedIndex == -1 || AddressTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "INSERT INTO ApartTbl(AName, AAddress, AType, Acost, Owner) VALUES (@AN, @AAdd, @AT, @AC, @AO)";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.Parameters.AddWithValue("@AN", ApNameTb.Text.Trim());
                    cmd.Parameters.AddWithValue("@AAdd", AddressTb.Text.Trim());
                    cmd.Parameters.AddWithValue("@AT", Convert.ToInt32(TypeCb.SelectedValue));
                    cmd.Parameters.AddWithValue("@AC", Convert.ToDecimal(CostTb.Text.Trim()));
                    cmd.Parameters.AddWithValue("@AO", Convert.ToInt32(LLcb.SelectedValue));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Appartment Added Successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Con.Close();
                    ShowApparts();
                    ResetData();
                }
            }
        }

        private void AppartmentsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < AppartmentsDGV.Rows.Count)
            {
                DataGridViewRow row = AppartmentsDGV.Rows[e.RowIndex];
                ApNameTb.Text = row.Cells[1].Value.ToString();
                AddressTb.Text = row.Cells[2].Value.ToString();
                TypeCb.SelectedValue = row.Cells[3].Value;
                CostTb.Text = row.Cells[4].Value.ToString();
                LLcb.SelectedValue = row.Cells[5].Value;

                Key = Convert.ToInt32(row.Cells[0].Value);
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (ApNameTb.Text == "" || LLcb.SelectedIndex == -1 || CostTb.Text == "" || TypeCb.SelectedIndex == -1 || AddressTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "UPDATE ApartTbl SET AName=@AN, AAddress=@AAdd, AType=@AT, Acost=@AC, Owner=@AO WHERE ANum=@AKey";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.Parameters.AddWithValue("@AN", ApNameTb.Text.Trim());
                    cmd.Parameters.AddWithValue("@AAdd", AddressTb.Text.Trim());
                    cmd.Parameters.AddWithValue("@AT", Convert.ToInt32(TypeCb.SelectedValue));
                    cmd.Parameters.AddWithValue("@AC", Convert.ToDecimal(CostTb.Text.Trim()));
                    cmd.Parameters.AddWithValue("@AO", Convert.ToInt32(LLcb.SelectedValue));
                    cmd.Parameters.AddWithValue("@AKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Appartment Updated Successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Con.Close();
                    ShowApparts();
                    ResetData();
                }
            }
        }

        int Key = 0;

        private void button2_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select the Appartment to Delete");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM ApartTbl WHERE ANum=@AKey", Con);
                    cmd.Parameters.AddWithValue("@AKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Appartment Deleted Successfully");
                    Con.Close();
                    ShowApparts();
                    ResetData();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void Appartments_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Tenants Obj = new Tenants();
            Obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Landlords Obj = new Landlords();
            Obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Rents Obj = new Rents();
            Obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Categories Obj = new Categories();
            Obj.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
