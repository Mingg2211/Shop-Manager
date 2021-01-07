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

namespace ShopManagementSystem
{
    public partial class StockDelete : Form
    {

        SqlConnection con;
        public StockDelete()
        {
            InitializeComponent();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                Connect connectObj = new Connect();

                con = connectObj.connect();

                SqlCommand cmd = new SqlCommand("DELETE FROM STOCK WHERE PID = @pid", con);
                cmd.Parameters.AddWithValue("@pid", ProductID.Text);
                int i = cmd.ExecuteNonQuery();
                

                if (i != 0)
                {
                    MessageBox.Show("Stock Deletion Successful!", "Captions", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Stock Deletion Failed", "Captions", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                con.Close();
                ProductID.Clear();
                Product_Name.Clear();
            }
            catch (Exception )
            {
                MessageBox.Show("Product Not found", "Captions", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if(con != null)
                {
                    con.Close();
                }
            }
        }

        private void Search_Click(object sender, EventArgs e)
        {
            try
            {
                Connect connectObj = new Connect();
                using (con = connectObj.connect())
                {

                    using (SqlCommand cmd = new SqlCommand("SELECT PNAME FROM PRODUCT WHERE PID = @pid"))
                    {
                        cmd.Parameters.AddWithValue("@pid", ProductID.Text);
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            sdr.Read();
                            Product_Name.Text = sdr["PNAME"].ToString();
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Product not found", "Captions", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if(con != null)
                {
                    con.Close();
                }
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            ProductID.Clear();
            Product_Name.Clear();
        }

        private void StockDelete_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
