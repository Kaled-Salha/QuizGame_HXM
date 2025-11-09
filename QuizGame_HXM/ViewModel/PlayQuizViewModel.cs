using Microsoft.Win32;
using QuizGame_HXM.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace QuizGame_HXM.ViewModel
{
    public class PlayQuizViewModel : INotifyPropertyChanged
    {
        public Quiz Quiz { get; set; }
        public Question CurrentQuestion { get; set; }
        public int TotalCorrect { get; set; }
        public int TotalAnswered { get; set; }

        public event EventHandler BackToMenuRequested;
        private void OnBackToMenuRequested()
        {
            BackToMenuRequested?.Invoke(this, EventArgs.Empty);
        }
        public void RequestBackToMenu()
        {
            OnBackToMenuRequested();
        }

        private string _scoreText;
        public string ScoreText
        {
            get => _scoreText;
            set
            {
                _scoreText = value;
                OnPropertyChanged(nameof(ScoreText));
            }
        }

        private string _answer1;
        public string Answer1
        {
            get => _answer1;
            set
            {
                _answer1 = value;
                OnPropertyChanged(nameof(Answer1));
            }
        }

        private string _answer2;
        public string Answer2
        {
            get => _answer2;
            set
            {
                _answer2 = value;
                OnPropertyChanged(nameof(Answer2));
            }
        }

        private string _answer3;
        public string Answer3
        {
            get => _answer3;
            set
            {
                _answer3 = value;
                OnPropertyChanged(nameof(Answer3));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // 🔹 NEW: Helper method to update answer text properties
        private void UpdateAnswerOptions()
        {
            if (CurrentQuestion != null && CurrentQuestion.AnswerOptions != null)
            {
                Answer1 = CurrentQuestion.AnswerOptions.Count > 0 ? CurrentQuestion.AnswerOptions[0] : "";
                Answer2 = CurrentQuestion.AnswerOptions.Count > 1 ? CurrentQuestion.AnswerOptions[1] : "";
                Answer3 = CurrentQuestion.AnswerOptions.Count > 2 ? CurrentQuestion.AnswerOptions[2] : "";

                OnPropertyChanged(nameof(Answer1));
                OnPropertyChanged(nameof(Answer2));
                OnPropertyChanged(nameof(Answer3));
            }
            else
            {
                Answer1 = Answer2 = Answer3 = "";
            }
        }

        public void NextQuestion(int selectedIndex)
        {
            if (CurrentQuestion == null)
            {
                Debug.WriteLine("⚠️ No question loaded");
                return;
            }

            Debug.WriteLine($"✅ Question loaded: {CurrentQuestion.QuestionText}");
            TotalAnswered++;

            if (selectedIndex == CurrentQuestion.CorrectAnswerIndex)
                TotalCorrect++;

            int percent = (int)((double)TotalCorrect / TotalAnswered * 100);
            ScoreText = $"Correct: {TotalCorrect} / {TotalAnswered} ({percent}%)";

            // Move to next question
            CurrentQuestion = Quiz.GetRandomQuestion();
            UpdateAnswerOptions();

            OnPropertyChanged(nameof(CurrentQuestion));
        }

        public async Task LoadQuizAsync()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json"
            };
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filePath = dialog.FileName;
                string json = await File.ReadAllTextAsync(filePath);
                Quiz = JsonSerializer.Deserialize<Quiz>(json);

                if (Quiz != null)
                {
                    Debug.WriteLine($"✅ Quiz loaded: {Quiz.Title}, {Quiz.Questions?.Count ?? 0} questions");

                    CurrentQuestion = Quiz.GetRandomQuestion();
                    UpdateAnswerOptions();

                    OnPropertyChanged(nameof(CurrentQuestion));
                    OnPropertyChanged(nameof(Quiz));
                }
                else
                {
                    Debug.WriteLine("❌ Quiz failed to load");
                }
            }
        }

        public async Task SaveQuizAsync()
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json"
            };

            bool? result = saveDialog.ShowDialog();
            if (result == true)
            {
                string filePath = saveDialog.FileName;
                string json = JsonSerializer.Serialize(this.Quiz);
                await File.WriteAllTextAsync(filePath, json);
            }
        }

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
