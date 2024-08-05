using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TriPeaks
{
    /// <summary>
    /// UI class that represents Game End Screen
    /// </summary>
    public class UIGameEnd : MonoBehaviour
    {
        static UIGameEnd s_instance;

        #region Serialized Variables

        [SerializeField] Canvas _canvas;
        [SerializeField] GraphicRaycaster _graphicRaycaster;
        [SerializeField] CanvasGroup _canvasGroup;

        [SerializeField, TextArea] string _leaderBoardEntryStrFormat;
        [SerializeField] TextMeshProUGUI _leaderBoardEntryPrefab;
        [SerializeField] Transform _leaderBoardEntryParent;

        [Header("Animation")]
        [SerializeField] float _fadeDuration = 1f;

        #endregion

        #region Member Variables

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

        #endregion

        #region Static Members

        /// <summary>
        /// Shows this UI
        /// </summary>
        /// <param name="newData">Current Games Score Data</param>
        public static void Show(ScoreData newData)
        {
            s_instance.Internal_Show(newData);
        }

        #endregion

        #region Interactions

        /// <summary>
        /// Called when Play Again button is clicked
        /// </summary>
        public void OnBtnClicked_PlayAgain()
        {
            StartCoroutine(IEFadeUI(0, GameManager.RestartGame));
        }

        #endregion

        #region Private Methods

        private void Internal_Show(ScoreData newData)
        {
            _canvas.enabled = true;
            _graphicRaycaster.enabled = true;

            LeaderBoardManager.LoadLeaderBoard();
            LeaderBoardManager.SaveToLeaderBoard(newData);

            StartCoroutine(IEFadeUI(1, PopulateLeaderBoardEntries));
        }

        private IEnumerator IEFadeUI(float target, Action onComplete = null)
        {
            float timer = 0;

            while (timer < _fadeDuration)
            {
                _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, target, timer / _fadeDuration);

                timer += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            _canvasGroup.alpha = target;

            onComplete?.Invoke();
        }

        private void PopulateLeaderBoardEntries()
        {
            for (int i = 0; i < LeaderBoardManager.LeaderBoardData.LstScoreData.Count; i++)
            {
                ScoreData data = LeaderBoardManager.LeaderBoardData.LstScoreData[i];
                TextMeshProUGUI entry = Instantiate(_leaderBoardEntryPrefab, _leaderBoardEntryParent);

                TimeSpan timeSpan = TimeSpan.FromSeconds(data.Time);

                entry.text = string.Format(
                    _leaderBoardEntryStrFormat,
                    i + 1,
                    data.StartTime.ToString(),
                    data.Score,
                    data.Moves,
                    string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds));
            }
        }

        #endregion
    }
}