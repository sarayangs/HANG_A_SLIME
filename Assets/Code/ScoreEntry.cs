using System;
using UnityEngine;

[Serializable]
public class ScoreEntry
{
        [SerializeField] public int Score;

        public ScoreEntry(int score)
        {
                Score = score;
        }
}