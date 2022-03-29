using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundTEST : MonoBehaviour
{
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DrumStickHead")
        {
            source.Play();
        }
        
    }
}
