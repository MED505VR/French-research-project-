using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
/*
public class MoodLight : MonoBehaviour
{
    [SerializeField]
    private MoodSync _moodSync;

    private MoodPlay _moodPlay;
    private MeshRenderer lightRenderer;

    private Color _originalLight;
    private Color _previousLight;
    private Color _original;

    public Light _light;

    private void Awake()
    {
        _moodSync = GetComponent<MoodSync>();

        _original = GetComponent<Renderer>().material.color;
        _originalLight = _light.GetComponent<Light>().color;

    }

    private void Update()
    {
        if (_play == true)
        {
            // Farven når den er aktiveret
            _color = Color.Lerp(_original, Color.white, .5f);
            _light.color = Color.Lerp(_originalLight, _lightColor, .5f);
        }       

        else if (_play == false)
        {
            // Farven når den ikke er aktiveret
            _color = _original;
            _light.color = _originalLight;
        }
    }  
}
*/
