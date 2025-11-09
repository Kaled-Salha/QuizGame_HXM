namespace QuizGame_HXM.Models
{
    public class Question
    {
        public string QuestionText { get; set; }
        public List<string> AnswerOptions { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public string Category { get; set; }

        public Question() { }

        public Question(string questionText, List<string> answerOptions, int correctAnswerIndex, string category)
        {
            QuestionText = questionText;
            AnswerOptions = answerOptions;
            CorrectAnswerIndex = correctAnswerIndex;
            Category = category;
        }


    }
}
