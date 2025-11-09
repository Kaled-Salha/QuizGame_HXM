using System.IO;
using System.Text.Json;

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
            int randomIndex = rnd.Next(0, Questions.Count);



            return Questions[randomIndex];
        }

        public Quiz FilterByCategories(List<string> selectedCategories)
        {
            var filtered = new Quiz(this.Title);
            filtered.Questions = this.Questions
                .Where(q => selectedCategories.Contains(q.Category))
                .ToList();
            return filtered;
        }

        public async Task SaveAsync(string format)
        {
            string json = JsonSerializer.Serialize(this);
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string appFolder = Path.Combine(folder, "QuizGame_HXM");
            Directory.CreateDirectory(appFolder);
            string filePath = Path.Combine(appFolder, $"{Title}.json");

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
