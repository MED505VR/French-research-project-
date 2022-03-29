using Normal.Realtime;
using UnityEngine;

namespace EmojiWall
{
    public class MoodPlay : MonoBehaviour
    {
        [SerializeField]
        private MoodSync _moodSync; // For normcore syncing 
        private GameObject leftHand, rightHand; // for objects
        private GameObject emoji0, emoji1, emoji2; // For public objects
        private RealtimeView myView; // For normcore object ownership

        // Varaibles for playing the sound
        public bool _play;
        private bool _prevPlay;

        // Variables for colour on the emojis
        private Color _color = default;
        private Color _previousColor = default;
        public Color _original;

        // Variables for the light caused by the emojis
        public Light _light;
        private Color _originalLight;
        public Color _lightColor;
        private Color _previousLight;

        private void Start() 
        {
            _moodSync = GetComponent<MoodSync>();
            leftHand = GameObject.Find("LeftHandAnchor");
            rightHand = GameObject.Find("RightHandAnchor");
            emoji0 = GameObject.Find("AngryEmoji");
            emoji1 = GameObject.Find("SadEmoji");
            emoji2 = GameObject.Find("NeutralEmoji");
            myView = GetComponent<RealtimeView>();
            _play = false;
            _original = GetComponent<Renderer>().material.color;
            _originalLight = _light.GetComponent<Light>().color;


        }

    
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == leftHand || other.gameObject == rightHand)
            {
                /*if (this.GetComponent<MoodSound>().play == true)
            {
                emoji0.GetComponent<MoodSound>().play = false;
                emoji1.GetComponent<MoodSound>().play = false;
                emoji2.GetComponent<MoodSound>().play = false;
                Debug.Log("Hej");
            }*/


                //emoji0.GetComponent<MoodSync>().SetPlay(false);
                //emoji1.GetComponent<MoodSync>().SetPlay(false);
                //emoji2.GetComponent<MoodSync>().SetPlay(false);
            
                /*
            if (emoji0)
            {
                Debug.Log("emoji0 works0");

                myView.RequestOwnership();

                emoji1.GetComponent<MoodPlay>()._play = false;
                emoji2.GetComponent<MoodPlay>()._play = false;

                emoji1.GetComponent<MoodSync>().SetPlay(false);
                emoji2.GetComponent<MoodSync>().SetPlay(false);

                Debug.Log("emoji0 works");

                //emoji1.GetComponent<MoodSync>().SetLight(emoji1.GetComponent<MoodPlay>()._originalLight);
                //emoji2.GetComponent<MoodSync>().SetLight(emoji2.GetComponent<MoodPlay>()._originalLight);

                myView.ClearOwnership();
            }

            else if (emoji1 == true) 
            {
                Debug.Log("emoji1 works0");

                myView.RequestOwnership();
                emoji0.GetComponent<MoodPlay>()._play = false;
                emoji2.GetComponent<MoodPlay>()._play = false;

                emoji0.GetComponent<MoodSync>().SetPlay(false);
                emoji2.GetComponent<MoodSync>().SetPlay(false);

                Debug.Log("emoji1 works");

                //emoji0.GetComponent<MoodSync>().SetLight(emoji0.GetComponent<MoodPlay>()._originalLight);
                //emoji2.GetComponent<MoodSync>().SetLight(emoji2.GetComponent<MoodPlay>()._originalLight);
                myView.ClearOwnership();
            }

            else if (emoji2 == true)
            {
                Debug.Log("emoji2 works0");

                myView.RequestOwnership();
                emoji0.GetComponent<MoodPlay>()._play = false;
                emoji1.GetComponent<MoodPlay>()._play = false;

                emoji0.GetComponent<MoodSync>().SetPlay(false);
                emoji1.GetComponent<MoodSync>().SetPlay(false);

                Debug.Log("emoji2 works");

                //emoji0.GetComponent<MoodSync>().SetLight(emoji0.GetComponent<MoodPlay>()._originalLight);
                //emoji1.GetComponent<MoodSync>().SetLight(emoji1.GetComponent<MoodPlay>()._originalLight);
                myView.ClearOwnership();
            }            */

                myView.RequestOwnership();
                _play = !_play;
                color();
            }
        }


        public void color()
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

        private void OnTriggerExit(Collider other)
        {
            myView.ClearOwnership();
        }

        public void Update()
        {
            if (_play != _prevPlay)
            {
                emoji0.GetComponent<MoodSound>().play = false;
                emoji0.GetComponent<MoodPlay>()._moodSync.SetPlay(false);
                emoji1.GetComponent<MoodSound>().play = false;
                emoji1.GetComponent<MoodPlay>()._moodSync.SetPlay(false);
                emoji2.GetComponent<MoodSound>().play = false;
                emoji2.GetComponent<MoodPlay>()._moodSync.SetPlay(false);

                _moodSync.SetPlay(_play);
                _prevPlay = _play;
            }

            if (_color != _previousColor)
            {
                _moodSync.SetColor(_color);
                _previousColor = _color;
            }

            if (_lightColor != _previousLight)
            {
                _moodSync.SetLight(_lightColor);
                _previousLight = _lightColor;
            }
        }
    }
}
