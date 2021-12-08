using System;
using UnityEngine;

[Serializable]
public class ScoreEntry
{
        [SerializeField] public int Score;
        [SerializeField] public string Name;
        [SerializeField] public int Time;
        

        public ScoreEntry(int score, string name)
        {
                Score = score;
                Name = name;
                Time = 0;
        }
}