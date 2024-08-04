using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TriPeaks
{
    /// <summary>
    /// UI class for showing Game stats in HUD
    /// </summary>
    public class UIGameHUD : MonoBehaviour
    {
        static UIGameHUD s_instance;

        #region Serialized Variables

        [SerializeField] Canvas _canvas;
        [SerializeField] GraphicRaycaster _graphicRaycaster;

        [SerializeField] TextMeshProUGUI _scoreText;
        [SerializeField] TextMeshProUGUI _movesText;
        [SerializeField] TextMeshProUGUI _timerText;

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

        public static void SetScore(string value)
            => s_instance.Internal_SetScore(value);

        public static void SetMoves(string value)
            => s_instance.Internal_SetMoves(value);

        public static void SetTimer(string value)
            => s_instance.Internal_SetTimer(value);

        #endregion

        private void Internal_SetScore(string value)
        {
            _scoreText.text = value;
        }

        private void Internal_SetMoves(string value)
        {
            _movesText.text = value;
        }

        private void Internal_SetTimer(string value)
        {
            _timerText.text = value;
        }
    }
}