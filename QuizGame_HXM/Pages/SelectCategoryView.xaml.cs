using QuizGame_HXM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QuizGame_HXM.Pages
{
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

            
            uniqueCategories.Insert(0, "All Categories");

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

            // If "All Categories" is selected, include all valid categories
            if (selected.Contains("All Categories"))
            {
                selected = _originalQuiz.Questions
                    .Select(q => q.Category)
                    .Where(c => !string.IsNullOrWhiteSpace(c))
                    .Distinct()
                    .ToList();
            }

            CategoriesConfirmed?.Invoke(this, selected);
        }
    }
}
