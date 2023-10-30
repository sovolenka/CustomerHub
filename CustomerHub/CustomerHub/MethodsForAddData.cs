using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace CustomerHub
{
    internal partial class Program
    {
        static void AddUser(string connectionString, int id, string email, string password)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                INSERT INTO users (id, email, password)
                VALUES ($id, $email, $password)
            ";
                command.Parameters.AddWithValue("$id", id);
                command.Parameters.AddWithValue("$email", email);
                command.Parameters.AddWithValue("$password", password);

                command.ExecuteNonQuery();
            }
        }

        static void AddCharacteristics(string connectionString, int id, string productType, string category, string description, string manufacturer, string country, string manufactureDate, string characteristicsStatus, int productsId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                INSERT INTO characteristics (id, product_type, category, description, manufacturer, country, manufacture_date, characteristics_status, products_id)
                VALUES ($id, $productType, $category, $description, $manufacturer, $country, $manufactureDate, $characteristicsStatus, $productsId)
            ";
                command.Parameters.AddWithValue("$id", id);
                command.Parameters.AddWithValue("$productType", productType);
                command.Parameters.AddWithValue("$category", category);
                command.Parameters.AddWithValue("$description", description);
                command.Parameters.AddWithValue("$manufacturer", manufacturer);
                command.Parameters.AddWithValue("$country", country);
                command.Parameters.AddWithValue("$manufactureDate", manufactureDate);
                command.Parameters.AddWithValue("$characteristicsStatus", characteristicsStatus);
                command.Parameters.AddWithValue("$productsId", productsId);

                command.ExecuteNonQuery();
            }
        }

        static void AddProduct(string connectionString, int id, string name, double price, int clientsId, int usersId, int characteristicsId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                INSERT INTO products (id, name, price, clients_id, users_id, characteristics_id)
                VALUES ($id, $name, $price, $clientsId, $usersId, $characteristicsId)
            ";
                command.Parameters.AddWithValue("$id", id);
                command.Parameters.AddWithValue("$name", name);
                command.Parameters.AddWithValue("$price", price);
                command.Parameters.AddWithValue("$clientsId", clientsId);
                command.Parameters.AddWithValue("$usersId", usersId);
                command.Parameters.AddWithValue("$characteristicsId", characteristicsId);

                command.ExecuteNonQuery();
            }
        }

        static void AddClient(string connectionString, int id, string firstName, string secondName, string thirdName, string phoneNumber, string email, string address, string factory, string dateAdded, string clientStatus, int usersId, int productsId, int remindersId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                INSERT INTO clients (id, first_name, second_name, third_name, phone_number, email, address, factory, date_added, client_status, users_id, products_id, reminders_id)
                VALUES ($id, $firstName, $secondName, $thirdName, $phoneNumber, $email, $address, $factory, $dateAdded, $clientStatus, $usersId, $productsId, $remindersId)
            ";
                command.Parameters.AddWithValue("$id", id);
                command.Parameters.AddWithValue("$firstName", firstName);
                command.Parameters.AddWithValue("$secondName", secondName);
                command.Parameters.AddWithValue("$thirdName", thirdName);
                command.Parameters.AddWithValue("$phoneNumber", phoneNumber);
                command.Parameters.AddWithValue("$email", email);
                command.Parameters.AddWithValue("$address", address);
                command.Parameters.AddWithValue("$factory", factory);
                command.Parameters.AddWithValue("$dateAdded", dateAdded);
                command.Parameters.AddWithValue("$clientStatus", clientStatus);
                command.Parameters.AddWithValue("$usersId", usersId);
                command.Parameters.AddWithValue("$productsId", productsId);
                command.Parameters.AddWithValue("$remindersId", remindersId);

                command.ExecuteNonQuery();
            }
        }

        static void AddReminder(string connectionString, int id, string note, string reminder, int clientsId, int usersId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                INSERT INTO reminders (id, note, reminder, clients_id, users_id)
                VALUES ($id, $note, $reminder, $clientsId, $usersId)
            ";
                command.Parameters.AddWithValue("$id", id);
                command.Parameters.AddWithValue("$note", note);
                command.Parameters.AddWithValue("$reminder", reminder);
                command.Parameters.AddWithValue("$clientsId", clientsId);
                command.Parameters.AddWithValue("$usersId", usersId);

                command.ExecuteNonQuery();
            }
        }

        static void PrintUsers(string connectionString)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM users";

                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("Users:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"User ID: {reader["id"]}, Email: {reader["email"]}, Password: {reader["password"]}");
                    }
                }
            }
        }

        static void PrintCharacteristics(string connectionString)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM characteristics";

                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("Characteristics:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Characteristics ID: {reader["id"]}, Product Type: {reader["product_type"]}, Category: {reader["category"]}, Description: {reader["description"]}");
                    }
                }
            }
        }

        static void PrintProducts(string connectionString)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM products";

                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("Products:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Product ID: {reader["id"]}, Name: {reader["name"]}, Price: {reader["price"]}, Clients ID: {reader["clients_id"]}, Users ID: {reader["users_id"]}, Characteristics ID: {reader["characteristics_id"]}");
                    }
                }
            }
        }

        static void PrintClients(string connectionString)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM clients";

                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("Clients:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Client ID: {reader["id"]}, First Name: {reader["first_name"]}, Second Name: {reader["second_name"]}, Third Name: {reader["third_name"]}, Phone Number: {reader["phone_number"]}, Email: {reader["email"]}, Address: {reader["address"]}, Factory: {reader["factory"]}, Date Added: {reader["date_added"]}, Client Status: {reader["client_status"]}, Users ID: {reader["users_id"]}, Products ID: {reader["products_id"]}, Reminders ID: {reader["reminders_id"]}");
                    }
                }
            }
        }

        static void PrintReminders(string connectionString)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM reminders";

                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("Reminders:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Reminder ID: {reader["id"]}, Note: {reader["note"]}, Reminder: {reader["reminder"]}, Clients ID: {reader["clients_id"]}, Users ID: {reader["users_id"]}");
                    }
                }
            }
        }

    }
}