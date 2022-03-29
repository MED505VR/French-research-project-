using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public bool play = false;
    private bool prevPlay;
    private AudioSource source;

    private void Awake()
    {
        play = false;
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (play != prevPlay)
        {
            if (play)
            {
                source.Play();
                prevPlay = play;
            }

            if (!play)
            {
                source.Play();
                prevPlay = play;
            }
        }
    }
}
