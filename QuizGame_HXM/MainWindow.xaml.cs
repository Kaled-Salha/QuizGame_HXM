using QuizGame_HXM.Pages;
using QuizGame_HXM.ViewModel;
using System.Windows;


namespace QuizGame_HXM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var view = new StartView();
            MainContent.Content = view;
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
                    var filteredViewModel = new PlayQuizViewModel
                    {
                        Quiz = filteredQuiz
                    };
                    filteredViewModel.LoadNextQuestion();

                    var view = new PlayQuizView();
                    view.DataContext = filteredViewModel;

                    filteredViewModel.BackToMenuRequested += (s2, args) =>
                    {
                        MainContent.Content = new StartView();
                    };

                    MainContent.Content = view;
                };

                MainContent.Content = categorySelectionView;
            }
        }




        private async void SaveQuiz_Click(object sender, RoutedEventArgs e)
        {
            // placeholder or actual logic
        }

    }
}