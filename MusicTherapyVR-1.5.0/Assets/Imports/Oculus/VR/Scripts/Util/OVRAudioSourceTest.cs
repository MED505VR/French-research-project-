using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRAudioSourceTest : MonoBehaviour
{
    public float period = 2.0f;
    private float nextActionTime;

    // Start is called before the first frame update
    private void Start()
    {
        var templateMaterial = GetComponent<Renderer>().material;
        var newMaterial = Instantiate<Material>(templateMaterial);
        newMaterial.color = Color.green;
        GetComponent<Renderer>().material = newMaterial;

        nextActionTime = Time.time + period;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + period;

            var mat = GetComponent<Renderer>().material;
            if (mat.color == Color.green)
                mat.color = Color.red;
            else
                mat.color = Color.green;

            var audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
                Debug.LogError("Unable to find AudioSource");
            else
                audioSource.Play();
        }
    }
}