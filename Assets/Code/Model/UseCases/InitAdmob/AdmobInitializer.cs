public class AdmobInitializer : IAdmobInitializer
{
    private readonly IAdmobService _admobService;
    private readonly IAccessUserData _userRepository;
    private readonly IAnalyticsService _firebaseAnalyticsService;

    public AdmobInitializer(IAdmobService admobService, IAccessUserData userRepository, IAnalyticsService firebaseAnalyticsService)
    {
        _admobService = admobService;
        _userRepository = userRepository;
        _firebaseAnalyticsService = firebaseAnalyticsService;
    }
    public void Start()
    {
        _admobService.Init();
    }

    public void ShowAd()
    {
        _firebaseAnalyticsService.ShowAdEvent();
        
        var user = _userRepository.GetLocalUser();
        user.GotAnotherChance = false;
        _userRepository.SetLocalUser(user);
        
        _admobService.StartAd();
    }
}