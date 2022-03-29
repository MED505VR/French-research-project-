using Sound;
using System.Collections;
using UnityEngine;

namespace EmojiWall
{
    public class EmojiSynchronizedSound : SynchronizedSound
    {
        private StopEmojiSounds _stopEmojiSounds;
        private bool recentlyTriggered;

        protected override void Start()
        {
            base.Start();

            _stopEmojiSounds = FindObjectOfType<StopEmojiSounds>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("DrumstickHeadL") || other.CompareTag("DrumstickHeadR"))
            {
                if (recentlyTriggered) return;

                var isPlaying = SoundAudioSource.isPlaying;

                _stopEmojiSounds.StopAllEmojiSoundsRightNow();

                if (isPlaying) return;
                PlaySynchronizedSound();
                StartCoroutine(RecentlyTriggeredWait());
            }
        }

        private IEnumerator RecentlyTriggeredWait()
        {
            recentlyTriggered = true;
            yield return new WaitForSeconds(0.2f);
            recentlyTriggered = false;
        }
    }
}
