using UniRx;
public class InitViewModel
{
    public readonly ReactiveProperty<bool> HasToLogNewUser;
    public readonly ReactiveProperty<bool> UserExists;
    public readonly ReactiveCommand LoginNewUser;
    public readonly ReactiveCommand LoginExistingUser;
    
    public InitViewModel()
    {
        HasToLogNewUser = new ReactiveProperty<bool>();
        UserExists = new ReactiveProperty<bool>();
        
        LoginNewUser = new ReactiveCommand();
        LoginExistingUser = new ReactiveCommand();
    }
}