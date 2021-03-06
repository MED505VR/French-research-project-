using Normal.Realtime;
using Normal.Realtime.Serialization;

namespace MovementBasedSynth
{
    [RealtimeModel]
    public partial class SoundSyncModel
    {
        [RealtimeProperty(1, true, true)] private double _frequency;

        [RealtimeProperty(2, true, true)] private float _gain;
    }

    /* ----- Begin Normal Autogenerated Code ----- */
    public partial class SoundSyncModel : RealtimeModel
    {
        public double frequency
        {
            get
            {
                return _frequencyProperty.value;
            }
            set
            {
                if (_frequencyProperty.value == value) return;
                _frequencyProperty.value = value;
                InvalidateReliableLength();
                FireFrequencyDidChange(value);
            }
        }

        public float gain
        {
            get
            {
                return _gainProperty.value;
            }
            set
            {
                if (_gainProperty.value == value) return;
                _gainProperty.value = value;
                InvalidateReliableLength();
                FireGainDidChange(value);
            }
        }

        public delegate void PropertyChangedHandler<in T>(SoundSyncModel model, T value);
        public event PropertyChangedHandler<double> frequencyDidChange;
        public event PropertyChangedHandler<float> gainDidChange;

        public enum PropertyID : uint
        {
            Frequency = 1,
            Gain = 2,
        }

        #region Properties

        private ReliableProperty<double> _frequencyProperty;

        private ReliableProperty<float> _gainProperty;

        #endregion

        public SoundSyncModel() : base(null)
        {
            _frequencyProperty = new ReliableProperty<double>(1, _frequency);
            _gainProperty = new ReliableProperty<float>(2, _gain);
        }

        protected override void OnParentReplaced(RealtimeModel previousParent, RealtimeModel currentParent)
        {
            _frequencyProperty.UnsubscribeCallback();
            _gainProperty.UnsubscribeCallback();
        }

        private void FireFrequencyDidChange(double value)
        {
            try
            {
                frequencyDidChange?.Invoke(this, value);
            }
            catch (System.Exception exception)
            {
                UnityEngine.Debug.LogException(exception);
            }
        }

        private void FireGainDidChange(float value)
        {
            try
            {
                gainDidChange?.Invoke(this, value);
            }
            catch (System.Exception exception)
            {
                UnityEngine.Debug.LogException(exception);
            }
        }

        protected override int WriteLength(StreamContext context)
        {
            var length = 0;
            length += _frequencyProperty.WriteLength(context);
            length += _gainProperty.WriteLength(context);
            return length;
        }

        protected override void Write(WriteStream stream, StreamContext context)
        {
            var writes = false;
            writes |= _frequencyProperty.Write(stream, context);
            writes |= _gainProperty.Write(stream, context);
            if (writes) InvalidateContextLength(context);
        }

        protected override void Read(ReadStream stream, StreamContext context)
        {
            var anyPropertiesChanged = false;
            while (stream.ReadNextPropertyID(out uint propertyID))
            {
                var changed = false;
                switch (propertyID)
                {
                    case (uint)PropertyID.Frequency:
                        {
                            changed = _frequencyProperty.Read(stream, context);
                            if (changed) FireFrequencyDidChange(frequency);
                            break;
                        }
                    case (uint)PropertyID.Gain:
                        {
                            changed = _gainProperty.Read(stream, context);
                            if (changed) FireGainDidChange(gain);
                            break;
                        }
                    default:
                        {
                            stream.SkipProperty();
                            break;
                        }
                }
                anyPropertiesChanged |= changed;
            }
            if (anyPropertiesChanged)
            {
                UpdateBackingFields();
            }
        }

        private void UpdateBackingFields()
        {
            _frequency = frequency;
            _gain = gain;
        }

    }
    /* ----- End Normal Autogenerated Code ----- */
}
