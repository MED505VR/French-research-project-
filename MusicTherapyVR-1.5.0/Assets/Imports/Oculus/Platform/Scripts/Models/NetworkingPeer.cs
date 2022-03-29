namespace Oculus.Platform.Models
{
    using UnityEngine;
    using System;
    using System.ComponentModel;

    public class NetworkingPeer
    {
        public NetworkingPeer(ulong id, PeerConnectionState state)
        {
            ID = id;
            State = state;
        }

        public ulong ID { get; private set; }
        public PeerConnectionState State { get; private set; }
    }
}