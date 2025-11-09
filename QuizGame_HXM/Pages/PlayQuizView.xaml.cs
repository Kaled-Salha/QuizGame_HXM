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


        public PlayQuizView()
        {
            InitializeComponent();

        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PlayQuizViewModel vm)
            {
                int selectedIndex = int.Parse((sender as Button).Tag.ToString());
                vm.NextQuestion(selectedIndex);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PlayQuizViewModel vm)
            {
                vm.RequestBackToMenu();
            }

        }
    }

}
