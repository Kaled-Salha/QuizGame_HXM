using QuizGame_HXM.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace QuizGame_HXM.Pages
{
    public partial class EditQuizView : UserControl
    {
        private Quiz _quiz;
        private string _originalFilePath;

        public EditQuizView(Quiz quiz, string filePath)
        {
            InitializeComponent();
            _quiz = quiz;
            _originalFilePath = filePath;

            QuestionList.ItemsSource = _quiz.Questions;
        }

        private void QuestionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (QuestionList.SelectedItem is Question q)
            {
                QuestionTextBox.Text = q.QuestionText;
                Answer1Box.Text = q.AnswerOptions.Count > 0 ? q.AnswerOptions[0] : "";
                Answer2Box.Text = q.AnswerOptions.Count > 1 ? q.AnswerOptions[1] : "";
                Answer3Box.Text = q.AnswerOptions.Count > 2 ? q.AnswerOptions[2] : "";
                CorrectIndexBox.Text = q.CorrectAnswerIndex.ToString();
                CategoryBox.Text = q.Category;
            }
        }

        private void ApplyChanges_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionList.SelectedItem is Question q)
            {
                q.QuestionText = QuestionTextBox.Text;
                q.AnswerOptions = new List<string>
                {
                    Answer1Box.Text,
                    Answer2Box.Text,
                    Answer3Box.Text
                };

                if (int.TryParse(CorrectIndexBox.Text, out int correctIndex) &&
                    correctIndex >= 0 && correctIndex <= 2)
                {
                    q.CorrectAnswerIndex = correctIndex;
                }
                else
                {
                    MessageBox.Show("Correct index must be 0, 1, or 2.");
                    return;
                }

                q.Category = CategoryBox.Text;

                QuestionList.Items.Refresh();
                MessageBox.Show("Question updated.");
            }
        }

        private async void SaveQuiz_Click(object sender, RoutedEventArgs e)
        {
            string json = JsonSerializer.Serialize(_quiz);
            await File.WriteAllTextAsync(_originalFilePath, json);
            MessageBox.Show("Quiz saved successfully.");
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContent.Content = new StartView();
            }
        }
    }
}
