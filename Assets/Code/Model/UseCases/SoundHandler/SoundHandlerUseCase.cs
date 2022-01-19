public class SoundHandlerUseCase : ISoundHandler
{
    public void Play(string name)
    {
        SoundManager.Instance.PlaySound(name);
    }

    public void ToggleAudio(bool toggle)
    {
        SoundManager.Instance.Toggle(toggle);
    }

    public void PlayMusic(string name)
    {
        SoundManager.Instance.PlayMusic(name);
    }
}