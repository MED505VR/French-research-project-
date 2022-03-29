using UnityEngine;

namespace EmojiWall
{
    public class StopEmojiSounds : MonoBehaviour
    {
        private EmojiSynchronizedSound[] _emojiSynchronizedSounds;

        private void Start()
        {
            _emojiSynchronizedSounds = FindObjectsOfType<EmojiSynchronizedSound>();
        }

        private void OnTriggerEnter(Collider other)
        {
            StopAllEmojiSoundsRightNow();
        }

        public void StopAllEmojiSoundsRightNow()
        {
            foreach (var emojiSynchronizedSound in _emojiSynchronizedSounds)
                emojiSynchronizedSound.StopSynchronizedSound();
        }
    }
}