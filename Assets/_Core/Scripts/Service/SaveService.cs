using System;
using UnityEngine;

namespace FloppaApp.Service
{
    public class SaveService
    {
        private const string ScoreKey = "Score";

        public void SaveScore(int score)
        {
            PlayerPrefs.SetInt(ScoreKey, score);
        }

        public int LoadScore() => PlayerPrefs.GetInt(ScoreKey);
    }
}