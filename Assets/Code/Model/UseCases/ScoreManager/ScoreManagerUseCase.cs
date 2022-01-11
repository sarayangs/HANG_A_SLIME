public class ScoreManagerUseCase : IScoreManager
{
    private readonly IAccessUserData _userRepository;

    public ScoreManagerUseCase(IAccessUserData userRepository)
    {
        _userRepository = userRepository;
    }
    public void AddScore()
    {
        var user = _userRepository.GetLocalUser();
        user.CorrectWords++;
        user.Score += 100 * user.CorrectWords;
        
        _userRepository.SetLocalUser(user);
    }
}