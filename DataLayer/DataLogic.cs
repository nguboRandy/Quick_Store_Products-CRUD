using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Product
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double StockValue { get; set; }
    }

    public class DataLogic
    {
        public  string connectionString = "Data Source=LAPTOP-4GNDOKQG;Initial Catalog=SandileQuickstore;Integrated Security=True;Encrypt=False";

        public void SaveProduct(string productName, int quantity, double price, double stockValue)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO PRODUCTS (Product_Name, Quantity, Price, Stock_Value) " +
                                     "VALUES (@ProductName, @Quantity, @Price, @StockValue)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    
                    command.Parameters.AddWithValue("@ProductName", productName);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@StockValue", stockValue);
                    command.ExecuteNonQuery();
                }
            }
        }




        public void UpdateProduct(string productName, int quantity, double price, double stockValue)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE PRODUCTS " +
                                     "SET Quantity = @Quantity, Price = @Price, Stock_Value = @StockValue " +
                                     "WHERE Product_Name = @ProductName";
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", productName);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@StockValue", stockValue);
                    command.ExecuteNonQuery();
                }
            }
        }


        public void DeleteProduct(string productName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM PRODUCTS WHERE Product_Name = @ProductName";
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", productName);
                    command.ExecuteNonQuery();
                }
            }
        }


        public List<Product> GetProductsByLowStock(int threshold)
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT Product_Name, Quantity, Price, Stock_Value " +
                                     "FROM PRODUCTS " +
                                     "WHERE Quantity < @Threshold";
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Threshold", threshold);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                ProductName = reader["Product_Name"].ToString(),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                Price = Convert.ToDouble(reader["Price"]),
                                StockValue = Convert.ToDouble(reader["Stock_Value"])
                            });
                        }
                    }
                }
            }

            return products;
        }



        public DataTable GetAllProducts()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT Product_Name, Quantity, Price, Stock_Value FROM PRODUCTS";
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }
    }

    



}

