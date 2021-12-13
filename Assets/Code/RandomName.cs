using System;

public class RandomName
{
    public string Name { get;}
    private string[] randomNames;

    public RandomName()
    {
        randomNames = new string[]
        {
            "Tiger", "Dog", "Pig", "Ox", "Dragon", "Rabbit", "Monkey", "Rooster", "Rat",
            "Snake", "Horse", "Goat"
        };

        var rand = new Random();
        var index = rand.Next(0, 12);

        Name = randomNames[index];
    }
}
