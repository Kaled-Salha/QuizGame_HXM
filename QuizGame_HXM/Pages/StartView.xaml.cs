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
            mainWindow?.LoadQuiz_Click(sender, e); // call the real handler
        }




        private void CreateQuiz_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            var view = new CreateQuizView();
            mainWindow.MainContent.Content = view;
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
