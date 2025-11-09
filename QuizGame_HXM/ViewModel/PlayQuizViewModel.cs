using Microsoft.Win32;
using QuizGame_HXM.Models;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;
using System.Windows.Media;


namespace QuizGame_HXM.ViewModel
{
    public class PlayQuizViewModel : INotifyPropertyChanged
    {
        public Quiz Quiz { get; set; }
        public Question CurrentQuestion { get; set; }
        public int TotalCorrect { get; set; }
        public int TotalAnswered { get; set; }

        private Brush _feedbackColor = Brushes.Black;
        public Brush FeedbackColor
        {
            get => _feedbackColor;
            set { _feedbackColor = value; OnPropertyChanged(); }
        }


        public event EventHandler BackToMenuRequested;

        private void OnBackToMenuRequested() => BackToMenuRequested?.Invoke(this, EventArgs.Empty);

        public void RequestBackToMenu() => OnBackToMenuRequested();

        private string _scoreText;
        public string ScoreText
        {
            get => _scoreText;
            set { _scoreText = value; OnPropertyChanged(); }
        }

        private string _answer1;
        public string Answer1
        {
            get => _answer1;
            set { _answer1 = value; OnPropertyChanged(); }
        }

        private string _answer2;
        public string Answer2
        {
            get => _answer2;
            set { _answer2 = value; OnPropertyChanged(); }
        }

        private string _answer3;
        public string Answer3
        {
            get => _answer3;
            set { _answer3 = value; OnPropertyChanged(); }
        }

        private bool _isAnswerSelected;
        public bool IsAnswerSelected
        {
            get => _isAnswerSelected;
            set { _isAnswerSelected = value; OnPropertyChanged(); }
        }

        private string _feedbackMessage;
        public string FeedbackMessage
        {
            get => _feedbackMessage;
            set { _feedbackMessage = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


        //    CORE METHODS       //

        private void UpdateAnswerOptions()
        {
            if (CurrentQuestion?.AnswerOptions != null)
            {
                Answer1 = CurrentQuestion.AnswerOptions.Count > 0 ? CurrentQuestion.AnswerOptions[0] : "";
                Answer2 = CurrentQuestion.AnswerOptions.Count > 1 ? CurrentQuestion.AnswerOptions[1] : "";
                Answer3 = CurrentQuestion.AnswerOptions.Count > 2 ? CurrentQuestion.AnswerOptions[2] : "";
            }
            else
            {
                Answer1 = Answer2 = Answer3 = "";
            }
        }

        public void SelectAnswer(int selectedIndex)
        {
            if (CurrentQuestion == null || IsAnswerSelected)
                return;

            TotalAnswered++;
            bool correct = selectedIndex == CurrentQuestion.CorrectAnswerIndex;

            FeedbackMessage = correct
                ? "✅ Correct!"
                : $"❌ Wrong! Correct: {CurrentQuestion.AnswerOptions[CurrentQuestion.CorrectAnswerIndex]}";

            FeedbackColor = correct ? Brushes.Green : Brushes.Red;

            if (correct)
                TotalCorrect++;

            int percent = (int)((double)TotalCorrect / TotalAnswered * 100);
            ScoreText = $"Correct: {TotalCorrect} / {TotalAnswered} ({percent}%)";

            IsAnswerSelected = true;
        }

        public void LoadNextQuestion()
        {
            if (Quiz == null || Quiz.Questions.Count == 0)
                return;

            CurrentQuestion = Quiz.GetRandomQuestion();
            UpdateAnswerOptions();

            FeedbackMessage = string.Empty;
            FeedbackColor = Brushes.Black;
            IsAnswerSelected = false;

            OnPropertyChanged(nameof(CurrentQuestion));
        }


        //    QUIZ FILE HANDLING //

        public async Task LoadQuizAsync()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json"
            };

            if (dialog.ShowDialog() == true)
            {
                string json = await File.ReadAllTextAsync(dialog.FileName);
                Quiz = JsonSerializer.Deserialize<Quiz>(json);

                if (Quiz != null)
                {
                    LoadNextQuestion();
                    OnPropertyChanged(nameof(Quiz));
                }
            }
        }

        public async Task SaveQuizAsync()
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json"
            };

            if (saveDialog.ShowDialog() == true)
            {
                string json = JsonSerializer.Serialize(this.Quiz);
                await File.WriteAllTextAsync(saveDialog.FileName, json);
            }
        }


        //        COMMANDS       //

        public ICommand AnswerCommand => new RelayCommand(param =>
        {
            int index = Convert.ToInt32(param);
            SelectAnswer(index);
        });

        public ICommand NextCommand => new RelayCommand(_ => LoadNextQuestion());
        public ICommand BackCommand => new RelayCommand(_ => RequestBackToMenu());
    }
}
