// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

namespace Oculus.Platform.Models
{
    using System;
    using System.Collections;
    using Models;
    using System.Collections.Generic;
    using UnityEngine;

    public class AssetFileDownloadCancelResult
    {
        public readonly ulong AssetFileId;
        public readonly ulong AssetId;
        public readonly string Filepath;
        public readonly bool Success;


        public AssetFileDownloadCancelResult(IntPtr o)
        {
            AssetFileId = CAPI.ovr_AssetFileDownloadCancelResult_GetAssetFileId(o);
            AssetId = CAPI.ovr_AssetFileDownloadCancelResult_GetAssetId(o);
            Filepath = CAPI.ovr_AssetFileDownloadCancelResult_GetFilepath(o);
            Success = CAPI.ovr_AssetFileDownloadCancelResult_GetSuccess(o);
        }
    }
}