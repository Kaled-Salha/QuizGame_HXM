using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuizGame_HXM.Models
{
    public class Quiz
    {
        public string Title { get; set; }
        public List<Question> Questions { get; set; }

        public Quiz(string title)
        {
            Title = title;
            Questions = new List<Question>();
        }

        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }

        public Question GetRandomQuestion()
        {
            Random rnd = new Random();
            int index = rnd.Next(0, Questions.Count);
            return Questions[index];
        }

        public Quiz FilterByCategories(List<string> selectedCategories)
        {
            var filteredQuiz = new Quiz(this.Title);
            filteredQuiz.Questions = this.Questions
                .Where(q => selectedCategories.Contains(q.Category))
                .ToList();

            return filteredQuiz;
        }

        public async Task SaveAsync(string format)
        {
            string json = JsonSerializer.Serialize(this);

            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string folder = Path.Combine(appData, "QuizGame_HXM");
            Directory.CreateDirectory(folder);

            string filePath = Path.Combine(folder, $"{Title}.json");
            await File.WriteAllTextAsync(filePath, json);
        }

        public static async Task<Quiz> LoadAsync(string filePath, string format)
        {
            string json = await File.ReadAllTextAsync(filePath);
            Quiz quiz = JsonSerializer.Deserialize<Quiz>(json);
            return quiz;
        }
    }
}
