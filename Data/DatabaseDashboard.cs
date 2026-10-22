using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using Дипломочка.Models;

namespace Дипломочка
{
    public partial class Database
    {
        public int GetProductCount()
        {
            using (var connection =
                   new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT COUNT(*) FROM Products";

                using (var command =
                       new SqliteCommand(sql, connection))
                {
                    return System.Convert.ToInt32(
                        command.ExecuteScalar());
                }
            }
        }

        public int GetCategoryCount()
        {
            using (var connection =
                   new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = @"
                    SELECT COUNT(DISTINCT Category)
                    FROM Products
                    WHERE Category IS NOT NULL
                      AND TRIM(Category) <> ''";

                using (var command =
                       new SqliteCommand(sql, connection))
                {
                    return System.Convert.ToInt32(
                        command.ExecuteScalar());
                }
            }
        }

        public int GetLowStockCount()
        {
            using (var connection =
                   new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = @"
                    SELECT COUNT(*)
                    FROM Products
                    WHERE Quantity <= 5";

                using (var command =
                       new SqliteCommand(sql, connection))
                {
                    return System.Convert.ToInt32(
                        command.ExecuteScalar());
                }
            }
        }

        public List<StockOperation> GetRecentOperations()
        {
            List<StockOperation> operations =
                new List<StockOperation>();

            using (var connection =
                   new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = @"
                    SELECT Id, ProductId, ProductName,
                           OperationType, Quantity, OperationDate
                    FROM Operations
                    ORDER BY Id DESC
                    LIMIT 10";

                using (var command =
                       new SqliteCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        StockOperation operation =
                            new StockOperation
                            {
                                Id = reader.GetInt32(0),
                                ProductId = reader.GetInt32(1),
                                ProductName = reader.GetString(2),
                                OperationType = reader.GetString(3),
                                Quantity = reader.GetInt32(4),
                                OperationDate = reader.GetString(5)
                            };

                        operations.Add(operation);
                    }
                }
            }

            return operations;
        }
    }
}
