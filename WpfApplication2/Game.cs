using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication2
{
    public class Game
    {
        public int TimeLeft;
        public int LivesCount;
        public int Score { get; private set; }
        public const int Lives = 3, NoOfBlock = 100;
        public static readonly List<string> wordList = new List<string>(100) { "its", "earth", "be", "will", "oil", "walk", "talk", "different", "upon", "word", "play", "food", "only", "of", "air", "even", "night", "own", "family", "children", "read", "took", "school", "here", "change", "very", "might", "there", "such", "study", "sea", "important", "me", "fall", "family", "world", "example", "young", "two", "seem", "eat", "side", "any", "man", "feet", "be", "really", "plant", "few", "really", "such", "did", "paper", "more", "white", "sun", "animal", "house", "here", "do", "than", "sun", "being", "found", "sound", "being", "long", "can", "let", "turn", "second", "apple", "give", "place", "great", "him", "through", "place", "miss", "then", "now", "right", "set", "not", "food", "place", "another", "now", "leave", "with", "extreme", "progressively", "continuous", "until", "appropriate", "opportunities", "supervisor", "community", "exceptional", "compliance" };

        public Game(int score)
        {
            TimeLeft = 30;
            Score = score;
            LivesCount = Lives;
        }

        public int GetScore(int addPoint)
        {
            Score += 200 - addPoint;
            return Score;
        }

        public bool CheckDie(double boxHeight, int DieHeight)
        {
            return (boxHeight >= DieHeight);
        }
    }
}
