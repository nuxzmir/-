using Microsoft.Data.Sqlite;
using System;

namespace Дипломочка
{
    public partial class Database
    {
        public void RegisterOperation(
            int productId,
            string operationType,
            int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException(
                    "Quantity must be greater than zero.");

            if (operationType != "Income" &&
                operationType != "Expense")
            {
                throw new ArgumentException(
                    "Invalid operation type.");
            }

            using (var connection =
                   new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var transaction =
                       connection.BeginTransaction())
                {
                    string productName;
                    int currentQuantity;

                    string selectSql = @"
                        SELECT Name, Quantity
                        FROM Products
                        WHERE Id = $id";

                    using (var command = new SqliteCommand(
                        selectSql, connection, transaction))
                    {
                        command.Parameters.AddWithValue(
                            "$id", productId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                                throw new InvalidOperationException(
                                    "Product not found.");

                            productName = reader.GetString(0);
                            currentQuantity = reader.GetInt32(1);
                        }
                    }

                    int newQuantity;

                    if (operationType == "Income")
                    {
                        newQuantity = checked(
                            currentQuantity + quantity);
                    }
                    else
                    {
                        if (quantity > currentQuantity)
                            throw new InvalidOperationException(
                                "Not enough stock.");

                        newQuantity = currentQuantity - quantity;
                    }

                    string updateSql = @"
                        UPDATE Products
                        SET Quantity = $quantity
                        WHERE Id = $id";

                    using (var command = new SqliteCommand(
                        updateSql, connection, transaction))
                    {
                        command.Parameters.AddWithValue(
                            "$quantity", newQuantity);

                        command.Parameters.AddWithValue(
                            "$id", productId);

                        command.ExecuteNonQuery();
                    }

                    string insertSql = @"
                        INSERT INTO Operations
                        (ProductId, ProductName, OperationType,
                         Quantity, OperationDate)
                        VALUES
                        ($productId, $productName, $operationType,
                         $quantity, $date)";

                    using (var command = new SqliteCommand(
                        insertSql, connection, transaction))
                    {
                        command.Parameters.AddWithValue(
                            "$productId", productId);

                        command.Parameters.AddWithValue(
                            "$productName", productName);

                        command.Parameters.AddWithValue(
                            "$operationType", operationType);

                        command.Parameters.AddWithValue(
                            "$quantity", quantity);

                        command.Parameters.AddWithValue(
                            "$date",
                            DateTime.Now.ToString(
                                "yyyy-MM-dd HH:mm:ss"));

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }
        }
    }
}
