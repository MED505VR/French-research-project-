using System;
using System.Collections;
using JetBrains.Annotations;
using Sound;
using UnityEngine;
using UnityEngine.Events;

namespace EmojiWall
{
    public class EmojiSynchronizedSound : SynchronizedSound
    {
        public static UnityEvent<string> emojiActivated;
        private bool _recentlyTriggered;
        private StopEmojiSounds _stopEmojiSounds;

        protected void Awake()
        {
            emojiActivated ??= new UnityEvent<string>();
        }

        protected override void Start()
        {
            base.Start();

            _stopEmojiSounds = FindObjectOfType<StopEmojiSounds>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("DrumstickHeadL") || other.CompareTag("DrumstickHeadR"))
            {
                if (_recentlyTriggered) return;

                var isPlaying = SoundAudioSource.isPlaying;

                _stopEmojiSounds.StopAllEmojiSoundsRightNow();

                if (isPlaying) return;
                
                PlaySynchronizedSound();
                emojiActivated.Invoke(gameObject.name);
                StartCoroutine(RecentlyTriggeredWait());
            }
        }

        private IEnumerator RecentlyTriggeredWait()
        {
            _recentlyTriggered = true;
            yield return new WaitForSeconds(0.2f);
            _recentlyTriggered = false;
        }
    }
}