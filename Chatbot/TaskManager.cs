using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Chatbot
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ReminderDate { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }

    public class TaskManager
    {
        // Add a new task to the MySQL database
        public void AddTask(string title, string description, string reminderDate)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Tasks (Title, Description, ReminderDate, IsCompleted) VALUES (@Title, @Description, @ReminderDate, 0)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@ReminderDate", string.IsNullOrEmpty(reminderDate) ? (object)DBNull.Value : reminderDate);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Retrieve all tasks from the database
        public List<TaskItem> GetAllTasks()
        {
            var tasks = new List<TaskItem>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT Id, Title, Description, ReminderDate, IsCompleted FROM Tasks";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasks.Add(new TaskItem
                            {
                                Id = reader.GetInt32("Id"),
                                Title = reader.GetString("Title"),
                                Description = reader.GetString("Description"),
                                ReminderDate = reader.IsDBNull(reader.GetOrdinal("ReminderDate")) ? string.Empty : reader.GetString("ReminderDate"),
                                IsCompleted = reader.GetBoolean("IsCompleted")
                            });
                        }
                    }
                }
            }
            return tasks;
        }

        // Mark a task as completed
        public void CompleteTask(int taskId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Tasks SET IsCompleted = 1 WHERE Id = @Id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", taskId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Delete a task from the database
        public void DeleteTask(int taskId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Tasks WHERE Id = @Id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", taskId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}