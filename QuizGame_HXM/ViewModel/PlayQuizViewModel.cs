using Microsoft.Win32;
using QuizGame_HXM.Models;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace QuizGame_HXM.ViewModel
{
    public class PlayQuizViewModel : INotifyPropertyChanged
    {
        public Quiz Quiz { get; set; }
        public Question CurrentQuestion { get; set; }
        public int CurrentQuestionIndex { get; set; }
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


        public event PropertyChangedEventHandler PropertyChanged;


        public void NextQuestion(int selectedIndex)
        {


            if (CurrentQuestion == null)
            {
                return;
            }

            TotalAnswered++;

            if (selectedIndex == CurrentQuestion.CorrectAnswerIndex)
            {
                TotalCorrect++;

            }

            int percent = (int)((double)TotalCorrect / TotalAnswered * 100);
            ScoreText = $"Correct: {TotalCorrect} / {TotalAnswered} ({percent}%)";


            CurrentQuestion = Quiz.GetRandomQuestion();

            OnPropertyChanged(nameof(CurrentQuestion));
        }

        public async Task LoadQuizAsync()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JSON files (*.json)|*.json";
            bool? result = dialog.ShowDialog();
            if (result == true)
            {

                string filePath = dialog.FileName;
                string json = await File.ReadAllTextAsync(filePath);
                Quiz = JsonSerializer.Deserialize<Quiz>(json);
                CurrentQuestion = Quiz.GetRandomQuestion();
                OnPropertyChanged(nameof(CurrentQuestion));
                OnPropertyChanged(nameof(Quiz));
            }


        }
        public async Task SaveQuizAsync()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "JSON files (*.json)|*.json";
            bool? result = saveDialog.ShowDialog();
            if (result == true)
            {
                string filePath = saveDialog.FileName;
                Quiz quiz = this.Quiz;
                string json = JsonSerializer.Serialize(quiz);
                await File.WriteAllTextAsync(filePath, json);


            }
        }
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }




    }
}
