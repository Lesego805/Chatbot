using Chatbot;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace CybersecurityChatbot
{
    public partial class MainWindow : Window
    {
        private ChatEngine chatEngine;
        private TaskManager taskManager;

        public MainWindow()
        {
            InitializeComponent();

            // --- AUDIO INITIALIZATION & DEBUGGING ---
            try
            {
                string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "greeting.wav");
                if (File.Exists(audioPath))
                {
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(audioPath);
                    player.Play();
                }
                else
                {
                    MessageBox.Show($"Audio file not found at: {audioPath}\n\nMake sure 'greeting.wav' in the Assets folder is set to 'Content' and 'Copy always'.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Audio error: {ex.Message}");
            }
            // ----------------------------------------

            chatEngine = new ChatEngine();
            taskManager = new TaskManager();

            AppendChat("Chatbot: Hello! Welcome to the Cybersecurity Awareness Assistant. What is your name? Or ask me about 'password safety', 'phishing', 'privacy', or type 'quiz'!");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Optional startup logic if needed
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            ProcessUserInput();
        }

        private void TxtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProcessUserInput();
            }
        }

        private void ProcessUserInput()
        {
            string userInput = TxtUserInput.Text.Trim();

            if (string.IsNullOrEmpty(userInput))
                return;

            AppendChat($"[You]: {userInput}");
            TxtUserInput.Clear();

            string botResponse = chatEngine.ProcessMessage(userInput);
            AppendChat($"[Chatbot]: {botResponse}");
        }

        private void AppendChat(string message)
        {
            TxtChatLog.Text += message + "\n\n";
            ChatScrollViewer.ScrollToEnd();
        }
    }
}