using Normal.Realtime;
using UnityEngine;

namespace C_scale
{
    public class ScaleChange : MonoBehaviour
    {
        [SerializeField] private float gain;
        private float previousGain;


        // Synchronization
        private ScaleSync scaleSync;

        // Hand info
        private GameObject leftHand, rightHand;


        private GameObject myObject;
        private RealtimeView myView;

        private float volume = 0.1f;


        private void Awake()
        {
            scaleSync = GetComponent<ScaleSync>();

            leftHand = GameObject.Find("LeftHandAnchor");

            rightHand = GameObject.Find("RightHandAnchor");

            myObject = GameObject.Find("Table");
            myView = myObject.GetComponent<RealtimeView>();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == leftHand || other.gameObject == rightHand)
            {
                myView.RequestOwnership();
                gain = volume;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == leftHand || other.gameObject == rightHand)
            {
                gain = 0;
                myView.ClearOwnership();
            }
        }

        private void Update()
        {
            if (gain != previousGain)
            {
                scaleSync.SetGain(gain);
                previousGain = gain;
            }
        }
    }
}