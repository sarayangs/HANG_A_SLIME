using System;
using UnityEngine;

[Serializable]
public class ScoreEntry
{
    [SerializeField] public int Score;
    [SerializeField] public string Id;
    [SerializeField] public string Name;
    [SerializeField] public string Time;


    public ScoreEntry(string id, int score, string name)
    {
        Id = id;
        Score = score;
        Name = name;
        Time = "0";
    }
}