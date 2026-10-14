using System;
using System.Collections.Generic;

namespace CybersecurityChatbot
{
    // C# Delegate declaration for processing messages
    public delegate string MessageProcessorDelegate(string userInput);

    public class ChatEngine
    {
        public UserMemory Memory { get; private set; } = new UserMemory();
        private readonly Random _random = new Random();

        // Collections storing dynamic responses
        private readonly List<string> _phishingTips = new List<string>
        {
            "Be cautious of emails asking for urgent personal information. Scammers often disguise themselves as trusted organisations.",
            "Always inspect the sender's full email address to spot subtle typos or strange domain extensions.",
            "Avoid clicking on unknown links or downloading unexpected attachments from unfamiliar senders."
        };

        private readonly List<string> _passwordTips = new List<string>
        {
            "Make sure to use strong, unique passwords for each account. Avoid using personal details in your passwords.",
            "Consider using a passphrase made of 4+ random words for superior security and memorable complexity.",
            "Enable Two-Factor Authentication (2FA) wherever possible to add an extra layer of defense."
        };

        private readonly List<string> _privacyTips = new List<string>
        {
            "Review your social media privacy settings regularly to limit who can see your personal updates.",
            "Avoid sharing sensitive information like your home address or phone number publicly online.",
            "Use encrypted messaging tools and turn off location sharing for non-essential applications."
        };

        public string ProcessMessage(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "I'm not sure I understand. Can you try rephrasing?";

            string lowerInput = input.ToLower();

            // 1. Memory Capture (Detect Name)
            if (lowerInput.StartsWith("my name is "))
            {
                string name = input.Substring(11).Trim();
                Memory.UserName = name;
                return $"It's great to meet you, {Memory.UserName}! How can I assist you with cybersecurity today?";
            }

            // 2. Sentiment Detection
            string sentimentPrefix = string.Empty;
            if (lowerInput.Contains("worried") || lowerInput.Contains("scared") || lowerInput.Contains("anxious"))
            {
                sentimentPrefix = "It's completely understandable to feel that way. Online security can be intimidating, but I'm here to help you stay safe. ";
            }
            else if (lowerInput.Contains("curious") || lowerInput.Contains("interested"))
            {
                sentimentPrefix = "That's fantastic! Staying curious is one of the best ways to protect yourself online. ";
            }
            else if (lowerInput.Contains("frustrated") || lowerInput.Contains("confused"))
            {
                sentimentPrefix = "Don't worry! Cybersecurity concepts can be tricky at first, but we can break it down together. ";
            }

            // 3. Natural Conversation Flow (Follow-ups)
            if (lowerInput.Contains("another tip") || lowerInput.Contains("explain more") || lowerInput.Contains("tell me more"))
            {
                if (!string.IsNullOrEmpty(Memory.LastTopicDiscussed))
                {
                    return GetTopicResponse(Memory.LastTopicDiscussed, sentimentPrefix);
                }
            }

            // 4. Keyword Recognition
            if (lowerInput.Contains("password"))
            {
                Memory.LastTopicDiscussed = "password";
                Memory.FavoriteTopic = "Password Security";
                return sentimentPrefix + GetRandomTip(_passwordTips);
            }
            if (lowerInput.Contains("phishing") || lowerInput.Contains("scam"))
            {
                Memory.LastTopicDiscussed = "phishing";
                Memory.FavoriteTopic = "Phishing Prevention";
                return sentimentPrefix + GetRandomTip(_phishingTips);
            }
            if (lowerInput.Contains("privacy"))
            {
                Memory.LastTopicDiscussed = "privacy";
                Memory.FavoriteTopic = "Online Privacy";
                return sentimentPrefix + GetRandomTip(_privacyTips);
            }

            // 5. Memory Recall
            if (lowerInput.Contains("what is my name") || lowerInput.Contains("who am i"))
            {
                return string.IsNullOrEmpty(Memory.UserName)
                    ? "You haven't told me your name yet! You can say 'My name is [Name]'."
                    : $"Your name is {Memory.UserName}!";
            }

            // 6. Error Handling / Fallback
            return "I'm not sure I understand. Can you try rephrasing? You can ask me about password safety, phishing scams, or online privacy.";
        }

        private string GetRandomTip(List<string> tips)
        {
            int index = _random.Next(tips.Count);
            return tips[index];
        }

        private string GetTopicResponse(string topic, string prefix)
        {
            return topic switch
            {
                "password" => prefix + GetRandomTip(_passwordTips),
                "phishing" => prefix + GetRandomTip(_phishingTips),
                "privacy" => prefix + GetRandomTip(_privacyTips),
                _ => "Feel free to ask a question about phishing, passwords, or privacy!"
            };
        }
    }
}