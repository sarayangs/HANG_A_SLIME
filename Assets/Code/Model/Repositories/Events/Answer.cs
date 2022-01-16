public class Answer
{
    public bool Correct;
    public int Score;
    public string Time;

    public Answer(bool isCorrect, int score, string time)
    {
        Correct = isCorrect;
        Score = score;
        Time = time;
    }
}