// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

namespace Oculus.Platform.Models
{
    using System;
    using System.Collections;
    using Models;
    using System.Collections.Generic;
    using UnityEngine;

    public class CalApplicationProposed
    {
        public readonly ulong ID;


        public CalApplicationProposed(IntPtr o)
        {
            ID = CAPI.ovr_CalApplicationProposed_GetID(o);
        }
    }
}