using System;
using UnityEngine;
using UnityEngine.UI;

namespace TriPeaks
{
    /// <summary>
    /// Helps with managing core Game flow
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager s_instance;

        [SerializeField] float _startDelay;
        [SerializeField] Image _blockingImage;

        #region Member Variables

        private bool _isGameRunning = false;
        private string _gameStartTime;
        private float _timer = 0;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            s_instance = this;
        }

        private void OnDestroy()
        {
            s_instance = null;
        }

        private void Start()
        {
            Invoke(nameof(PrepareGame), _startDelay);
        }

        private void Update()
        {
            if (!_isGameRunning) return;

            TimeSpan timeSpan = TimeSpan.FromSeconds(_timer);
            UIGameHUD.SetTimer(string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds));
            _timer += Time.deltaTime;
        }

        #endregion

        #region Static Members

        public static void StartGame()
        {
            s_instance._gameStartTime = DateTime.Now.ToString();
            s_instance._isGameRunning = true;
            s_instance._blockingImage.enabled = false;
        }

        public static void EndGame()
        {
            s_instance._isGameRunning = false;
            ScoreData newData = new(s_instance._gameStartTime, ScoreManager.TotalScore, ScoreManager.TotalMoves, s_instance._timer);
            UIGameEnd.Show(newData);
        }

        #endregion

        #region Private Methods

        private void PrepareGame()
        {
            StockPile.Initialize();
            TriPeaksManager.Initialize();
            ScoreManager.Initialize();
        }

        #endregion
    }
}