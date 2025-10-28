using QuizGame_HXM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace QuizGame_HXM.Pages
{
    /// <summary>
    /// Interaction logic for PlayQuizView.xaml
    /// </summary>
    public partial class PlayQuizView : UserControl
    {
        public PlayQuizViewModel ViewModel { get; set; }

        public PlayQuizView()
        {
            InitializeComponent();
            ViewModel = new PlayQuizViewModel();
            DataContext = ViewModel;
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = int.Parse((sender as Button).Tag.ToString());
            ViewModel.NextQuestion(selectedIndex);
        }
    }

}
