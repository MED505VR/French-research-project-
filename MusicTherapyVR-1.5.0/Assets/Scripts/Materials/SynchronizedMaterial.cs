using Materials.Models;
using Normal.Realtime;
using UnityEngine;

namespace Materials
{
    [RequireComponent(typeof(MeshRenderer))]
    public abstract class SynchronizedMaterial : RealtimeComponent<SynchronizedMaterialModel>
    {
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
        private static readonly int PColor = Shader.PropertyToID("_Color");
        private Material _material;
        private MeshRenderer _meshRenderer;

        protected virtual void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _material = _meshRenderer.material;
            model.color = _material.GetColor(PColor);
            model.emissionColor = _material.GetColor(EmissionColor);
            model.isEmissive = false;
        }

        protected override void OnRealtimeModelReplaced(SynchronizedMaterialModel previousModel,
            SynchronizedMaterialModel currentModel)
        {
            if (previousModel != null)
            {
                previousModel.colorDidChange -= ColorDidChange;
                currentModel.emissionColorDidChange -= EmissionColorDidChange;
                currentModel.isEmissiveDidChange -= IsEmissiveDidChange;
            }

            if (currentModel != null)
            {
                currentModel.colorDidChange += ColorDidChange;
                currentModel.emissionColorDidChange += EmissionColorDidChange;
                currentModel.isEmissiveDidChange += IsEmissiveDidChange;
            }
        }

        private void IsEmissiveDidChange(SynchronizedMaterialModel synchronizedMaterialModel, bool value)
        {
            if (value)
                _material.EnableKeyword("_EMISSION");
            else
                _material.DisableKeyword("_EMISSION");
        }

        private void EmissionColorDidChange(SynchronizedMaterialModel synchronizedMaterialModel, Color value)
        {
            _material.SetColor(EmissionColor, value);
        }

        private void ColorDidChange(SynchronizedMaterialModel synchronizedMaterialModel, Color value)
        {
            _material.SetColor(PColor, value);
        }

        public void SetIsEmissive(bool value)
        {
            model.isEmissive = value;
        }

        public void SetEmissionColor(Color value)
        {
            model.emissionColor = value;
        }

        public void SetColor(Color value)
        {
            model.emissionColor = value;
        }
    }
}