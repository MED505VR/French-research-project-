// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

namespace Oculus.Platform.Models
{
    using System;
    using System.Collections;
    using Models;
    using System.Collections.Generic;
    using UnityEngine;

    public class SupplementaryMetric
    {
        public readonly ulong ID;
        public readonly long Metric;


        public SupplementaryMetric(IntPtr o)
        {
            ID = CAPI.ovr_SupplementaryMetric_GetID(o);
            Metric = CAPI.ovr_SupplementaryMetric_GetMetric(o);
        }
    }
}