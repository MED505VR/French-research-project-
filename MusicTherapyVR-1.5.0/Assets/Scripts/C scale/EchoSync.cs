using Normal.Realtime;

namespace C_scale
{
    public class EchoSync : RealtimeComponent<EchoModel>
    {
        private Echo echo;

        private void Awake()
        {
            echo = GetComponent<Echo>();
        }


        private void UpdateEcho()
        {
            echo.echo = model.echo;
        }


        protected override void OnRealtimeModelReplaced(EchoModel previousModel, EchoModel currentModel)
        {
            if (previousModel != null)
                // Unregister from events
                previousModel.echoDidChange -= EchoDidChange;

            if (currentModel != null)
            {
                // If this is a model that has no data set on it, populate it with the current mesh renderer color.
                if (currentModel.isFreshModel)
                    currentModel.echo = echo.echo;

                // Update the mesh render to match the new model
                UpdateEcho();

                // Register for events so we'll know if the color changes later
                currentModel.echoDidChange += EchoDidChange;
            }
        }


        private void EchoDidChange(EchoModel model, bool value)
        {
            UpdateEcho();
        }


        public void SetEcho(bool echo)
        {
            model.echo = echo;
        }
    }
}