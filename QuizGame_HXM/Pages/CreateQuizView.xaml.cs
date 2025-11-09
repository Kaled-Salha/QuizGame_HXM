using QuizGame_HXM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace QuizGame_HXM.Pages
{
    public partial class CreateQuizView : UserControl
    {
        private CreateQuizViewModel vm;

        public CreateQuizView()
        {
            InitializeComponent();
            vm = new CreateQuizViewModel();
            this.DataContext = vm;
        }

        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            vm.AddQuestion();
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
    }
}
