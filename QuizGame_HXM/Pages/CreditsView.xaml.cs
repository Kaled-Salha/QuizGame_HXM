using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace QuizGame_HXM.Pages
{
    public partial class CreditsView : UserControl
    {
        public CreditsView(bool showBackButton = true)
        {
            InitializeComponent();
            BackButton.Visibility = showBackButton ? Visibility.Visible : Visibility.Collapsed;

            // Auto-close if back button is hidden
            if (!showBackButton)
            {
                var timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(5)
                };
                timer.Tick += (s, e) =>
                {
                    timer.Stop();
                    ReturnToMenu();
                };
                timer.Start();
            }
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            ReturnToMenu();
        }

        private void ReturnToMenu()
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContent.Content = new StartView();
            }
        }
    }
}
