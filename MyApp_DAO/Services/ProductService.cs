using System;
using System.Collections.Generic;
using System.Data;
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


        // xóa 1/nhiều sản phẩm
        public int DeleteMultiProduct(int[] ids)
        {
            string listIds = "";
            try
            {
                using (var connection = new SqlConnection(_connectionDB))
                {
                    connection.Open();
                    for (var id = 0; id < ids.Length; id++)
                    {
                        if (id > 0)
                        {
                            listIds += "," + ids[id];

                        }
                        else
                        {
                            listIds += ids[id];
                        }
                    }
                    using var command = new SqlCommand();
                    command.Connection = connection;
                    string query = $"DELETE FROM product WHERE id IN ({listIds})";
                    command.CommandText = query;
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 0;
            }
        }

        // cập nhật sản phẩm
        public int UpdateProduct(Product product)
        {

            try
            {
                using (var connection = new SqlConnection(_connectionDB))
                {
                    connection.Open();
                    using var command = new SqlCommand();
                    command.Connection = connection;
                    string query = @"update product set name = @name, price = @price, description = @description, category = @category where id = @id";
                    command.CommandText = query;

                    command.Parameters.AddWithValue("@id", product.id);
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
    }
}