using UnityEngine;

namespace TriPeaks
{
    /// <summary>
    /// Represents a TriPeaks Node
    /// </summary>
    /// <remarks>
    /// This structure with its Manager can handle many different types of TriPeaks
    /// https://solitaired.com/free-games/tripeaks
    /// The simple referencing of parents and children makes this possible
    /// <remarks/>
    public class TriPeaksNode : MonoBehaviour
    {
        [SerializeField] TriPeaksNode[] _parents;
        [SerializeField] TriPeaksNode[] _children;

        Card _card;

        bool _isOccupied;

        /// <summary>
        /// Place a card on this Node
        /// </summary>
        /// <param name="card"></param>
        /// <param name="cardPlaceDuration"></param>
        /// <param name="placeDelay"></param>
        public void PlaceCard(Card card, float cardPlaceDuration = 0, float placeDelay = 0)
        {
            _card = card;
            _card.MoveTo(transform.position, cardPlaceDuration, placeDelay);
            _isOccupied = true;
        }

        /// <summary>
        /// Will try to unfold the contained card, 
        /// if all children are not occupied it will Unfold, otherwise it won't
        /// </summary>
        public void TryUnfoldCard()
        {
            foreach (TriPeaksNode child in _children)
                if (child._isOccupied)
                    return;

            _card.Unfold();
        }

        private void OnCollectCard()
        {
            _isOccupied = false;

            foreach (TriPeaksNode parent in _parents)
                parent.TryUnfoldCard();
        }

        private void OnMouseDown()
        {
            if (_card.IsFolded
                || !TriPeaksManager.TryCollectCard(_card))
                return;

            OnCollectCard();
        }
    }
}