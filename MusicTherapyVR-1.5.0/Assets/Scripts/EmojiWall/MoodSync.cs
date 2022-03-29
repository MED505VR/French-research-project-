using Normal.Realtime;
using UnityEngine;

namespace EmojiWall
{
    public class MoodSync : RealtimeComponent<MoodSyncModel>
    {
    
        //private MeshRenderer _meshRenderer;
        private MoodSound _play;
        private MeshRenderer _meshRenderer;
        private MeshRenderer _lightRenderer;

        private void Awake()
        {
            _play = GetComponent<MoodSound>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _lightRenderer = GetComponent<MeshRenderer>();
        }

        protected override void OnRealtimeModelReplaced(MoodSyncModel previousModel, MoodSyncModel currentModel)
        {
            if (previousModel != null)
            {
                // Unregister from events
                previousModel.playDidChange -= PlayDidChange;
                previousModel.colorDidChange -= ColorDidChange;
                previousModel.lightDidChange -= LightDidChange;
            }

            if (currentModel != null)
            {
                // If this is a model that has no data set on it, populate it with the current mesh renderer color.
                if (currentModel.isFreshModel)
                    currentModel.play = _play.play;
                currentModel.color = _meshRenderer.material.color;
                currentModel.light = _lightRenderer.material.color;

                // Update the mesh render or boolean to match the new model
                UpdateLoopPlay();
                UpdateMeshRendererColor();
                UpdateMeshRendererLight();

                // Register for events so we'll know if the color or bool changes later
                currentModel.colorDidChange += ColorDidChange;
                currentModel.playDidChange += PlayDidChange;
                currentModel.lightDidChange += LightDidChange;
            }
        }

        private void PlayDidChange(MoodSyncModel model, bool value)
        {
            // Update the bool
            UpdateLoopPlay();
        }

        private void ColorDidChange(MoodSyncModel model, Color value)
        {
            // Update the mesh renderer
            UpdateMeshRendererColor();
        }

        private void LightDidChange(MoodSyncModel model, Color value)
        {
            // Update the mesh renderer
            UpdateMeshRendererLight();
        }

        private void UpdateLoopPlay()
        {
            // Get the bool from the model and set it on the model
            _play.play = model.play;
        }

        private void UpdateMeshRendererColor()
        {
            // Get the color from the model and set it on the mesh renderer
            _meshRenderer.material.color = model.color;
        }

        private void UpdateMeshRendererLight()
        {
            // Get the color from the model and set it on the mesh renderer
            _lightRenderer.material.color = model.light;
        }

        public void SetPlay(bool play)
        {
            // Set the bool on the model
            // This will fire the boolChanged event on the model, which will update the bool for both the local player and all remote players
            model.play = play;
        }

        public void SetColor(Color color)
        {
            // Set the color on the model
            // This will fire the colorChanged event on the model, which will update the renderer for both the local player and all remote players
            model.color = color;
        }

        public void SetLight(Color light)
        {
            // Set the color on the model
            // This will fire the colorChanged event on the model, which will update the renderer for both the local player and all remote players
            model.light = light;
        }
    }
}
