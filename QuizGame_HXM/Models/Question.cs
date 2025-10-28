namespace QuizGame_HXM.Models
{
    public class Question
    {
        public string QuestionText { get; set; }
        public List<string> AnswerOptions { get; set; }
        public int CorrectAnswerIndex { get; set; }

        public Question(string questionText, List<string> answerOptions, int correctAnswerIndex)
        {
            QuestionText = questionText;
            AnswerOptions = answerOptions;
            CorrectAnswerIndex = correctAnswerIndex;
        }


    }
}
