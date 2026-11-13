using Chatbot;
using System;
using System.Collections.Generic;

namespace CybersecurityChatbot
{
    public static class ActivityLogger
    {
        private static List<string> logs = new List<string>();

        public static void LogAction(string action)
        {
            logs.Add($"{DateTime.Now:HH:mm} - {action}");
        }

        public static List<string> GetRecentLogs()
        {
            return logs;
        }
    }

    public class ChatEngine
    {
        private TaskManager taskManager = new TaskManager();

        private string userName = "";
        private string lastTopic = "";
        private bool isQuizActive = false;
        private int currentQuizQuestion = 0;
        private int quizScore = 0;

        public string ProcessMessage(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Please type a message or question so I can assist you.";

            string lowerInput = input.ToLower().Trim();

            if (isQuizActive && (lowerInput == "exit" || lowerInput == "quit"))
            {
                isQuizActive = false;
                ActivityLogger.LogAction("Exited Cybersecurity Quiz.");
                return "Quiz exited. Type 'quiz' anytime you want to restart!";
            }

            if (isQuizActive)
            {
                return HandleQuizInput(lowerInput);
            }

            // --- NAME CAPTURE & MEMORY ---
            if (lowerInput.StartsWith("my name is "))
            {
                userName = input.Substring(11).Trim();
                ActivityLogger.LogAction($"Set user name to {userName}.");
                return $"Nice to meet you, {userName}! How can I help you stay safe online today? You can ask about 'password safety', 'phishing', 'privacy', manage tasks, or type 'quiz'.";
            }

            // --- TASK ASSISTANT COMMANDS ---
            if (lowerInput.Contains("show tasks") || lowerInput.Contains("view tasks") || lowerInput.Contains("list tasks"))
            {
                var tasks = taskManager.GetAllTasks();
                ActivityLogger.LogAction("Viewed task list.");
                if (tasks.Count == 0)
                    return "You have no tasks stored in the database.";

                string taskListResponse = "Here are your current tasks:\n";
                foreach (var t in tasks)
                {
                    string status = t.IsCompleted ? "[Completed]" : "[Pending]";
                    string reminder = string.IsNullOrEmpty(t.ReminderDate) ? "No reminder" : $"Reminder: {t.ReminderDate}";
                    taskListResponse += $"{t.Id}. {t.Title}: {t.Description} {status} ({reminder})\n";
                }
                return taskListResponse.TrimEnd();
            }

            if (lowerInput.StartsWith("add task") || lowerInput.StartsWith("remind me"))
            {
                string details = input.Substring(input.IndexOf(' ') + 1).Trim();
                if (details.ToLower().StartsWith("task "))
                    details = details.Substring(5).Trim();

                string[] parts = details.Split('|');

                string title = parts.Length > 0 ? parts[0].Trim() : "General Task";
                string description = parts.Length > 1 ? parts[1].Trim() : "";
                string reminderDate = parts.Length > 2 ? parts[2].Trim() : "";

                taskManager.AddTask(title, description, reminderDate);
                ActivityLogger.LogAction($"Task added: '{title}'");

                return string.IsNullOrEmpty(reminderDate)
                    ? $"Task added: '{title}'. (Tip: You can add a description and reminder using '| Description | Date')"
                    : $"Task added: '{title}' with a reminder set for {reminderDate}.";
            }

            if (lowerInput.StartsWith("complete task"))
            {
                string idStr = lowerInput.Replace("complete task", "").Trim();
                if (int.TryParse(idStr, out int taskId))
                {
                    taskManager.CompleteTask(taskId);
                    ActivityLogger.LogAction($"Marked task ID {taskId} as completed.");
                    return $"Task {taskId} has been marked as completed!";
                }
                return "Please specify a valid task ID to complete (e.g., 'complete task 1').";
            }

            if (lowerInput.StartsWith("delete task"))
            {
                string idStr = lowerInput.Replace("delete task", "").Trim();
                if (int.TryParse(idStr, out int taskId))
                {
                    taskManager.DeleteTask(taskId);
                    ActivityLogger.LogAction($"Deleted task ID {taskId}.");
                    return $"Task {taskId} has been deleted from the database.";
                }
                return "Please specify a valid task ID to delete (e.g., 'delete task 1').";
            }

            // --- QUIZ & ACTIVITY LOG ---
            if (lowerInput.Contains("quiz") || lowerInput.Contains("start quiz"))
            {
                isQuizActive = true;
                currentQuizQuestion = 1;
                quizScore = 0;
                ActivityLogger.LogAction("Started Cybersecurity Quiz.");
                return "Starting Cybersecurity Quiz! (Type 'exit' to quit anytime).\n\nQuestion 1: What does 'phishing' typically refer to?\nA) Catching fish in a network\nB) Deceptive emails trying to steal sensitive information\nC) Upgrading firewall security\n(Reply with A, B, or C)";
            }

            if (lowerInput.Contains("activity log") || lowerInput.Contains("show log"))
            {
                ActivityLogger.LogAction("Viewed activity log.");
                var logs = ActivityLogger.GetRecentLogs();
                if (logs.Count == 0)
                    return "No activity recorded yet.";
                return "Recent Activity Log:\n" + string.Join("\n", logs);
            }

            // --- SENTIMENT DETECTION ---
            string nameGreeting = string.IsNullOrEmpty(userName) ? "" : $", {userName}";
            if (lowerInput.Contains("worried") || lowerInput.Contains("scared") || lowerInput.Contains("anxious"))
            {
                ActivityLogger.LogAction("Detected worried sentiment.");
                string topicNote = string.IsNullOrEmpty(lastTopic)
                    ? "Using strong passwords, enabling MFA, and verifying unexpected links will keep you well protected."
                    : $"Since we were just discussing {lastTopic}, staying alert and following best practices will keep you safe.";
                return $"It's completely understandable to feel worried{nameGreeting}. Online threats can seem overwhelming, but simple habits make a big difference. {topicNote}";
            }
            else if (lowerInput.Contains("curious") || lowerInput.Contains("interested"))
            {
                ActivityLogger.LogAction("Detected curious sentiment.");
                return $"I love your curiosity{nameGreeting}! Staying informed is the best defense against cyber threats. Ask me about 'password safety', 'phishing', or 'privacy' to dive deeper.";
            }

            // --- CYBERSECURITY TIPS ---
            if (lowerInput.Contains("password"))
            {
                lastTopic = "password safety";
                ActivityLogger.LogAction("Queried password safety.");
                return $"Password Safety Tip{nameGreeting}: Use strong passwords with a mix of letters, numbers, and symbols, and avoid reusing passwords across multiple accounts.";
            }
            else if (lowerInput.Contains("phishing") || lowerInput.Contains("scam"))
            {
                lastTopic = "phishing";
                ActivityLogger.LogAction("Queried phishing info.");
                return $"Phishing Tip{nameGreeting}: Always verify sender addresses and never click unexpected links or attachments from unknown sources.";
            }
            else if (lowerInput.Contains("privacy"))
            {
                lastTopic = "privacy";
                ActivityLogger.LogAction("Queried privacy info.");
                return $"Privacy Tip{nameGreeting}: Regularly review your social media and application permission settings to limit data exposure.";
            }

            ActivityLogger.LogAction($"Handled general query: {input}");
            return $"I'm your Cybersecurity Awareness Assistant{nameGreeting}. You can ask me about 'password safety', 'phishing', 'privacy', type 'quiz' to test your knowledge, or manage tasks ('show tasks', 'add task').";
        }

        private string HandleQuizInput(string input)
        {
            bool isCorrect = false;

            switch (currentQuizQuestion)
            {
                case 1: if (input == "b") isCorrect = true; break;
                case 2: if (input == "b") isCorrect = true; break;
                case 3: if (input == "c") isCorrect = true; break;
                case 4: if (input == "b") isCorrect = true; break;
                case 5: if (input == "a") isCorrect = true; break;
                case 6: if (input == "c") isCorrect = true; break;
                case 7: if (input == "b") isCorrect = true; break;
                case 8: if (input == "a") isCorrect = true; break;
                case 9: if (input == "c") isCorrect = true; break;
                case 10: if (input == "b") isCorrect = true; break;
            }

            string feedback = isCorrect
                ? "Correct! Great job.\n\n"
                : "Incorrect. Keep going!\n\n";

            if (isCorrect) quizScore++;
            currentQuizQuestion++;

            if (currentQuizQuestion > 10)
            {
                isQuizActive = false;
                ActivityLogger.LogAction($"Completed quiz with score {quizScore}/10.");
                int finalScore = quizScore;
                quizScore = 0;
                currentQuizQuestion = 0;
                return $"{feedback}Quiz finished! You scored {finalScore} out of 10. Great job improving your cybersecurity awareness!";
            }

            return feedback + GetQuestionText(currentQuizQuestion);
        }

        private string GetQuestionText(int questionNumber)
        {
            switch (questionNumber)
            {
                case 2: return "Question 2: What does MFA stand for?\nA) Multiple File Access\nB) Multi-Factor Authentication\nC) Main Firewall Architecture\n(Reply A, B, or C)";
                case 3: return "Question 3: Which of these is an example of a strong password?\nA) password123\nB) John2026\nC) K#9mP!2xL$8\n(Reply A, B, or C)";
                case 4: return "Question 4: What is ransomware?\nA) Free antivirus software\nB) Malware that encrypts files until a ransom is paid\nC) A secure cloud storage tool\n(Reply A, B, or C)";
                case 5: return "Question 5: Why should you avoid public Wi-Fi for sensitive logins without a VPN?\nA) Data can be intercepted by hackers on the same network\nB) It uses up too much mobile data\nC) It slows down your browser\n(Reply A, B, or C)";
                case 6: return "Question 6: What is a social engineering attack?\nA) A bug in social media software\nB) A network hardware failure\nC) Manipulating individuals into handing over confidential information\n(Reply A, B, or C)";
                case 7: return "Question 7: What does HTTPS ensure on a website?\nA) The website loads faster\nB) Traffic between your browser and the site is encrypted\nC) The website is completely immune to hackers\n(Reply A, B, or C)";
                case 8: return "Question 8: How often should you update your software and operating system?\nA) As soon as security updates and patches are available\nB) Once every five years\nC) Only when the computer stops working\n(Reply A, B, or C)";
                case 9: return "Question 9: What is a zero-day vulnerability?\nA) A bug that takes zero days to fix\nB) A virus that deletes data at midnight\nC) An unknown software flaw exploited by attackers before a fix is released\n(Reply A, B, or C)";
                case 10: return "Question 10: What is the safest way to dispose of old documents containing personal info?\nA) Throw them straight into the public trash bin\nB) Shred them securely before disposal\nC) Burn them in your backyard\n(Reply A, B, or C)";
                default: return "";
            }
        }
    }
}