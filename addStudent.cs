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
    public partial class addStudent : Form
    {
        public addStudent()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("this will delete your unsave data", "Warrning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
            else
            {
                return;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (txtSname.Text != "")
            {
                txtSname.Clear();
                txtScontact.Clear();
                txtD.Clear();
                txtEno.Clear();
                txtSsemester.Clear();
                txtEmail.Clear();
                txtSname.Focus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSname.Text == "")
            {
                MessageBox.Show("Enter Student Name", "validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if (txtEno.Text == "")
            {
                MessageBox.Show("Enter Entroll Number", "validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if (txtD.Text == "")
            {
                MessageBox.Show("Enter Student Department", "validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if (txtSsemester.Text == "")
            {
                MessageBox.Show("Enter Student Semester", "validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if (txtScontact.Text == "")
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
                    SqlCommand cmd = new SqlCommand("insert into Student(sName,entroll,depart,sem,contact,email) values (@sName,@entroll,@depart,@sem,@contact,@email)", conn);

                    cmd.Parameters.AddWithValue("@sName", txtSname.Text);
                    cmd.Parameters.AddWithValue("@entroll", txtEno.Text);
                    cmd.Parameters.AddWithValue("@depart", txtD.Text);
                    cmd.Parameters.AddWithValue("@sem", txtSsemester.Text);
                    cmd.Parameters.AddWithValue("@contact", Int64.Parse(txtScontact.Text));
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);

                    cmd.ExecuteNonQuery();

                    txtSname.Clear();
                    txtScontact.Clear();
                    txtD.Clear();
                    txtEno.Clear();
                    txtSsemester.Clear();
                    txtEmail.Clear();
                    txtSname.Focus();

                    MessageBox.Show("Successfully", "Saving Data",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}