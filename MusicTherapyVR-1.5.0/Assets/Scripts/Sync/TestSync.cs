using Normal.Realtime;
using UnityEngine;

namespace Sync
{
    public class TestSync : RealtimeComponent<TestModel>
    {
        private MeshRenderer _meshRenderer;
        private Synth _synth;


        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _synth = GetComponent<Synth>();
        }


        private void UpdateMeshRendererColor()
        {
            // Get the color from the model and set it on the mesh renderer.
            _meshRenderer.material.color = model.color;
        }

        private void UpdateSynthGain()
        {
            _synth.gain = model.gain;
        }

        protected override void OnRealtimeModelReplaced(TestModel previousModel, TestModel currentModel)
        {
            if (previousModel != null)
            {
                // Unregister from events
                previousModel.colorDidChange -= ColorDidChange;
                previousModel.gainDidChange -= GainDidChange;
            }

            if (currentModel != null)
            {
                // If this is a model that has no data set on it, populate it with the current mesh renderer color.
                if (currentModel.isFreshModel)
                    currentModel.color = _meshRenderer.material.color;
                currentModel.gain = _synth.gain;

                // Update the mesh render to match the new model
                UpdateMeshRendererColor();
                UpdateSynthGain();

                // Register for events so we'll know if the color changes later
                currentModel.colorDidChange += ColorDidChange;
                currentModel.gainDidChange += GainDidChange;
            }
        }

        private void ColorDidChange(TestModel model, Color value)
        {
            // Update the mesh renderer
            UpdateMeshRendererColor();
        }

        private void GainDidChange(TestModel model, float value)
        {
            UpdateSynthGain();
        }

        public void SetColor(Color color)
        {
            // Set the color on the model
            // This will fire the colorChanged event on the model, which will update the renderer for both the local player and all remote players.
            model.color = color;
        }

        public void SetGain(float gain)
        {
            model.gain = gain;
        }
    }
}