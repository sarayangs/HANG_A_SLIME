public class ResponseData
{
    public string Letter;
    public string Hangman;
    public bool Correct;

    public ResponseData(string letter, string hangman, bool correct)
    {
        Letter = letter;
        Hangman = hangman;
        Correct = correct;
    }
}