public class InstantiateHangmanEvent
{
    public bool Instantiate { get; set; }
    public int Health { get; }

    public InstantiateHangmanEvent(bool instantiate, int health)
    {
        Instantiate = instantiate;
        Health = health;
    }
}