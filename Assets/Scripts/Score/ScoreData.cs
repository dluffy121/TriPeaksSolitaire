using System;
using UnityEngine;

namespace TriPeaks
{
    [Serializable]
    /// <summary>
    /// Stores Score related data
    /// </summary>
    public class ScoreData
    {
        [SerializeField] string _startTime;
        [SerializeField] float _score;
        [SerializeField] int _moves;
        [SerializeField] float _time;

        public string StartTime => _startTime;
        public float Score => _score;
        public int Moves => _moves;
        public float Time => _time;

        public ScoreData(string startTime, float score, int moves, float time)
        {
            _startTime = startTime;
            _score = score;
            _moves = moves;
            _time = time;
        }
    }
}
