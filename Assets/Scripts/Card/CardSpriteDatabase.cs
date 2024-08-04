using UnityEngine;

namespace TriPeaks
{
    [CreateAssetMenu(fileName = nameof(CardSpriteDatabase), menuName = "TriPeaks/" + nameof(CardSpriteDatabase), order = 0)]
    /// <summary>
    /// Scriptable object to hold Card Sprites
    /// </summary>
    public class CardSpriteDatabase : ScriptableObject
    {
        public struct CardSpriteData
        {
            public Sprite _frontSprite;
            public Sprite _backSprite;
            public Vector2 _scale;

            public CardSpriteData(Sprite frontSprite, Sprite backSprite, Vector2 scale)
            {
                _frontSprite = frontSprite;
                _backSprite = backSprite;
                _scale = scale;
            }
        }

        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private Sprite _backSprite;
        [SerializeField] private Vector2 _relativeSize = Vector2.one;

        /// <summary>
        /// Provides a sprite data for Car based on id
        /// </summary>
        /// <param name="cardId">Card Id</param>
        /// <returns>Card Sprite Data</returns>
        public CardSpriteData GetSpriteData(byte cardId)
            => new(_sprites[cardId], _backSprite, _relativeSize);
    }
}
