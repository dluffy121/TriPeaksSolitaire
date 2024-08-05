using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace TriPeaks
{
    [Serializable]
    /// <summary>
    /// Represents a Leader Board data, which is also saved
    /// </summary>
    public class LeaderBoardData
    {
        [SerializeField] List<ScoreData> _lstScoreData;
        [SerializeField] ScoreData _highScoreData;

        public List<ScoreData> LstScoreData => _lstScoreData;
        public ScoreData HighScoreDat => _highScoreData;

        public LeaderBoardData(ScoreData scoreData)
        {
            _lstScoreData = new() { scoreData };
        }

        public void AddScoreData(ScoreData scoreData)
        {
            if (_lstScoreData == null)
                _lstScoreData = new() { scoreData };
            else
                _lstScoreData.Add(scoreData);

            _lstScoreData = _lstScoreData.OrderBy(x => x.Moves).ToList();

            _highScoreData = _lstScoreData[0];
        }
    }
}