// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

namespace Oculus.Platform.Models
{
    using System;
    using System.Collections;
    using Models;
    using System.Collections.Generic;
    using UnityEngine;

    public class CalApplicationFinalized
    {
        public readonly int CountdownMS;
        public readonly ulong ID;
        public readonly string LaunchDetails;


        public CalApplicationFinalized(IntPtr o)
        {
            CountdownMS = CAPI.ovr_CalApplicationFinalized_GetCountdownMS(o);
            ID = CAPI.ovr_CalApplicationFinalized_GetID(o);
            LaunchDetails = CAPI.ovr_CalApplicationFinalized_GetLaunchDetails(o);
        }
    }
}