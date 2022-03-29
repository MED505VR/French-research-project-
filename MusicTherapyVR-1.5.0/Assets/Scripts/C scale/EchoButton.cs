using Normal.Realtime;
using UnityEngine;

namespace C_scale
{
    public class EchoButton : MonoBehaviour
    {
        private GameObject myObject;
        private RealtimeView myView;

        private bool _echo = false;
        private bool _previousEcho;

        private EchoSync _echoSync;


        private GameObject leftHand, rightHand;

        //private AudioEchoFilter echo;
        private void Awake()
        {
            myObject = GameObject.Find("Table");
            myView = myObject.GetComponent<RealtimeView>();
            leftHand = GameObject.Find("LeftHandAnchor");
            rightHand = GameObject.Find("RightHandAnchor");

            _echoSync = GetComponent<EchoSync>();
        }

        private void OnTriggerEnter(Collider other)
        {
            myView.RequestOwnership();
            if (other.gameObject == leftHand || other.gameObject == rightHand) _echo = !_echo;
        }

        private void OnTriggerExit(Collider other)
        {
            myView.ClearOwnership();
        }

        // Update is called once per frame
        private void Update()
        {
            if (_echo != _previousEcho)
            {
                _echoSync.SetEcho(_echo);
                _previousEcho = _echo;
            }
        }
    }
}