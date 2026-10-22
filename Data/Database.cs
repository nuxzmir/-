using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Дипломочка.Models;

//База Данных
namespace Дипломочка
{
    public partial class Database
    {
        private string connectionString = "Data Source=warehouse.db";
        //Создание БД
        public void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string productsSql = @"
            CREATE TABLE IF NOT EXISTS Products
            (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Category TEXT,
                Quantity INTEGER NOT NULL,
                Price REAL NOT NULL
            )";

                using (var command = new SqliteCommand(productsSql, connection))
                {
                    command.ExecuteNonQuery();
                }

                string operationsSql = @"
            CREATE TABLE IF NOT EXISTS Operations
            (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ProductId INTEGER NOT NULL,
                ProductName TEXT NOT NULL,
                OperationType TEXT NOT NULL,
                Quantity INTEGER NOT NULL,
                OperationDate TEXT NOT NULL
            )";

                using (var command = new SqliteCommand(operationsSql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
