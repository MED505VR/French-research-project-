using UnityEngine.Audio;
using UnityEngine;
using System;

namespace Sound
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 10f)] public float volume;
        [Range(1f, 2f)] public float pitch;

        public bool loop;

        [HideInInspector] public AudioSource source;
    }

    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;

        private void Awake()
        {
            foreach (var s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }

        public void Play(string name)
        {
            var s = Array.Find(sounds, Sound => Sound.name == name);
            s.source.Play();
        }

        public void Stop(string name)
        {
            var s = Array.Find(sounds, Sound => Sound.name == name);
            s.source.Stop();
        }
    }
}
