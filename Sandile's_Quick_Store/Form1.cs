using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;





namespace Sandile_s_Quick_Store
{
    public partial class Form1 : Form
    {
        BusinessLogic logicInstance = new BusinessLogic();

        public Form1()
        {
            InitializeComponent();
            dataGridView1.CellClick += dataGridView1_CellClick;           

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                string productName = Convert.ToString(selectedRow.Cells["ProductName"].Value);
                int quantity = Convert.ToInt32(selectedRow.Cells["Quantity"].Value);
                double price = Convert.ToDouble(selectedRow.Cells["Price"].Value);

                
                productNameTxbx.Text = productName;
                quantityTxbx.Text = quantity.ToString();
                priceTxbx.Text = price.ToString();
            }
        }



        private void insertBtn_Click(object sender, EventArgs e)
        {
            string productName = productNameTxbx.Text;
            int quantity = int.Parse(quantityTxbx.Text);
            double price = double.Parse(priceTxbx.Text);
            double stockValue = quantity * price; 

            logicInstance.SaveProduct(productName, quantity, price, stockValue);
            MessageBox.Show("Record Added to Database");
            DisplayProducts();

        }
        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                try
                {
                    
                    string productName = selectedRow.Cells["ProductName"].Value.ToString();
                    int quantity = Convert.ToInt32(quantityTxbx.Text); 
                    double price = Convert.ToDouble(priceTxbx.Text); 
                    double stockValue = quantity * price; 

                   
                    logicInstance.UpdateProduct(productName, quantity, price, stockValue);                   
                    DisplayProducts();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid quantity or price format. Please enter valid numeric values.", "Update Product", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating product: {ex.Message}", "Update Product", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a product to update.", "Update Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                
                string productNameToDelete = dataGridView1.SelectedRows[0].Cells["ProductName"].Value.ToString();                
                logicInstance.DeleteProduct(productNameToDelete);                
                DisplayProducts();
            }
            else
            {
                MessageBox.Show("Please select a product to delete.", "Delete Product");
            }
        }



        private void clearBtn_Click(object sender, EventArgs e)
        {
            productNameTxbx.Clear();
            quantityTxbx.Clear();
            priceTxbx.Clear();
            stockValueTxbx.Clear();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DisplayProducts();
        }


        private void DisplayProducts()
        {
            dataGridView1.DataSource = logicInstance.GetAllProducts();
        }


        private void productNameTxbx_TextChanged(object sender, EventArgs e)
        {
            UpdateStockValue();

        }

        private void quantityTxbx_TextChanged(object sender, EventArgs e)
        {
            UpdateStockValue();

        }

        private void priceTxbx_TextChanged(object sender, EventArgs e)
        {
            UpdateStockValue();

        }


        private void stockValueTxbx_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(quantityTxbx.Text, out int quantity) && double.TryParse(priceTxbx.Text, out double price))
            {                
                double stockValue = quantity * price;
                stockValueTxbx.Text = stockValue.ToString(); 
            }

        }


        private void UpdateStockValue()
        {
            if (int.TryParse(quantityTxbx.Text, out int quantity) && double.TryParse(priceTxbx.Text, out double price))
            {
               
                double stockValue = quantity * price;
                stockValueTxbx.Text = stockValue.ToString(); 
            }
        }
    }
}
