using Normal.Realtime;

namespace MovementBasedSynth
{
    public class SoundSync : RealtimeComponent<SoundSyncModel>
    {
        private Oscillator1 _oscillator;

        private void Awake()
        {
            _oscillator = GetComponent<Oscillator1>();
        }

        private void UpdateFrequency()
        {
            _oscillator.frequency = model.frequency;
        }

        private void UpdateGain()
        {
            _oscillator.gain = model.gain;
        }


        protected override void OnRealtimeModelReplaced(SoundSyncModel previousModel, SoundSyncModel currentModel)
        {
            if (previousModel != null)
            {
                // Unregister from events
                previousModel.frequencyDidChange -= FrequencyDidChange;
                previousModel.gainDidChange -= GainDidChange;
            }

            if (currentModel != null)
            {
                // If this is a model that has no data set on it, populate it with the current mesh renderer color.
                if (currentModel.isFreshModel)
                    currentModel.frequency = _oscillator.frequency;
                currentModel.gain = _oscillator.gain;

                // Update the mesh render to match the new model
                UpdateFrequency();
                UpdateGain();

                // Register for events so we'll know if the color changes later
                currentModel.frequencyDidChange += FrequencyDidChange;
                currentModel.gainDidChange += GainDidChange;
            }
        }


        private void FrequencyDidChange(SoundSyncModel model, double value)
        {
            UpdateFrequency();
        }

        private void GainDidChange(SoundSyncModel model, float value)
        {
            UpdateGain();
        }

        public void SetFrequency(double frequency)
        {
            model.frequency = frequency;
        }

        public void SetGain(float gain)
        {
            model.gain = gain;
        }
    }
}
