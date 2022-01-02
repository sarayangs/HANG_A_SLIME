public class TokenRepository
{
    private string _token;

    public TokenRepository()
    {
        _token = null;
    }
    
    public string GetToken()
    {
        return _token;
    }

    public void SetToken(string token)
    {
        _token = token;
    }
}