using Microsoft.Win32;
using QuizGame_HXM.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows;

namespace QuizGame_HXM.ViewModel
{
    public class CreateQuizViewModel : INotifyPropertyChanged
    {

        //        FIELDS         //

        private string _quizTitle;
        public string QuizTitle
        {
            get => _quizTitle;
            set { _quizTitle = value; OnPropertyChanged(); }
        }

        private string _questionText;
        public string QuestionText
        {
            get => _questionText;
            set { _questionText = value; OnPropertyChanged(); }
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

        private int _correctAnswerIndex;
        public int CorrectAnswerIndex
        {
            get => _correctAnswerIndex;
            set { _correctAnswerIndex = value; OnPropertyChanged(); }
        }

        private string _category;
        public string Category
        {
            get => _category;
            set { _category = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Question> Questions { get; set; } = new ObservableCollection<Question>();


        //       METHODS         //

        public void AddQuestion()
        {
            // === Validation ===
            if (string.IsNullOrWhiteSpace(QuestionText) ||
                string.IsNullOrWhiteSpace(Answer1) ||
                string.IsNullOrWhiteSpace(Answer2) ||
                string.IsNullOrWhiteSpace(Answer3))
            {
                MessageBox.Show("Please fill in all fields before adding a question.", "Validation Error");
                return;
            }

            if (CorrectAnswerIndex < 0 || CorrectAnswerIndex > 2)
            {
                MessageBox.Show("Correct answer index must be 0, 1, or 2.", "Validation Error");
                return;
            }

            // === Create question ===
            var question = new Question(
                this.QuestionText,
                new List<string> { Answer1, Answer2, Answer3 },
                this.CorrectAnswerIndex,
                this.Category
            );

            Questions.Add(question);

            // === Reset fields ===
            QuestionText = string.Empty;
            Answer1 = string.Empty;
            Answer2 = string.Empty;
            Answer3 = string.Empty;
            CorrectAnswerIndex = 0;
            Category = string.Empty;

            // Notify UI
            OnPropertyChanged(nameof(QuestionText));
            OnPropertyChanged(nameof(Answer1));
            OnPropertyChanged(nameof(Answer2));
            OnPropertyChanged(nameof(Answer3));
            OnPropertyChanged(nameof(CorrectAnswerIndex));
            OnPropertyChanged(nameof(Category));
        }


        public async Task SaveQuizAsync()
        {
            // === Validation ===
            if (string.IsNullOrWhiteSpace(QuizTitle))
            {
                MessageBox.Show("Please enter a quiz title before saving.", "Validation Error");
                return;
            }

            if (Questions.Count == 0)
            {
                MessageBox.Show("You must add at least one question before saving.", "Validation Error");
                return;
            }

            // === Create quiz ===
            var quiz = new Quiz(this.QuizTitle)
            {
                Questions = Questions.ToList()
            };

            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                FileName = QuizTitle + ".json"
            };

            if (dialog.ShowDialog() == true)
            {
                string json = JsonSerializer.Serialize(quiz);
                await File.WriteAllTextAsync(dialog.FileName, json);
                MessageBox.Show("Quiz saved successfully.", "Success");

                // reset fields
                QuizTitle = string.Empty;
                Questions.Clear();

                OnPropertyChanged(nameof(QuizTitle));
                OnPropertyChanged(nameof(Questions));
            }
        }



        //  INotifyPropertyChanged


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
