﻿using System;
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
    public partial class StockUpdate : Form
    {

        SqlConnection con;

        public StockUpdate()
        {
            InitializeComponent();
        }

        private void search_Click(object sender, EventArgs e)
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

        private void update_Click(object sender, EventArgs e)
        {
            if (ProductID.Text == "" || Quantity.Text == "" || Product_Name.Text == "")
            {
                MessageBox.Show("Please provide all the details", "Captions", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Connect connectObj = new Connect();
                con = connectObj.connect();

                SqlCommand cmd = new SqlCommand("UPDATE STOCK SET QUANTITY = @quantity WHERE PID = @pid", con);
                
                cmd.Parameters.AddWithValue("@pid", ProductID.Text);
                cmd.Parameters.AddWithValue("@quantity", Quantity.Text);
                int i = cmd.ExecuteNonQuery();

                if (i != 0)
                {
                    MessageBox.Show("Stock Updation Successful!", "Captions", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Stock Updation Failed", "Captions", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                con.Close();
                ProductID.Clear();
                Quantity.Clear();
                Product_Name.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Captions", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if(con != null)
                {
                    con.Close();
                }
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            ProductID.Clear();
            Product_Name.Clear();
            Quantity.Clear();
        }

        private void StockUpdate_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
