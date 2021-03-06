namespace Oculus.Platform.Models
{
    using UnityEngine;
    using System;
    using System.ComponentModel;

    public class PingResult
    {
        public PingResult(ulong id, ulong? pingTimeUsec)
        {
            ID = id;
            this.pingTimeUsec = pingTimeUsec;
        }

        public ulong ID { get; private set; }
        public ulong PingTimeUsec => pingTimeUsec.HasValue ? pingTimeUsec.Value : 0;
        public bool IsTimeout => !pingTimeUsec.HasValue;

        private ulong? pingTimeUsec;
    }
}