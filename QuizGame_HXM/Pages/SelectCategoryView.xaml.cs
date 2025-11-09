using QuizGame_HXM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuizGame_HXM.Pages
{
    /// <summary>
    /// Interaction logic for SelectCategoryView.xaml
    /// </summary>
    public partial class SelectCategoryView : UserControl
    {
        private Quiz _originalQuiz;

        public event EventHandler<List<string>> CategoriesConfirmed;

        public SelectCategoryView(Quiz quiz)
        {
            InitializeComponent();
            _originalQuiz = quiz;

            var uniqueCategories = quiz.Questions
                .Select(q => q.Category)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct()
                .ToList();

            CategoryList.ItemsSource = uniqueCategories;
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            var selected = CategoryList.SelectedItems
                .OfType<string>()
                .ToList();

            if (selected.Count == 0)
            {
                MessageBox.Show("Please select at least one category.", "No Selection");
                return;
            }

            CategoriesConfirmed?.Invoke(this, selected);
        }

    }
}
