using System.IO;
using UnityEngine;

namespace TriPeaks
{
    /// <summary>
    /// Helps with Loading and Saving of <see cref="LeaderBoardData"/>
    /// </summary>
    public class LeaderBoardManager : MonoBehaviour
    {
        static LeaderBoardManager s_instance;

        #region Serialized Variables

        [SerializeField] private string _filePath;

        #endregion

        #region Member Variables

        LeaderBoardData _leaderBoardData;
        string _fullSavePath;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            s_instance = this;
            _fullSavePath = Path.Combine(Application.persistentDataPath, _filePath);
        }

        private void OnDestroy()
        {
            s_instance = null;
        }

        #endregion

        #region Static Members

        /// <summary>
        /// Provides access to the leader-board data
        /// </summary>
        public static LeaderBoardData LeaderBoardData => s_instance._leaderBoardData;

        /// <summary>
        /// Loads Leader Board data
        /// </summary>
        public static void LoadLeaderBoard()
            => s_instance.Internal_LoadLeaderBoard();

        /// <summary>
        /// Saves a <see cref="ScoreData"/> to Leader Board Data
        /// </summary>
        /// <param name="scoreData"></param>
        public static void SaveToLeaderBoard(ScoreData scoreData)
             => s_instance.Internal_SaveToLeaderBoard(scoreData);

        #endregion

        private void Internal_LoadLeaderBoard()
        {
            if (File.Exists(_fullSavePath))
            {
                // Load the JSON from the file
                string jsonData = File.ReadAllText(_fullSavePath);

                // Deserialize the JSON to a ScoreData object
                _leaderBoardData = JsonUtility.FromJson<LeaderBoardData>(jsonData);
            }
            else
                Debug.LogWarning("File not found: " + _fullSavePath);
        }

        private void Internal_SaveToLeaderBoard(ScoreData scoreData)
        {
            if (_leaderBoardData == null)
                _leaderBoardData = new(scoreData);
            else
                _leaderBoardData.AddScoreData(scoreData);

            string jsonData = JsonUtility.ToJson(_leaderBoardData, true);

            File.WriteAllText(_fullSavePath, jsonData);

            Debug.Log("Data saved to " + _fullSavePath);
        }
    }
}