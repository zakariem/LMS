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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Library_Management_System
{
    public partial class viewBook : Form
    {
        public viewBook()
        {
            InitializeComponent();
        }

        private void viewBook_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            displayData();
        }
        private void displayData()
        {
            using (SqlConnection conn = connectionClass.connect())
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from newBook", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Form1.click)
            {
                panel2.Visible = true;
                panel2.Focus();
                txtbname.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtbAname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtbpublication.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtbquantity.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = connectionClass.connect())
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from newBook where bName like '" + txtSearch.Text + "%'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtbname.Text == "")
            {
                MessageBox.Show("Enter Book Name", "validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if (txtbAname.Text == "")
            {
                MessageBox.Show("Enter Author Name", "validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if (txtbpublication.Text == "")
            {
                MessageBox.Show("Enter Book Publication", "validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            
            else if (txtbquantity.Text == "")
            {
                MessageBox.Show("Enter Book Quantity", "validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
            {
                using (SqlConnection conn = connectionClass.connect())
                {
                    SqlCommand cmd = new SqlCommand("update newBook set bName = @name, bAuthor = @author, bPubl = @publ, bPDate = @bpdate, bQuantity = @bquantity where bName = @name", conn);

                    cmd.Parameters.AddWithValue("@name", txtbname.Text);
                    cmd.Parameters.AddWithValue("@author", txtbAname.Text);
                    cmd.Parameters.AddWithValue("@publ", txtbpublication.Text);
                    cmd.Parameters.AddWithValue("@bpdate", dateTimePicker1.Value);

                    cmd.Parameters.AddWithValue("@bquantity", Int64.Parse(txtbquantity.Text));

                    cmd.ExecuteNonQuery();

                    displayData();

                    panel2.Visible = false;

                    dataGridView1.Focus();

                    MessageBox.Show("Successfully", "Update Data",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
           if(MessageBox.Show("are you sure?","Checking",MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                using (SqlConnection conn = connectionClass.connect())
                {
                    SqlCommand cmd = new SqlCommand("delete from newBook where bName = @name", conn);
                    cmd.Parameters.AddWithValue("@name", txtbname.Text);

                    cmd.ExecuteNonQuery();


                    displayData();

                    panel2.Visible = false;

                    dataGridView1.Focus();

                    MessageBox.Show("Successfully", "Delete Data",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            displayData();
            txtSearch.Clear();
        }
    }
}
