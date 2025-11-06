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
        }

        private async void LoadQuiz_Click(object sender, RoutedEventArgs e)
        {
            var vm = new PlayQuizViewModel();
            await vm.LoadQuizAsync();
            if (vm.Quiz != null)
            {
                var view = new PlayQuizView();
                view.DataContext = vm;
                MainContent.Content = view;
            }
        }
        private async void SaveQuiz_Click(object sender, RoutedEventArgs e)
        {
            // placeholder or actual logic
        }

    }
}