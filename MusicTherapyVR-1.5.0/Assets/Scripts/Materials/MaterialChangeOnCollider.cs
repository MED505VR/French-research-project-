using UnityEngine;

namespace Materials
{
    public class MaterialChangeOnCollider : SynchronizedMaterial
    {
        private static readonly int EmissionColor1 = Shader.PropertyToID("_EmissionColor");

        [SerializeField] private Color _emissionColor;
        [SerializeField] private Color _color;
        private Color _baseColor;
        private Color _baseEmissionColor;

        protected override void Start()
        {
            base.Start();

            _baseColor = GetComponent<MeshRenderer>().material.color;
            _baseEmissionColor = GetComponent<MeshRenderer>().material.GetColor(EmissionColor1);
        }

        private void OnTriggerEnter(Collider other)
        {
            SetEmissionColor(_emissionColor);
            SetColor(_color);
        }

        private void OnTriggerExit(Collider other)
        {
            SetEmissionColor(_baseEmissionColor);
            SetColor(_baseColor);
        }
    }
}