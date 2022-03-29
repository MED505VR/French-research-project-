using EmojiWall;
using UnityEngine;

namespace Materials
{
    [RequireComponent(typeof(EmojiSynchronizedSound))]
    public class EmojiMaterialChange : SynchronizedMaterial
    {
        private static readonly int EmissionColor1 = Shader.PropertyToID("_EmissionColor");

        [Header("Color Settings")] [SerializeField]
        private Color color;

        [SerializeField] private Color emissionColor;

        private Color _baseColor, _baseEmissionColor;
        private EmojiSynchronizedSound _emojiSynchronizedSound;

        protected override void Start()
        {
            base.Start();

            _emojiSynchronizedSound = GetComponent<EmojiSynchronizedSound>();
            _baseColor = GetComponent<MeshRenderer>().material.color;
            _baseEmissionColor = GetComponent<MeshRenderer>().material.GetColor(EmissionColor1);
        }

        private void Update()
        {
            if (_emojiSynchronizedSound.SoundAudioSource.isPlaying)
            {
                SetColor(color);
                SetEmissionColor(emissionColor);
                SetIsEmissive(true);
            }
            else
            {
                SetColor(_baseColor);
                SetEmissionColor(_baseEmissionColor);
                SetIsEmissive(false);
            }
        }
    }
}