using UnityEngine;
using Normal.Realtime;

namespace MovementBasedSynth
{
    public class SoundChange : MonoBehaviour
    {
        [SerializeField] private float _gain;
        private float _previousGain;

        [SerializeField] private double _frequency;
        private double _previousFrequency;

        private double freqL;
        private double freqR;
        private bool left = false;
        private bool right = false;

        // Synchronization
        private SoundSync _soundSync;

        // Hand info
        private GameObject leftHand, rightHand;
        private Transform leftHandPosition, rightHandPosition;
        private float leftHandY;
        private float rightHandY;


        private GameObject myObject;
        private RealtimeView myView;

        private float volume = 0.1f;


        private void Awake()
        {
            _soundSync = GetComponent<SoundSync>();

            leftHand = GameObject.Find("LeftHandAnchor");
            leftHandPosition = leftHand.transform;
            rightHand = GameObject.Find("RightHandAnchor");
            rightHandPosition = rightHand.transform;
            myObject = GameObject.Find("SynthCube");
            myView = myObject.GetComponent<RealtimeView>();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == leftHand || other.gameObject == rightHand)
            {
                if (other.gameObject == leftHand) left = true;
                if (other.gameObject == rightHand) right = true;
                myView.RequestOwnership();
                _gain = volume;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == leftHand || other.gameObject == rightHand)
            {
                _gain = 0;
                if (other.gameObject == leftHand) left = false;
                if (other.gameObject == rightHand) right = false;
                myView.ClearOwnership();
            }
        }

        private void Update()
        {
            leftHandY = leftHandPosition.position[1];
            rightHandY = rightHandPosition.position[1];
            freqL = 440 + leftHandY * 100;
            freqR = 440 + rightHandY * 100;
            if (_gain > 0 && left == true) _frequency = freqL;
            if (_gain > 0 && right == true) _frequency = freqR;


            if (_frequency != _previousFrequency)
            {
                _soundSync.SetFrequency(_frequency);
                _previousFrequency = _frequency;
            }

            if (_gain != _previousGain)
            {
                _soundSync.SetGain(_gain);
                _previousGain = _gain;
            }
        }
    }
}
