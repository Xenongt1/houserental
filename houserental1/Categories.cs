using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace houserental1
{
    public partial class Categories : Form
    {
        public Categories()
        {
            InitializeComponent();
            Showcategories();
        }

        public void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Mubarak\Desktop\mbrk c#\houserental1\houserental1\houserental1.mdf"";Integrated Security=True;Connect Timeout=30");

        private void Showcategories()
        {
            Con.Open();
            string Query = "SELECT * FROM   CategoryTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CategoriesDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        public void ResetData()
        {
            CategoryTb.Text = "";
            RemarksTb.Text = "";
        }

        public void AddBtn_Click(object sender, EventArgs e)
        {
        }


        private void label10_Click(object sender, EventArgs e)
        {
            // Your event handler code here
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            // Your event handler code here
        }

        private void CategoriesDGV_Paint(object sender, PaintEventArgs e)
        {

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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CategoriesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AddBtn_Click_1(object sender, EventArgs e)
        {

            if (CategoryTb.Text == "" || RemarksTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "INSERT INTO CategoryTbl(Category, Remarks) VALUES(@Cat, @Rem)";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.Parameters.AddWithValue("@Cat", CategoryTb.Text);
                    cmd.Parameters.AddWithValue("@Rem", RemarksTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Added Successfully");
                    Con.Close();
                    Showcategories();
                    ResetData();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Error: " + Ex.Message);
                }

            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Tenants Obj = new Tenants();
            Obj.Show();
            this.Hide();
        }
    }
}
