using UnityEngine;

namespace TriPeaks
{
    /// <summary>
    /// This class represents a Card Deck
    /// </summary>
    public class Deck
    {
        #region Const

        public const int DECK_SIZE = 52;

        #endregion

        #region Member Variables

        byte _cardCount = DECK_SIZE;

        readonly byte[] _cards = new byte[DECK_SIZE]
        {
            0,  // Ace
            1,  // Ace
            2,  // Ace
            3,  // Ace

            4,  // Two
            5,  // Two
            6,  // Two
            7,  // Two

            8,  // Three
            9,  // Three
            10,  // Three
            11,  // Three

            12,  // Four
            13,  // Four
            14,  // Four
            15,  // Four

            16,  // Five
            17,  // Five
            18,  // Five
            19,  // Five

            20,  // Six
            21,  // Six
            22,  // Six
            23,  // Six

            24,  // Seven
            25,  // Seven
            26,  // Seven
            27,  // Seven

            28,  // Eight
            29,  // Eight
            30,  // Eight
            31,  // Eight

            32,  // Nine
            33,  // Nine
            34,  // Nine
            35,  // Nine

            36,  // Ten
            37,  // Ten
            38,  // Ten
            39,  // Ten

            40, // Jack
            41, // Jack
            42, // Jack
            43, // Jack

            44, // Queen
            45, // Queen
            46, // Queen
            47, // Queen

            48, // King
            49, // King
            50, // King
            51, // King
        };


        readonly Card _cardPrefab;
        readonly Transform _cardsParent;
        readonly CardSpriteDatabase _cardSpriteDatabase;

        #endregion

        #region Public Members

        /// <summary>
        /// Remaining Card Count
        /// </summary>
        public byte CardCount => _cardCount;

        public Deck(Card cardPrefab, Transform cardsParent, CardSpriteDatabase cardSpriteDatabase)
        {
            _cardPrefab = cardPrefab;
            _cardsParent = cardsParent;
            _cardSpriteDatabase = cardSpriteDatabase;
        }

        /// <summary>
        /// Will try drawing a card
        /// </summary>
        /// <param name="card">Outs Card as reference</param>
        /// <returns>True if cards remaining, false otherwise</returns>
        public bool TryDrawCard(out Card card)
        {
            if (_cardCount == 0)
            {
                card = default;
                return false;
            }

            card = Object.Instantiate(_cardPrefab, _cardsParent);
            card.transform.localPosition = Vector3.zero;

            byte id = GenerateCardId();

            card.Initialize(id,
                            _cardSpriteDatabase.GetSpriteData(id));

            return true;
        }

        #endregion

        #region Private Methods

        private byte GenerateCardId()
        {
            int index = Random.Range(0, _cardCount);

            byte id = _cards[index];

            RemoveCard(index);

            return id;
        }

        private void RemoveCard(int index)
        {
            _cardCount--;
            (_cards[index], _cards[_cardCount]) = (_cards[_cardCount], _cards[index]); // swap with last element
        }

        #endregion
    }
}
