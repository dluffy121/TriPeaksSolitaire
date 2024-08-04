using UnityEngine;

namespace TriPeaks
{
    /// <summary>
    /// This class represents the waste pile where all successfully picked cards get collected
    /// </summary>
    public class WastePile : MonoBehaviour
    {
        static WastePile s_instance = null;

        #region Serialized Variables

        [Header("Animation")]
        [SerializeField] private float _cardMoveDuration;

        #endregion

        #region Member Variables

        Card _topCard;
        Card _prevTopCard;

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
        /// Provides with Top Card's Id, if doesn't exist will give <see cref="byte.MaxValue"/>
        /// </summary>
        public static byte TopCardId => s_instance._topCard == null ? byte.MaxValue : s_instance._topCard.Id;

        /// <summary>
        /// Adds a card to this Waste Pile, replacing the top most card with this one
        /// </summary>
        /// <param name="card"></param>
        public static void Add(Card card)
            => s_instance.Internal_Add(card);

        #endregion

        #region Private Methods

        private void Internal_Add(Card card)
        {
            if (_topCard != null)
            {
                if (_prevTopCard != null)
                    _prevTopCard.Dismiss();

                _topCard.MoveTo(_topCard.transform.position + Vector3.forward, 0);
                _topCard.Fold();

                _prevTopCard = _topCard;
            }

            _topCard = card;

            card.MoveTo(transform.position, _cardMoveDuration);
        }

        #endregion
    }
}