using System;
using System.Collections;
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
    public partial class issueBook : Form
    {
        public issueBook()
        {
            InitializeComponent();
            textBox1.Clear();
        }

        private void issueBook_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = connectionClass.connect())
            {
                string query = "SELECT bName from NewBook";
                SqlCommand command = new SqlCommand(query, conn);

                SqlDataReader sr = command.ExecuteReader();

                while (sr.Read())
                {
                    for(int i = 0; i < sr.FieldCount; i++)
                    {
                        comboBox1.Items.Add(sr.GetString(i));
                    }
                }
                sr.Close();
            }
        }
        int count;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = connectionClass.connect())
                {
                    string query = "SELECT * from Student where entroll = '" + textBox1.Text + "'";

                    SqlCommand command = new SqlCommand(query, conn);

                    SqlDataReader reader = command.ExecuteReader();


                    if (reader.Read())
                    {
                        txtSname.Text = reader[1].ToString();
                        txtD.Text = reader[3].ToString();
                        txtSsemester.Text = reader[4].ToString();
                        txtScontact.Text = reader[5].ToString();
                        txtEmail.Text = reader[6].ToString();
                    }
                    else
                    {
                        // If no data is found, clear the TextBoxes
                        txtSname.Clear();
                        txtD.Clear();
                        txtSsemester.Clear();
                        txtScontact.Clear();
                        txtEmail.Clear();

                        MessageBox.Show("No data found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    reader.Close();


                    //-------------------------------------------------

                    string q = "SELECT count(std_entroll) from IRBook where std_entroll = '" + textBox1.Text + "' and book_return_date is null";

                    SqlCommand cmd = new SqlCommand(q, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    count = int.Parse(ds.Tables[0].Rows[0][0].ToString());


                    //--------------------------------------------------
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        private void btnIssue_Click(object sender, EventArgs e)
        {
           if(txtSname.Text != "")
            {
                if (comboBox1.SelectedIndex != -1 && count <=2)
                {
                    using (SqlConnection conn = connectionClass.connect())
                    {
                        if (issuedCheck() && available())
                        {
                            SqlCommand cmd = new SqlCommand("insert into IRBook (std_entroll,std_name,std_depart,std_sem,std_contact,std_email,book_name,book_issue_date) VALUES (@std_entroll,@std_name,@std_depart,@std_sem,@std_contact,@std_email,@book_name,@book_issue_date)", conn);

                            cmd.Parameters.AddWithValue("@std_entroll", textBox1.Text);
                            cmd.Parameters.AddWithValue("@std_name", txtSname.Text);
                            cmd.Parameters.AddWithValue("@std_depart", txtD.Text);
                            cmd.Parameters.AddWithValue("@std_sem", txtSsemester.Text);
                            cmd.Parameters.AddWithValue("@std_contact", Int64.Parse(txtScontact.Text));
                            cmd.Parameters.AddWithValue("@std_email", txtEmail.Text);
                            cmd.Parameters.AddWithValue("@book_name", comboBox1.Text);
                            cmd.Parameters.AddWithValue("@book_issue_date", dateTimePicker1.Text);

                            cmd.ExecuteNonQuery();


                            textBox1.Clear();
                            txtD.Clear();
                            txtEmail.Clear();
                            txtScontact.Clear();
                            txtSname.Clear();
                            txtSsemester.Clear();
                            comboBox1.Text = "";


                            MessageBox.Show("Book Issued", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (issuedCheck() == false)
                            {
                                MessageBox.Show("Sorry you alread issued this book", "Warrning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                MessageBox.Show("Sorry we don't have this book choose another one", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            
                            }
                        }    
                    }
                }
                else
                {
                    if (comboBox1.SelectedIndex == -1)
                    {
                        MessageBox.Show("Select a book name", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Sorry you alread issued 3 books return 1 book to issuerd another one", "Warrning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    }
                }
            }
           
        }

        private void btnExit_Click(object sender, EventArgs e)
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

        Boolean issuedCheck()
        {
            bool issued = false;
            int count;

            using (SqlConnection conn = connectionClass.connect())
            {
                string q = "SELECT COUNT(std_entroll) FROM IRBook WHERE std_entroll = '" + textBox1.Text + "' AND book_return_date IS NULL AND book_name = '" + comboBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(q, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                count = int.Parse(ds.Tables[0].Rows[0][0].ToString());

                if (count != 1)
                    issued = true;
                else 
                    issued = false;
            }

            return issued;
        }

        Boolean available()
        {
            bool isavailable = false;
            int count;
            using (SqlConnection conn = connectionClass.connect())
            {
                string q = "SELECT bQuantity FROM NewBook WHERE bName = '" + comboBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(q, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                count = int.Parse(ds.Tables[0].Rows[0][0].ToString());

                if(count != 0)
                {
                    isavailable = true;
                    count--;

                    string q2 = "update NewBook set bQuantity = '" + count + "' WHERE bName = '" + comboBox1.Text + "'";
                    SqlCommand cmd2 = new SqlCommand(q2, conn);
                    cmd2.ExecuteNonQuery();
                }
                else
                {
                    isavailable = false;
                }
            }


            return isavailable;
        }

        private void btnR_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            txtD.Clear();
            txtEmail.Clear();
            txtScontact.Clear();
            txtSname.Clear();
            txtSsemester.Clear();
            comboBox1.Text = "";

        }
    }
}
