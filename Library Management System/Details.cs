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
    public partial class Details : Form
    {
        public Details()
        {
            InitializeComponent();
        }

        private void Details_Load(object sender, EventArgs e)
        {
            displayData(dataGridView1, "is null");
            displayData(dataGridView2, "is not null");

        }

        private void displayData(DataGridView dg,String condition)
        {
            using (SqlConnection conn = connectionClass.connect())
            {
                SqlCommand cmd = new SqlCommand("select * from IRBook where book_return_date "+condition+"", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dg.DataSource = dt;

            }

        }
    }
}
