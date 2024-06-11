using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
namespace BusinessLayer
{
    public class BusinessLogic
    {
        public  DataLogic data = new DataLogic();
        public  string connectionString = "Data Source=LAPTOP-4GNDOKQG;Initial Catalog=SandileQuickstore;Integrated Security=True;Encrypt=False";


        public void SaveProduct(string productName, int quantity, double price, double stockValue)
        {
            data.SaveProduct(productName, quantity, price, stockValue);
        }


        public void UpdateProduct(string productName, int quantity, double price, double stockValue)
        {
            data.UpdateProduct(productName, quantity, price, stockValue);
        }


        public void DeleteProduct(string productName)
        {
            
            string deleteQuery = "DELETE FROM PRODUCTS WHERE Product_Name = @ProductName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", productName);
                    command.ExecuteNonQuery();
                }
            }
        }
        public List<Product> GetAllProducts()
        {
            return GetProductsByLowStock(5);            

        }


        public List<Product> GetProductsByLowStock(int threshold)
        {
            return data.GetProductsByLowStock(threshold);
        }

       
    }

    


}


