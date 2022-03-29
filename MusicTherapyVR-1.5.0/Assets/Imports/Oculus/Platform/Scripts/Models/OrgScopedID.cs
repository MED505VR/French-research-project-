// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

namespace Oculus.Platform.Models
{
    using System;
    using System.Collections;
    using Models;
    using System.Collections.Generic;
    using UnityEngine;

    public class OrgScopedID
    {
        public readonly ulong ID;


        public OrgScopedID(IntPtr o)
        {
            ID = CAPI.ovr_OrgScopedID_GetID(o);
        }
    }
}