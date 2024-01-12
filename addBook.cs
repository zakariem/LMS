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
    public partial class addBook : Form
    {
        public addBook()
        {
            InitializeComponent();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("this will delete your unsave data","Warrning",MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
            else
            {
                return;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Enter Book Name", "validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Enter Author Name", "validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Enter Book Publication", "validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if (textBox4.Text == "")
            {
                MessageBox.Show("Enter Book Price", "validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if (textBox5.Text == "")
            {
                MessageBox.Show("Enter Book Quantity", "validation", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
            {
                using (SqlConnection conn = connectionClass.connect())
                {
                    SqlCommand cmd = new SqlCommand("insert into NewBook(bName,bAuthor,bPubl,bPDate,bPrice,bQuantity) values (@name,@author,@publ,@pdata,@bprice,@bquantity)", conn);

                    cmd.Parameters.AddWithValue("@name",textBox1.Text);
                    cmd.Parameters.AddWithValue("@author", textBox2.Text);
                    cmd.Parameters.AddWithValue("@publ", textBox3.Text);
                    cmd.Parameters.AddWithValue("@pdata", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@bprice", Int64.Parse(textBox4.Text));
                    cmd.Parameters.AddWithValue("@bquantity", Int64.Parse(textBox5.Text));

                    cmd.ExecuteNonQuery();

                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    textBox1.Focus();

                    MessageBox.Show("Successfully", "Saving Data",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
