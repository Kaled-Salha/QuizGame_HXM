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
            TotalAnswered++;

            if (selectedIndex == CurrentQuestion.CorrectAnswerIndex)
            {
                TotalCorrect++;

            }
            int percent = (int)((double)TotalCorrect / TotalAnswered * 100);
            ScoreText = $"Rätt: {TotalCorrect} / {TotalAnswered} ({percent}%)";

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
                Quiz quiz = JsonSerializer.Deserialize<Quiz>(json);
                Quiz = quiz;
                CurrentQuestion = Quiz.GetRandomQuestion();
                OnPropertyChanged(nameof(CurrentQuestion));
                OnPropertyChanged(nameof(Quiz));
            }


        }
        public async Task SaveQuizAsync()
        { }
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }




    }
}
