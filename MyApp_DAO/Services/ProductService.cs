using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Product = MyApp_DAO.Models.Product;

namespace MyApp_DAO.Services
{
    public class ProductService
    {
        private readonly string _connectionDB;
        public ProductService(string connectionDB)
        {
            _connectionDB = connectionDB;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> productList = new List<Product>();

            try
            {
                using (var connection = new SqlConnection(_connectionDB))
                {
                    connection.Open();
                    using var command = new SqlCommand();
                    command.Connection = connection;
                    string query = @"SELECT * FROM product";
                    command.CommandText = query;
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            productList.Add(
                                new Product()
                                {
                                    id = (int)reader["id"],
                                    name = (string)reader["name"],
                                    price = (int)reader["price"],
                                    description = (string)reader["description"],
                                    category = (string)reader["category"]
                                });
                        }
                    }
                    else
                    {
                        return new List<Product>();
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return productList;
        }


        public List<Product> GetDetailProduct(int id)
        {

            List<Product> productList = new List<Product>();

            try
            {
                using (var connection = new SqlConnection(_connectionDB))
                {
                    connection.Open();
                    using var command = new SqlCommand();
                    command.Connection = connection;
                    string query = @"SELECT * FROM product where id = @id";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@id", id);
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            productList.Add(
                                new Product()
                                {
                                    id = (int)reader["id"],
                                    name = (string)reader["name"],
                                    price = (int)reader["price"],
                                    description = (string)reader["description"],
                                    category = (string)reader["category"]
                                });
                        }
                    }
                    else
                    {
                        return new List<Product>();
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return productList;
        }

        // thêm sản phẩm

        public int CreateNewProduct(Product product)
        {

            try
            {
                using (var connection = new SqlConnection(_connectionDB))
                {
                    connection.Open();
                    using var command = new SqlCommand();
                    command.Connection = connection;
                    string query = @"insert into product values (@name, @price, @description, @category)";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@name", product.name);
                    command.Parameters.AddWithValue("@price", product.price);
                    command.Parameters.AddWithValue("@description", product.description);
                    command.Parameters.AddWithValue("@category", product.category);
                    var reader = command.ExecuteNonQuery();
                    connection.Close();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 0;
            }
        }

        internal int CreateNewProduct(Controllers.Product product)
        {
            throw new NotImplementedException();
        }
    }
}