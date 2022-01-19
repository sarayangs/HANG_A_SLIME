public interface ISoundHandler
{
    void Play(string name);
    void ToggleAudio(bool toggle);
    void PlayMusic(string name);
}