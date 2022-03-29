#if NORMCORE

using UnityEngine;
#pragma warning disable 618

namespace Normal.Realtime.Examples
{
    public class CubePlayerManager : MonoBehaviour
    {
        private Realtime _realtime;

        private void Awake()
        {
            // Get the Realtime component on this game object
            _realtime = GetComponent<Realtime>();

            // Notify us when Realtime successfully connects to the room
            _realtime.didConnectToRoom += DidConnectToRoom;
        }

        private void DidConnectToRoom(Realtime realtime)
        {
            // Instantiate the CubePlayer for this client once we've successfully connected to the room
            Realtime.Instantiate("CubePlayer", // Prefab name
                Vector3.up, // Start 1 meter in the air
                Quaternion.identity, // No rotation
                true, // Make sure the RealtimeView on this prefab is owned by this client
                true, // Prevent other clients from calling RequestOwnership() on the root RealtimeView.
                useInstance: realtime); // Use the instance of Realtime that fired the didConnectToRoom event.
        }
    }
}

#endif