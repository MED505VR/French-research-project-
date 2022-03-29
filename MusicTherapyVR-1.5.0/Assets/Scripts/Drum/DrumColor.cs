using UnityEngine;

namespace Drum
{
    public class DrumColor : MonoBehaviour
    {
        public Color drumSkinOriginal = new Color(204, 204, 169, 255);
        public Color drumSkinHit = new Color(89, 89, 89, 255);
        private GameObject leftHand, rightHand, myObject;

        private void Awake()
        {
            leftHand = GameObject.Find("LeftHandAnchor");
            rightHand = GameObject.Find("RightHandAnchor");
            myObject = GameObject.Find("Skin");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == leftHand || other.gameObject == rightHand)
                myObject.GetComponent<Renderer>().material.color = drumSkinHit;
            else if (other.gameObject != leftHand || other.gameObject != rightHand)
                myObject.GetComponent<Renderer>().material.color = drumSkinOriginal;
        }
    }
}