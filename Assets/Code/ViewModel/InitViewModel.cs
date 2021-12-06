using UniRx;
public class InitViewModel
{
    public readonly ReactiveProperty<bool> ChangeScene;

    public InitViewModel()
    {
        ChangeScene = new ReactiveProperty<bool>(false);
    }
}