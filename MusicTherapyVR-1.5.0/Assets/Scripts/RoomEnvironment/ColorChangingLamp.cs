using Normal.Realtime;
using RoomEnvironment.Models;
using UnityEngine;

namespace RoomEnvironment
{
    public class ColorChangingLamp : RealtimeComponent<ColorChangingLampModel>
    {
        private ColorChangingLampController _controller;
        private bool _isChanging;

        public Light PointLight { get; private set; }
        private Light SpotLight { get; set; }

        private void Start()
        {
            if (!PointLight) PointLight = GetComponentsInChildren<Light>()[0];
            if (!SpotLight) SpotLight = GetComponentsInChildren<Light>()[1];

            _controller = FindObjectOfType<ColorChangingLampController>();
            model.color = PointLight.color;
        }

        protected override void OnRealtimeModelReplaced(ColorChangingLampModel previousModel,
            ColorChangingLampModel currentModel)
        {
            if (previousModel != null) previousModel.colorDidChange -= UpdateColorToMatchModel;
            if (currentModel != null) currentModel.colorDidChange += UpdateColorToMatchModel;
        }

        public void SetModelColor(Color color)
        {
            model.color = color;
        }

        private void UpdateColorToMatchModel(ColorChangingLampModel pModel, Color value)
        {
            PointLight.color = value;
            SpotLight.color = value;
        }
    }
}