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
    public partial class registration : Form
    {
        public registration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try{
                if (txtUserName.Text == "")
                {
                    MessageBox.Show("Enter userName");
                }
                else if (txtPassword.Text == "")
                {
                    MessageBox.Show("Enter Password");
                }
                else if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Select userType");
                }
                else
                {
                    using (SqlConnection conn = connectionClass.connect())
                    {
                        SqlCommand cmd = new SqlCommand("insert into users (username,password,userType) values(@username,@password,@userType)", conn);

                        cmd.Parameters.AddWithValue("@username", txtUserName.Text);
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@userType", comboBox1.Text);

                        cmd.ExecuteNonQuery();

                        txtPassword.Clear();
                        txtUserName.Clear();
                        comboBox1.SelectedText = "";

                        MessageBox.Show("Success");
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPassword_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
