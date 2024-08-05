using UnityEngine;

namespace TriPeaks
{
    /// <summary>
    /// Helps playing Sounds
    /// </summary>
    public class SoundPlayer : MonoBehaviour
    {
        static SoundPlayer s_instance;

        #region Serialized Variables

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _cardFlipSFX;
        [SerializeField] private AudioClip _cardPlaceSFX;
        [SerializeField] private AudioClip _deckPlaceSFX;

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

        public static void PlayCardFlipSFX()
            => s_instance.Internal_PlayCardFlipSFX();

        public static void PlayCardPlaceSFX()
            => s_instance.Internal_PlayCardPlaceSFX();

        public static void PlayDeckPlaceSFX()
            => s_instance.Internal_PlayDeckPlaceSFX();

        #endregion

        #region Private Methods

        private void Internal_PlayCardFlipSFX()
            => _audioSource.PlayOneShot(_cardFlipSFX);

        private void Internal_PlayCardPlaceSFX()
            => _audioSource.PlayOneShot(_cardPlaceSFX);

        private void Internal_PlayDeckPlaceSFX()
            => _audioSource.PlayOneShot(_deckPlaceSFX);

        #endregion
    }
}