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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Exit_MouseHover(object sender, EventArgs e)
        {
            Exit.ForeColor = Color.White;
            Exit.BackColor = Color.Red;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Exit_MouseLeave(object sender, EventArgs e)
        {
            Exit.ForeColor = Color.Red;
            Exit.BackColor = Color.White;
        }

        private void txtUserName_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtUserName.Text == "username")
            {
                txtUserName.Clear();
            }
        }

        private void txtPassword_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtPassword.Text == "password")
            {
                txtPassword.Clear();
                txtPassword.PasswordChar = '*';
            }
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            using(SqlConnection conn = connectionClass.connect())
            {
                SqlCommand cmd = new SqlCommand("select * from loginTable where username = @Username AND password = @Password", conn);
                cmd.Parameters.AddWithValue("@Username",txtUserName.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count != 0)
                {
                    this.Hide();
                    Dashboard fm = new Dashboard();
                    fm.Show();
                }
                else
                {
                    MessageBox.Show("Wrong username and password", "Errory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
