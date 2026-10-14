# Cybersecurity Awareness Assistant Chatbot (Part 2)

An interactive WPF (Windows Presentation Foundation) desktop application designed to educate users on essential cybersecurity concepts, best practices and online safety.

---

## 🚀 Features

* **Interactive WPF GUI:** Clean, dark-themed user interface built with custom styling for easy readability and engagement.
* **Voice Audio Greeting:** Automatically plays a recorded audio message upon launch welcoming the user using `System.Windows.Media.MediaPlayer`.
* **Keyword & Topic Recognition:** Responds accurately to inquiries regarding **passwords**, **phishing**, **privacy** and general cyber safety tips.
* **Dynamic & Random Responses:** Uses collections of varied responses to keep conversations engaging and natural.
* **User Memory & Context Tracking:**
  * Remembers user names (`"My name is..."`).
  * Remembers favorite topics (`"My favorite topic is..."`).
  * Tracks conversation history and recalls past topics upon request.
* **Sentiment & Concern Detection:** Recognizes user anxiety or concern regarding scams/threats and responds with empathetic, reassuring advice.
* **Guided Follow-Up System:** Prompts users with logical follow-up questions to deepen educational interaction.

---

## 🛠️ Project Structure

* **`MainWindow.xaml` / `MainWindow.xaml.cs`**: XAML layout and UI event handlers.
* **`ChatEngine.cs`**: Core conversation logic, keyword detection, sentiment analysis, and random response selections.
* **`UserMemory.cs`**: Data model responsible for storing and retrieving user-specific memory fields.
* **`AudioPlayer.cs`**: Manages media playback for startup voice greetings and sound effects.
* **`Assets/`**: Contains embedded media assets such as `greeting.wav`.

---

## 📋 Pre-Requisites & Requirements

* **Framework:** .NET 6.0 / .NET 8.0 SDK or higher
* **IDE:** Visual Studio 2022 / 2026 with WPF workload installed
* **OS:** Windows 10 / 11

---

## 🔧 Installation & Setup

1. **Clone the Repository:**
   ```bash
   git clone [https://github.com/Lesego805/Chatbot.git](https://github.com/Lesego805/Chatbot.git)
