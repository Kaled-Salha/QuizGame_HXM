using QuizGame_HXM.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuizGame_HXM.ViewModel
{
    public class PlayQuizViewModel : INotifyPropertyChanged
    {
        public Quiz Quiz { get; set; }
        public Question CurrentQuestion { get; set; }
        public int CurrentQuestionIndex { get; set; }
        public int TotalCorrect { get; set; }
        public int TotalAnswered { get; set; }

        public PlayQuizViewModel()
        {
            Quiz = new Quiz("TestQuiz");
            Quiz.AddQuestion(new Question("What is the capital of Sweden?",
                new List<string> { "Stockholm", "Gothenburg", "Malmö" }, 0));
            Quiz.AddQuestion(new Question("What color is the sky?",
                new List<string> { "Red", "Green", "Blue" }, 2));

            CurrentQuestion = Quiz.GetRandomQuestion();
            OnPropertyChanged(nameof(CurrentQuestion));
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
        { }
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
