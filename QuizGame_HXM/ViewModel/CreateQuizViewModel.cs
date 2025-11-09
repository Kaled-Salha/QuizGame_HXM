using Microsoft.Win32;
using QuizGame_HXM.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace QuizGame_HXM.ViewModel
{
    public class CreateQuizViewModel : INotifyPropertyChanged
    {
        private string _quizTitle;
        public string QuizTitle
        {
            get => _quizTitle;
            set { _quizTitle = value; OnPropertyChanged(); }
        }

        public string QuestionText { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public int CorrectAnswerIndex { get; set; }

        public ObservableCollection<Question> Questions { get; set; } = new ObservableCollection<Question>();

        public void AddQuestion()
        {
            var question = new Question(this.QuestionText,
                new List<string> { Answer1, Answer2, Answer3 },
                this.CorrectAnswerIndex, "");


            Questions.Add(question);

            // Reset fields
            QuestionText = string.Empty;
            Answer1 = string.Empty;
            Answer2 = string.Empty;
            Answer3 = string.Empty;
            CorrectAnswerIndex = 0;

            OnPropertyChanged(nameof(QuestionText));
            OnPropertyChanged(nameof(Answer1));
            OnPropertyChanged(nameof(Answer2));
            OnPropertyChanged(nameof(Answer3));
            OnPropertyChanged(nameof(CorrectAnswerIndex));
        }

        public async Task SaveQuizAsync()
        {
            var quiz = new Quiz(this.QuizTitle);
            quiz.Questions = Questions.ToList();

            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json"
            };

            if (dialog.ShowDialog() == true)
            {
                string json = JsonSerializer.Serialize(quiz);
                await File.WriteAllTextAsync(dialog.FileName, json);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
