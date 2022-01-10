using UniRx;
public class ResultPopupViewModel
{
    public readonly ReactiveProperty<bool> Win;

    public ResultPopupViewModel()
    {
        Win = new ReactiveProperty<bool>();
    }
}