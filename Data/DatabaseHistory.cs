using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using Дипломочка.Models;

namespace Дипломочка
{
    public partial class Database
    {
        public List<StockOperation> GetOperations()
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
                    ORDER BY Id DESC";

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
