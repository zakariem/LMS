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
    public partial class Veiw : Form
    {
        public Veiw()
        {
            InitializeComponent();
            displayData();
            panel1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = connectionClass.connect())
            {
                if (MessageBox.Show("Are you sure you want to update this user?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Validate input fields are not empty or whitespace
                    if (
                        string.IsNullOrWhiteSpace(txtUserName.Text) ||
                        string.IsNullOrWhiteSpace(textBox1.Text) ||
                        string.IsNullOrWhiteSpace(txtPassword.Text
                        )
                        )
                    {
                        MessageBox.Show("Please fill in all the fields.");
                        return; // Exit the method if validation fails
                    }


                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    // Create a SqlCommand with parameters to update the user
                    using (SqlCommand cmd = new SqlCommand("UPDATE Users SET Username = @Username,Password = @Password, userType = @userType WHERE Username = @Username", conn))
                    {
                        // Add parameters with values from your form controls
                       
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                        cmd.Parameters.AddWithValue("@userType", textBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@Username", txtUserName.Text.Trim());

                        // Execute the update query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("User updated successfully.", "Updating Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No user found with the specified username, or no new data to update.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    // Clear the input fields
                    panel1.Visible = false;
                    textBox1.Clear();
                    txtUserName.Clear();
                    txtPassword.Clear();

                    // Refresh the DataGridView to show the updated data
                    displayData(); // This should be a method that refreshes the DataGridView with users.
                }
                else
                {
                    MessageBox.Show("Update cancelled.");
                }
            }

         }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = connectionClass.connect())
            {
                if (MessageBox.Show("Are you sure to delete this user?", "Confirmation", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {

                    SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE Username = @Username", conn);

                    cmd.Parameters.AddWithValue("@Username", txtUserName.Text.Trim());



                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User Deleted Successfully", "Deleting Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No user found with the specified username.", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // Clear the input fields
                    panel1.Visible = false;
                    textBox1.Clear();
                    txtUserName.Clear();
                    txtPassword.Clear();

                    // refresh your data grid or other UI elements to reflect the deletion
                    displayData();
                }
                else
                {
                    MessageBox.Show("Deletion Cancelled");
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {

                txtUserName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtPassword.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

                panel1.Visible = true;
                panel1.Focus();


            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Veiw_Load(object sender, EventArgs e)
        {

        }

        private void displayData()
        {
            using (SqlConnection conn = connectionClass.connect())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Users", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }
    }
}
