using QuizGame_HXM.Models;
using QuizGame_HXM.Pages;
using QuizGame_HXM.ViewModel;
using System.Text.Json;
using System.Windows;
using System.IO;

namespace QuizGame_HXM
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = new StartView();
        }

        public async Task LoadQuizAsync()
        {
            var vm = new PlayQuizViewModel();
            await vm.LoadQuizAsync();

            if (vm.Quiz != null)
            {
                var categorySelectionView = new SelectCategoryView(vm.Quiz);

                categorySelectionView.CategoriesConfirmed += (s, selectedCategories) =>
                {
                    var filteredQuiz = vm.Quiz.FilterByCategories(selectedCategories);
                    var filteredVM = new PlayQuizViewModel { Quiz = filteredQuiz };

                    var playView = new PlayQuizView { DataContext = filteredVM };

                    // Load question after view is ready
                    playView.Loaded += (sender, e) =>
                    {
                        filteredVM.LoadNextQuestion();
                    };

                    filteredVM.BackToMenuRequested += (_, _) =>
                    {
                        MainContent.Content = new StartView();
                    };

                    MainContent.Content = playView;
                };

                MainContent.Content = categorySelectionView;
            }
        }

        public async Task LoadQuizForEditAsync()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json"
            };

            if (dialog.ShowDialog() == true)
            {
                string filePath = dialog.FileName;
                string json = await File.ReadAllTextAsync(filePath);
                var quiz = JsonSerializer.Deserialize<Quiz>(json);

                if (quiz != null)
                {
                    var editView = new EditQuizView(quiz, filePath);
                    MainContent.Content = editView;
                }
            }
        }
    }
}
