using System;
using MySql.Data.MySqlClient;

namespace Chatbot
{
    public static class DatabaseHelper
    {
        // Replace 'YOUR_PASSWORD_HERE' with your actual MySQL root password
        private static string connectionString = "server=localhost;database=CybersecurityChatbotDB;uid=root;pwd=Gomolemo@30;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}