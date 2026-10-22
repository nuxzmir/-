using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using Дипломочка.Models;

namespace Дипломочка
{
    public partial class Database
    {
        public void AddProduct(Product product)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = @"
                    INSERT INTO Products (Name, Category, Quantity, Price)
                    VALUES ($name, $category, $quantity, $price)";

                using (var command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("$name", product.Name);
                    command.Parameters.AddWithValue("$category", product.Category);
                    command.Parameters.AddWithValue("$quantity", product.Quantity);
                    command.Parameters.AddWithValue("$price", product.Price);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Product> GetProducts()
        {
            return SearchProducts("");
        }

        public List<Product> SearchProducts(string searchText)
        {
            List<Product> products = new List<Product>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = @"
                    SELECT Id, Name, Category, Quantity, Price
                    FROM Products
                    WHERE Name LIKE $search
                       OR Category LIKE $search
                    ORDER BY Id DESC";

                using (var command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue(
                        "$search", "%" + searchText + "%");

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Category = reader.IsDBNull(2)
                                    ? ""
                                    : reader.GetString(2),
                                Quantity = reader.GetInt32(3),
                                Price = Convert.ToDecimal(reader.GetValue(4))
                            };

                            products.Add(product);
                        }
                    }
                }
            }

            return products;
        }

        public void UpdateProduct(Product product)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = @"
                    UPDATE Products
                    SET Name = $name,
                        Category = $category,
                        Quantity = $quantity,
                        Price = $price
                    WHERE Id = $id";

                using (var command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("$id", product.Id);
                    command.Parameters.AddWithValue("$name", product.Name);
                    command.Parameters.AddWithValue("$category", product.Category);
                    command.Parameters.AddWithValue("$quantity", product.Quantity);
                    command.Parameters.AddWithValue("$price", product.Price);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProduct(int id)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = "DELETE FROM Products WHERE Id = $id";

                using (var command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("$id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}