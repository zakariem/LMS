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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Library_Management_System
{
    public partial class returnBook : Form
    {
        public returnBook()
        {
            InitializeComponent();
            panel3.Visible = false;
            label5.Visible = false;
        }

        Int64 rowID;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.RowCount != 0)
            {
                panel3.Visible = true;
                panel3.Focus();

                rowID = Int64.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                textBox2.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();

            }
            else
                panel3.Visible = false;

        }


        int count;
        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                {
                    using (SqlConnection conn = connectionClass.connect())
                    {
                        //return
                        SqlCommand cmd = new SqlCommand("update IRBook set book_return_date = '" + dateTimePicker1.Value + "' where std_entroll = '" + textBox1.Text + "' and sID = '" + rowID + "'", conn);
                        cmd.ExecuteNonQuery();
                        
                        

                        //update book quantity
                        SqlCommand selectCommand = new SqlCommand("SELECT bQuantity FROM NewBook WHERE bName = '" + textBox2.Text + "'", conn);
                        SqlDataAdapter da = new SqlDataAdapter(selectCommand);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        count = int.Parse(ds.Tables[0].Rows[0][0].ToString());

                        // Increment the value
                        count++;

                        // Update the database with the new value
                        SqlCommand updateCommand = new SqlCommand("update NewBook set bQuantity =  '" + count + "' where bName =  '" + textBox2.Text + "'", conn);
                        updateCommand.ExecuteNonQuery();


                        
                        textBox2.Clear();
                        textBox3.Clear();

                        panel3.Visible = false;
                        displayData();


                        MessageBox.Show("return it successfuly");
                    }
                }
                else
                {
                    MessageBox.Show("Enter entroll no");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void displayData()
        {
            if(textBox1.Text != "")
            {
                using (SqlConnection conn = connectionClass.connect())
                {
                    SqlCommand cmd = new SqlCommand("select * from IRBook where std_entroll = '" + textBox1.Text + "' and book_return_date is null", conn);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                   

                }
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
                displayData();
            else
                MessageBox.Show("enter student entroll no");
        }

        private void btnR_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
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

        
    }
}
