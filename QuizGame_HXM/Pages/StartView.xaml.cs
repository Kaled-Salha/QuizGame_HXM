using System.Windows;
using System.Windows.Controls;

namespace QuizGame_HXM.Pages
{
    public partial class StartView : UserControl
    {
        public StartView()
        {
            InitializeComponent();
        }

        private async void PlayQuiz_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                await mainWindow.LoadQuizAsync();
            }
        }

        private void CreateQuiz_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContent.Content = new CreateQuizView();
            }
        }

        private async void EditQuiz_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                await mainWindow.LoadQuizForEditAsync();
            }
        }
        private void Credits_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContent.Content = new CreditsView();
            }
        }


        private async void Exit_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                var creditsView = new CreditsView(showBackButton: false);
                mainWindow.MainContent.Content = creditsView;

                await Task.Delay(5000); // Wait 5 seconds before closing
                Application.Current.Shutdown();
            }
        }

    }
}
