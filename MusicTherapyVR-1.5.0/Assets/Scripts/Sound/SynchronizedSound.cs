using System;
using System.Collections;
using Normal.Realtime;
using Sound.Models;
using UnityEngine;

namespace Sound
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class SynchronizedSound : RealtimeComponent<SynchronizedSoundModel>
    {
        public AudioSource SoundAudioSource { get; set; }

        [field: SerializeField] private AudioClip SoundAudioClip { get; set; }

        protected virtual void Start()
        {
            SoundAudioSource = GetComponent<AudioSource>();
            SoundAudioSource.clip = SoundAudioClip;
        }

        private void Update()
        {
            if (!SoundAudioSource.isPlaying) StopSynchronizedSound();
        }

        public static event Action<string> SynchronizedSoundIsFired;
        public static event Action<string, float> SoundInteractionStrengthIsUsed;

        protected override void OnRealtimeModelReplaced(SynchronizedSoundModel previousModel,
            SynchronizedSoundModel currentModel)
        {
            if (previousModel != null) // Unregister from events 
            {
                previousModel.playSynchronizedSoundDidChange -= PlaySynchronizedSoundDidChange;
                previousModel.soundInteractionStrengthDidChange -= SoundInteractionStrengthDidChange;
            }

            if (currentModel != null)
            {
                // If this is a model that has no data set on it, populate it with the current mesh renderer color.
                if (currentModel.isFreshModel)
                {
                    currentModel.playSynchronizedSound = false;
                    currentModel.soundInteractionStrength = 0;
                }

                // Register for events so we'll know if the color changes later
                currentModel.playSynchronizedSoundDidChange += PlaySynchronizedSoundDidChange;
                currentModel.soundInteractionStrengthDidChange += SoundInteractionStrengthDidChange;
            }
        }

        private void SoundInteractionStrengthDidChange(SynchronizedSoundModel synchronizedSoundModel, float value)
        {
            SoundInteractionStrengthIsUsed?.Invoke(gameObject.name, value);
        }

        protected void SetSoundInteractionStrength(float value)
        {
            model.soundInteractionStrength = value;
        }

        private void PlaySynchronizedSoundDidChange(SynchronizedSoundModel pModel, bool value)
        {
            if (model.playSynchronizedSound)
            {
                SynchronizedSoundIsFired?.Invoke(gameObject.name);
                SoundAudioSource.Play();
            }
            else
            {
                SoundAudioSource.Stop();
            }
        }

        protected void PlaySynchronizedSound()
        {
            StartCoroutine(UpdatePlaySynchronizedSound());
        }

        protected void PlaySynchronizedSound(AudioClip clip)
        {
            SoundAudioSource.clip = clip;
            model.playSynchronizedSound = true;
        }

        public void StopSynchronizedSound()
        {
            model.playSynchronizedSound = false;
        }

        private IEnumerator UpdatePlaySynchronizedSound()
        {
            model.playSynchronizedSound = false;

            yield return new WaitForSecondsRealtime(0.01f); // Wait for previous model update to make it to server

            model.playSynchronizedSound = true;
        }
    }
}