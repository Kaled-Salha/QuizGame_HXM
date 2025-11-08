using QuizGame_HXM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace QuizGame_HXM.Pages
{
    /// <summary>
    /// Interaction logic for StartView.xaml
    /// </summary>
    public partial class StartView : UserControl
    {
        public StartView()
        {
            InitializeComponent();
        }

        private void PlayQuiz_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            var playQuizView = new PlayQuizView();
            var vm = new PlayQuizViewModel();
            playQuizView.DataContext = vm;
            mainWindow.MainContent.Content = playQuizView;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
