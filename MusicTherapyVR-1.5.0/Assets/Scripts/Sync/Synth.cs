using UnityEngine;

namespace Sync
{
    public class Synth : MonoBehaviour
    {
        public double frequency = 440;
        private double _increment;
        private double _phase;
        private double _samplingFrequency = 48000.0;

        public float gain = 0.0f;
        private float _prevGain;

        public Material material;
        public Material oldMat;

        private MeshRenderer _meshrenderer;

        private void Awake()
        {
            _meshrenderer = gameObject.GetComponent<MeshRenderer>();
        }

        private void OnAudioFilterRead(float[] data, int channels)
        {
            _increment = frequency * 2.0 * Mathf.PI / _samplingFrequency;

            for (var i = 0; i < data.Length; i += channels)
            {
                _phase += _increment;
                data[i] = (float)(gain * Mathf.Sin((float)_phase));

                if (channels == 2) data[i + 1] = data[i];
                if (_phase > Mathf.PI * 2) _phase = 0.0;
            }
        }

        private void Update()
        {
            if (gain != _prevGain)
            {
                if (gain > 0)
                {
                    _meshrenderer.material = material;
                    _prevGain = gain;
                }

                if (gain == 0)
                {
                    _meshrenderer.material = oldMat;
                    _prevGain = gain;
                }
            }
        }
    }
}