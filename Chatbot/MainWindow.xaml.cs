using System;
using System.Windows;
using System.Windows.Input;

namespace CybersecurityChatbot
{
    public partial class MainWindow : Window
    {
        private readonly ChatEngine _engine;
        private readonly MessageProcessorDelegate _messageProcessor;

        public MainWindow()
        {
            InitializeComponent();
            _engine = new ChatEngine();

            // Hook up the delegate required by assessment specifications
            _messageProcessor = new MessageProcessorDelegate(_engine.ProcessMessage);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Play WAV audio greeting safely on application start
            try
            {
                AudioPlayer.PlayGreeting();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Audio playback error: {ex.Message}");
            }

            // Display welcome message in the chat log
            AppendMessage("Chatbot", "Hello! Welcome to the Cybersecurity Awareness Assistant. " +
                                     "What is your name? Or ask me about 'password safety', 'phishing', or 'privacy'!");
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void TxtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
            }
        }

        private void SendMessage()
        {
            string userInput = TxtUserInput.Text.Trim();
            if (string.IsNullOrEmpty(userInput)) return;

            // Display user message in chat log
            AppendMessage("You", userInput);
            TxtUserInput.Clear();

            // Process response via delegate
            string response = _messageProcessor.Invoke(userInput);

            // Display chatbot response
            AppendMessage("Chatbot", response);
        }

        private void AppendMessage(string sender, string message)
        {
            TxtChatLog.Text += $"[{sender}]: {message}\n\n";
            ChatScrollViewer.ScrollToEnd();
        }
    }
}