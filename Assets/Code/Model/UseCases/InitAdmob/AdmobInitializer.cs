public class AdmobInitializer : IAdmobInitializer
{
    private readonly IAdmobService _admobService;
    private readonly IAccessUserData _userRepository;

    public AdmobInitializer(IAdmobService admobService, IAccessUserData userRepository)
    {
        _admobService = admobService;
        _userRepository = userRepository;
    }
    public void Start()
    {
        _admobService.Init();
    }

    public void ShowAd()
    {
        var user = _userRepository.GetLocalUser();
        user.GotAnotherChance = false;
        _userRepository.SetLocalUser(user);
        
        _admobService.StartAd();
    }
}