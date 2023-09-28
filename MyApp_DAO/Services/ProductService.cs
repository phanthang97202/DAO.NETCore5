using MyApp_DAO.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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


        public Product GetDetailProduct ()
        {

            return { };
        }
    }
}