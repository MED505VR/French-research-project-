using System;
using UnityEngine;

namespace Sound
{
    public class TestSynchronizedSound : SynchronizedSound
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.LogWarning("Collider Entered");

            PlaySynchronizedSound();
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.LogWarning("Collider Exited");

            StopSynchronizedSound();
        }
    }
}
