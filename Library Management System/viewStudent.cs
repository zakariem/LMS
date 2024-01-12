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

namespace Library_Management_System
{
    public partial class viewStudent : Form
    {
        public viewStudent()
        {
            InitializeComponent();
            panel2.Visible = false;
            displayData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                txtSearch.Clear();
            }
        }

        private void displayData()
        {
            using (SqlConnection conn = connectionClass.connect())
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from Student", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;


            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            panel2.Visible = true;
            panel2.Focus();
            txtSname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtEntroll.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtDepart.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtSemester.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtContact.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtSname.Text == "")
            {
                MessageBox.Show("Enter Student Name", "validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if (txtEntroll.Text == "")
            {
                MessageBox.Show("Enter Entroll Number", "validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if (txtDepart.Text == "")
            {
                MessageBox.Show("Enter Student Department", "validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if (txtSemester.Text == "")
            {
                MessageBox.Show("Enter Student Semester", "validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if (txtContact.Text == "")
            {
                MessageBox.Show("Enter Student contact", "validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if (txtEmail.Text == "")
            {
                MessageBox.Show("Enter Student Email", "validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
            {
                using (SqlConnection conn = connectionClass.connect())
                {
                    SqlCommand cmd = new SqlCommand("update Student set sName = @sName, entroll = @entroll, depart = @depart, sem = @sem, contact = @contact,email = @email where entroll = @entroll ", conn);

                    cmd.Parameters.AddWithValue("@sName", txtSname.Text);
                    cmd.Parameters.AddWithValue("@entroll", txtEntroll.Text);
                    cmd.Parameters.AddWithValue("@depart", txtDepart.Text);
                    cmd.Parameters.AddWithValue("@sem", txtSemester.Text);
                    cmd.Parameters.AddWithValue("@contact", Int64.Parse(txtContact.Text));
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);

                    cmd.ExecuteNonQuery();

                    txtSname.Clear();
                    txtContact.Clear();
                    txtDepart.Clear();
                    txtEntroll.Clear();
                    txtSemester.Clear();
                    txtEmail.Clear();

                    panel2.Visible = false;
                    displayData();

                    MessageBox.Show("Successfully", "Updating Data",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(txtSname.Text == "")
            {
                MessageBox.Show("Enter at least Student Name to Delete");
            }
            else if (MessageBox.Show("are you sure?", "Checking", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                using (SqlConnection conn = connectionClass.connect())
                {
                    SqlCommand cmd = new SqlCommand("delete from student where entroll = @entroll", conn);
                    cmd.Parameters.AddWithValue("@entroll", txtEntroll.Text);

                    cmd.ExecuteNonQuery();


                    displayData();

                    panel2.Visible = false;

                    
                    MessageBox.Show("Successfully", "Delete Data",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                return;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if(txtSearch.Text != "")
            {
                Image img = Image.FromFile("D:\\Library Management System\\img/search1.gif");
                pictureBox1.Image = img;
            }
            else
            {
                Image img = Image.FromFile("D:\\Library Management System\\img/search.gif");
                pictureBox1.Image = img;
            }
        }
    }
}
