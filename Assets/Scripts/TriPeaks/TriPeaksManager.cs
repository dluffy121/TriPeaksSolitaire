using System;
using System.Collections;
using UnityEngine;

namespace TriPeaks
{
    /// <summary>
    /// Helps with populating as well as managing 
    /// </summary>
    public class TriPeaksManager : MonoBehaviour
    {
        static TriPeaksManager s_instance;

        [SerializeField] TriPeaksNode[] _triPeakNodes;
        [SerializeField] int _faceUpCards = 10;
        [SerializeField] int _winCollectCount = 28;

        [Header("Animation")]
        [SerializeField] float _cardPlaceDuration = 0.01f;
        [SerializeField] float _cardPlaceDurationIncrement = 0.01f;

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
        /// Event raised when Card is collected from TriPeaks
        /// </summary>
        public static event Action OnCardCollected;

        public static void Initialize()
            => s_instance.Internal_Initialize();

        public static bool TryCollectCard(Card card)
            => s_instance.Internal_CollectCard(card);

        #endregion

        #region Private Methods

        private void Internal_Initialize()
        {
            PlaceCards();
            UnfoldInitialCards();
        }

        private bool Internal_CollectCard(Card card)
        {
            if (!CanCollectCard(card)) return false;

            SoundPlayer.PlayCardPlaceSFX();
            WastePile.Add(card);

            _winCollectCount--;

            OnCardCollected?.Invoke();

            if (_winCollectCount == 0)
                GameManager.EndGame();

            return true;
        }

        private void PlaceCards()
        {
            float initialPlaceDuration = 0;

            foreach (TriPeaksNode node in _triPeakNodes)
            {
                if (!StockPile.TryDrawCard(out Card card))
                    return;

                node.PlaceCard(card, _cardPlaceDuration, initialPlaceDuration);
                initialPlaceDuration += _cardPlaceDurationIncrement;
            }

            StartCoroutine(IEWaitToEnableInteraction(initialPlaceDuration + _cardPlaceDuration));
        }

        private IEnumerator IEWaitToEnableInteraction(float waitDuration)
        {
            yield return new WaitForSeconds(waitDuration);

            GameManager.StartGame();
            StockPile.UpdateVisuals();
        }

        private void UnfoldInitialCards()
        {
            for (int i_node = 1; i_node <= _faceUpCards; i_node++)
                _triPeakNodes[^i_node].TryUnfoldCard();
        }

        private void DealFoundationCard()
        {
            if (StockPile.TryDrawCard(out Card card))
                WastePile.Add(card);
        }

        private bool CanCollectCard(Card card)
        {
            byte topCardId = WastePile.TopCardId;

            if (topCardId == byte.MaxValue) // if not card on top of Waste Pile
                return true;

            int idAbsDiff = Mathf.Abs(card.Id / 4 - WastePile.TopCardId / 4);
            return idAbsDiff == 1                       // if either one above or below
                   || idAbsDiff == Deck.DECK_SIZE / 4 - 1;      // if either ace or king
        }

        #endregion
    }
}