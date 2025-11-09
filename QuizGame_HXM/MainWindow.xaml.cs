using QuizGame_HXM.Pages;
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

        public async void LoadQuiz_Click(object sender, RoutedEventArgs e)
        {
            var vm = new QuizGame_HXM.ViewModel.PlayQuizViewModel();
            await vm.LoadQuizAsync();

            if (vm.Quiz != null)
            {
                var view = new PlayQuizView();
                view.DataContext = vm;

                vm.BackToMenuRequested += (s, args) =>
                {
                    MainContent.Content = new StartView();
                };

                MainContent.Content = view;
            }
        }


        private async void SaveQuiz_Click(object sender, RoutedEventArgs e)
        {
            // placeholder or actual logic
        }

    }
}