using UnityEngine;

namespace TriPeaks
{
    /// <summary>
    /// Helps with managing score of currently dealt board
    /// </summary>
    public class ScoreManager : MonoBehaviour
    {
        static ScoreManager s_instance;

        #region Serialized Variables

        [SerializeField] private float _scoreOnCollect;
        [SerializeField] private string _saveFilePath;

        #endregion

        #region Member Variables

        float _totalScore;
        int _totalMoves;

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
        /// Total score of current board
        /// </summary>
        public static float TotalScore => s_instance._totalScore;

        /// <summary>
        /// Total Moves performed on current board
        /// </summary>
        public static int TotalMoves => s_instance._totalMoves;

        public static void Initialize()
            => s_instance.Internal_Initialize();

        public static void DeInitialize()
            => s_instance.Internal_DeInitialize();

        #endregion

        private void Internal_Initialize()
        {
            TriPeaksManager.OnCardCollected += AddMove;
            TriPeaksManager.OnCardCollected += AddScore;
            StockPile.OnDrawnFromDeck += AddMove;
            StockPile.OnBuyNewDeck += DeductScore;
        }

        private void Internal_DeInitialize()
        {
            TriPeaksManager.OnCardCollected -= AddScore;
            TriPeaksManager.OnCardCollected -= AddScore;
            StockPile.OnDrawnFromDeck -= AddMove;
            StockPile.OnBuyNewDeck -= DeductScore;
        }

        private void AddScore()
        {
            _totalScore += _scoreOnCollect;
            UIGameHUD.SetScore(_totalScore.ToString());
        }

        private void AddMove()
        {
            _totalMoves++;
            UIGameHUD.SetMoves(_totalMoves.ToString());
        }

        private void DeductScore(float score)
        {
            _totalScore -= score;
        }
    }
}