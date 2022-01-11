public class Answer
{
    public bool Correct;
    public int Score;
    public int Time;

    public Answer(bool isCorrect, int score, int time)
    {
        Correct = isCorrect;
        Score = score;
        Time = time;
    }
}