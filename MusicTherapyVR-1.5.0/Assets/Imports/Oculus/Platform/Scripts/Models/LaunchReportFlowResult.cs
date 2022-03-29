// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

namespace Oculus.Platform.Models
{
    using System;
    using System.Collections;
    using Models;
    using System.Collections.Generic;
    using UnityEngine;

    public class LaunchReportFlowResult
    {
        public readonly bool DidCancel;
        public readonly ulong UserReportId;


        public LaunchReportFlowResult(IntPtr o)
        {
            DidCancel = CAPI.ovr_LaunchReportFlowResult_GetDidCancel(o);
            UserReportId = CAPI.ovr_LaunchReportFlowResult_GetUserReportId(o);
        }
    }
}