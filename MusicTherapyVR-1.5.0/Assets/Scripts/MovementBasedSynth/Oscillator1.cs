using UnityEngine;

namespace MovementBasedSynth
{
    public class Oscillator1 : MonoBehaviour
    {
        public double frequency = 440;
        private double increment;
        private double phase;
        private double samplingFrequency = 48000.0;

        public float gain = 0.0f;

        public Material trailMaterial;


        private GameObject leftHand, rightHand;
        private Transform leftHandPosition, rightHandPosition;

        private void Awake()
        {
            leftHand = GameObject.Find("LeftHandAnchor");
            rightHand = GameObject.Find("RightHandAnchor");
        }


        /* private void OnTriggerStay(Collider other)
         {
             Vector3 closestToLeftHand = other.ClosestPoint(leftHandPosition.position);
             if (Vector3.Distance(closestToLeftHand, leftHandPosition.position) < .01f)
             {
                 LeaveTrail(closestToLeftHand, .01f, trailMaterial);
             }

             Vector3 closestToRightHand = other.ClosestPoint(rightHandPosition.position);
             if (Vector3.Distance(closestToLeftHand, rightHandPosition.position) < .01f)
             {
                 LeaveTrail(closestToLeftHand, .01f, trailMaterial);
             }
         }*/


        private void OnAudioFilterRead(float[] data, int channels)
        {
            increment = frequency * 2.0 * Mathf.PI / samplingFrequency;

            for (var i = 0; i < data.Length; i += channels)
            {
                phase += increment;
                data[i] = (float)(gain * Mathf.Sin((float)phase));

                if (channels == 2) data[i + 1] = data[i];
                if (phase > Mathf.PI * 2) phase = 0.0;
            }
        }

        private void LeaveTrail(Vector3 point, float scale, Material material)
        {
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = Vector3.one * scale;
            sphere.transform.position = point;
            sphere.transform.parent = transform.parent;
            sphere.GetComponent<Collider>().enabled = false;
            sphere.GetComponent<Renderer>().material = material;
            Destroy(sphere, 2f);
        }
    }
}
