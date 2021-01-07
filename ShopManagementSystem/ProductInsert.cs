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
    public partial class ProductInsert : Form
    {

        SqlConnection con;

        public ProductInsert()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            if (Product_Name.Text == "" || VendorID.Text == "" || Amount.Text == "" || ProductID.Text == "")
            {
                MessageBox.Show("Please provide all the details", "Captions", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {

                Connect connectObj = new Connect();
                con = connectObj.connect();

                SqlCommand cmd = new SqlCommand("Insert into PRODUCT (PID,PNAME,AMOUNT,VID) values(@pid,@pname,@amount,@vid);", con);
                
                cmd.Parameters.AddWithValue("@pid", ProductID.Text);
                cmd.Parameters.AddWithValue("@pname", Product_Name.Text);

                cmd.Parameters.AddWithValue("@vid", VendorID.Text);
                cmd.Parameters.AddWithValue("@amount", Amount.Text);

                int i = cmd.ExecuteNonQuery();

                if (i != 0)
                {
                    MessageBox.Show("Product Insertion Successful!", "Captions", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Product Insertion Failed", "Captions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                con.Close();

                Product_Name.Clear();
                VendorID.Clear();
                Amount.Clear();
                ProductID.Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            Product_Name.Clear();
            VendorID.Clear();
            Amount.Clear();
            ProductID.Clear();
        }

        private void ProductInsert_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
