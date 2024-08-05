using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TriPeaks
{
    /// <summary>
    /// This class represents the remaining cards stock pile and also helps setup the TriPeaks
    /// </summary>
    public class StockPile : MonoBehaviour
    {
        static StockPile s_instance;

        #region Serialized Variables

        [SerializeField] Card _cardPrefab;
        [SerializeField] Transform _cardsParent;
        [SerializeField] CardSpriteDatabase _cardSpriteDatabase;

        [SerializeField] BoxCollider2D _collider;

        [Header("Visuals")]
        [SerializeField] SpriteRenderer _deckRenderer;
        [SerializeField] TextMeshPro _stockPileText;

        [Header("New Deck")]
        [SerializeField] float _newDeckCost;
        [SerializeField] string _buyMoreStrFormat;

        #endregion

        #region Member Variables

        List<Deck> _decks = new(3);
        Deck _currentDeck;

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

        private void OnMouseDown()
        {
            DrawFromDeck();
        }

        #endregion

        #region Static Members

        /// <summary>
        /// Event raised when a card is Drawn from the deck
        /// </summary>
        public static event Action OnDrawnFromDeck;

        /// <summary>
        /// Event raised when buying a new deck
        /// </summary>
        public static event Action<float> OnBuyNewDeck;

        /// <summary>
        /// To initialize Stock Pile
        /// </summary>
        public static void Initialize()
            => s_instance.Internal_Initialize();

        public static bool TryDrawCard(out Card card)
            => s_instance.Internal_TryDrawCard(out card);

        public static void UpdateVisuals()
            => s_instance.Internal_UpdateVisuals();

        #endregion

        #region Private Methods

        private void Internal_Initialize()
        {
            _currentDeck = new(_cardPrefab, _cardsParent, _cardSpriteDatabase);
            _decks.Add(_currentDeck);
        }

        private bool Internal_TryDrawCard(out Card card)
            => _currentDeck.TryDrawCard(out card);

        private void DrawFromDeck()
        {
            if (!TryDrawCard(out Card card))
            {
                BuyNewDeck();
                return;
            }

            card.Unfold();
            SoundPlayer.PlayCardFlipSFX();
            WastePile.Add(card);

            OnDrawnFromDeck?.Invoke();

            UpdateVisuals();
        }

        private void BuyNewDeck()
        {
            _currentDeck = new(_cardPrefab, _cardsParent, _cardSpriteDatabase);
            _decks.Add(_currentDeck);

            UpdateVisuals();

            SoundPlayer.PlayDeckPlaceSFX();
            OnBuyNewDeck?.Invoke(_newDeckCost);
        }

        private void Internal_UpdateVisuals()
        {
            if (_currentDeck.CardCount > 0)
            {
                _stockPileText.text = _currentDeck.CardCount.ToString();
                _deckRenderer.color = Color.white;
            }
            else
            {
                _stockPileText.text = string.Format(_buyMoreStrFormat, _newDeckCost);
                _deckRenderer.color -= new Color(0, 0, 0, -0.5f);
            }
        }

        #endregion
    }
}