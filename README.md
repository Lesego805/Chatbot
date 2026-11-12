# Cybersecurity Awareness Chatbot (WPF Desktop Application)

**Student Name:** Lesego  
**Student Number:** ST10499067  
**Repository:** `ST10499067_Chatbot_Part3`  

## Overview
The **Cybersecurity Awareness Chatbot** is a C# .NET 8.0 Windows Presentation Foundation (WPF) desktop application designed to educate users on essential online safety practices. It combines natural keyword recognition, sentiment detection, and conversational memory with interactive productivity tools—including a cybersecurity Task Assistant with reminders, an interactive mini-game quiz, and a real-time Activity Log.

---

## Core Features
* **Audio Welcome Greeting:** Plays an automated voice greeting upon application startup via `AudioPlayer.cs`.
* **Interactive Cybersecurity Q&A (`ChatEngine.cs`):** Recognizes cybersecurity keywords (phishing, malware, password safety, safe browsing, two-factor authentication) and provides actionable tips[cite: 1, 3].
* **Sentiment Detection & Memory Recall:** Adapts responses based on the user's emotional tone (e.g., *worried*, *curious*, *frustrated*) and remembers the user's name and favorite cybersecurity topic across the conversation.
* **Task Assistant & Reminders:** Allows users to create, view, complete, and delete cybersecurity tasks with optional reminder dates.
* **Cybersecurity Mini-Game Quiz:** A multi-question interactive quiz featuring multiple-choice and true/false questions with instant explanations and score tracking.
* **Activity Log:** Automatically records and displays a chronological history of user actions (tasks added, reminders set, quiz attempts).

---

## Project Architecture
The solution follows a modular separation of concerns between the user interface and core business logic:
* `MainWindow.xaml` / `MainWindow.xaml.cs` – Defines the WPF graphical user interface, layout styling, and UI event handlers.
* `ChatEngine.cs` – Handles natural language keyword parsing, sentiment detection, conversational memory, and dynamic response generation[cite: 1, 3].
* `AudioPlayer.cs` – Manages playback of the startup WAV welcome audio greeting[cite: 3, 5].
* *Task & Quiz Models/Managers* – Encapsulates task item properties, reminder tracking, quiz questions, and activity logging.

---

## Prerequisites & Setup Steps

### 1. System Requirements
* **Operating System:** Windows 10 or Windows 11
* **IDE:** Visual Studio 2022 (or newer) with the **.NET Desktop Development** workload installed
* **Framework:** .NET 8.0 SDK (`net8.0-windows`)[cite: 3, 5]

### 2. Cloning and Running the Project
1. **Clone the repository:**
   ```bash
   git clone [https://github.com/Lesego805/ST10499067_Chatbot_Part3.git](https://github.com/Lesego805/ST10499067_Chatbot_Part3.git)
