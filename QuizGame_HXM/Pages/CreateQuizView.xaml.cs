using QuizGame_HXM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace QuizGame_HXM.Pages
{
    public partial class CreateQuizView : UserControl
    {
        public CreateQuizView()
        {
            InitializeComponent();
            DataContext = new CreateQuizViewModel();
        }

        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CreateQuizViewModel vm)
            {
                vm.AddQuestion();
            }
        }
        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContent.Content = new StartView();
            }
        }


        private async void SaveQuiz_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CreateQuizViewModel vm)
            {
                await vm.SaveQuizAsync();

                var mainWindow = Window.GetWindow(this) as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.MainContent.Content = new StartView();
                }
            }
        }
        private void BrowseImage_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CreateQuizViewModel vm)
            {
                vm.BrowseImage();
            }
        }

    }
}
