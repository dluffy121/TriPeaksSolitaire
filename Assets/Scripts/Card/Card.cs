using System.Collections;
using UnityEngine;

namespace TriPeaks
{
    /// <summary>
    /// This class represents a Card and features functionality like folding, unfolding and Dismissing or card 
    /// </summary>
    public class Card : MonoBehaviour
    {
        [SerializeField] float _flipDuration;
        [SerializeField] SpriteRenderer _frontRenderer;
        [SerializeField] SpriteRenderer _backRenderer;

        private byte _id;
        private bool _isFolded = true;
        private Coroutine _foldCoroutine;
        private Coroutine _moveToCoroutine;

        public byte Id => _id;
        public bool IsFolded => _isFolded;

        /// <summary>
        /// Initializes the card with its id and sprite data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="spriteData"></param>
        public void Initialize(byte id, CardSpriteDatabase.CardSpriteData spriteData)
        {
            _id = id;
            _frontRenderer.sprite = spriteData._frontSprite;
            _backRenderer.sprite = spriteData._backSprite;
            _frontRenderer.transform.localScale = spriteData._scale;
            _backRenderer.transform.localScale = spriteData._scale;
        }

        /// <summary>
        /// Dismisses the card making it un-interactable and not visible
        /// </summary>
        public void Dismiss()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Flip the card upside down
        /// </summary>
        public void Fold()
        {
            if (_foldCoroutine != null)
                StopCoroutine(_foldCoroutine);

            _foldCoroutine = StartCoroutine(IEFoldAnimation(_flipDuration));
            _frontRenderer.sortingOrder = -1;
            _backRenderer.sortingOrder = 0;
            _isFolded = true;
        }

        /// <summary>
        /// Flip the card, revealing it
        /// </summary>
        public void Unfold()
        {
            if (_foldCoroutine != null)
                StopCoroutine(_foldCoroutine);

            _foldCoroutine = StartCoroutine(IEFoldAnimation(_flipDuration));
            _frontRenderer.sortingOrder = 0;
            _backRenderer.sortingOrder = -1;
            _isFolded = false;
        }

        /// <summary>
        /// Moves the card to the target position
        /// </summary>
        /// <param name="target"></param>
        /// <param name="duration"></param>
        /// <param name="delay"></param>
        public void MoveTo(Vector3 target, float duration, float delay = 0)
        {
            if (_moveToCoroutine != null)
                StopCoroutine(_moveToCoroutine);

            _moveToCoroutine = StartCoroutine(IEMoveAnimation(target, duration, delay));
        }

        private IEnumerator IEMoveAnimation(Vector3 target, float duration, float delay = 0)
        {
            if (delay > 0)
                yield return new WaitForSeconds(delay);

            float timer = 0;

            while (timer < duration)
            {
                transform.position = Vector3.Lerp(transform.position, target, timer / duration);

                timer += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            transform.position = target;
        }

        private IEnumerator IEFoldAnimation(float duration)
        {
            float timer = 0;

            Quaternion target = Quaternion.Euler(0, _isFolded ? 0 : 180, 0);

            while (timer < duration)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, target, timer / duration);

                timer += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            transform.localRotation = target;
        }
    }
}