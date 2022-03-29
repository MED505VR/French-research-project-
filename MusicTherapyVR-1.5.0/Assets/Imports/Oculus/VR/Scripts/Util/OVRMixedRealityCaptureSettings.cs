/************************************************************************************
Copyright : Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.

Your use of this SDK or tool is subject to the Oculus SDK License Agreement, available at
https://developer.oculus.com/licenses/oculussdk/

Unless required by applicable law or agreed to in writing, the Utilities SDK distributed
under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
ANY KIND, either express or implied. See the License for the specific language governing
permissions and limitations under the License.
************************************************************************************/

#if UNITY_ANDROID && !UNITY_EDITOR
#define OVR_ANDROID_MRC
#endif

using UnityEngine;
using System;
using System.IO;

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_ANDROID
public class OVRMixedRealityCaptureSettings : ScriptableObject, OVRMixedRealityCaptureConfiguration
{
    public bool enableMixedReality = false;
    public LayerMask extraHiddenLayers;
    public LayerMask extraVisibleLayers;
    public bool dynamicCullingMask = true;
    public OVRManager.CompositionMethod compositionMethod = OVRManager.CompositionMethod.External;
    public Color externalCompositionBackdropColorRift = Color.green;
    public Color externalCompositionBackdropColorQuest = Color.clear;
    public OVRManager.CameraDevice capturingCameraDevice = OVRManager.CameraDevice.WebCamera0;
    public bool flipCameraFrameHorizontally = false;
    public bool flipCameraFrameVertically = false;
    public float handPoseStateLatency = 0.0f;
    public float sandwichCompositionRenderLatency = 0.0f;
    public int sandwichCompositionBufferedFrames = 8;
    public Color chromaKeyColor = Color.green;
    public float chromaKeySimilarity = 0.6f;
    public float chromaKeySmoothRange = 0.03f;
    public float chromaKeySpillRange = 0.04f;
    public bool useDynamicLighting = false;
    public OVRManager.DepthQuality depthQuality = OVRManager.DepthQuality.Medium;
    public float dynamicLightingSmoothFactor = 8.0f;
    public float dynamicLightingDepthVariationClampingValue = 0.001f;
    public OVRManager.VirtualGreenScreenType virtualGreenScreenType = OVRManager.VirtualGreenScreenType.Off;
    public float virtualGreenScreenTopY;
    public float virtualGreenScreenBottomY;
    public bool virtualGreenScreenApplyDepthCulling = false;
    public float virtualGreenScreenDepthTolerance = 0.2f;
    public OVRManager.MrcActivationMode mrcActivationMode;

    // OVRMixedRealityCaptureConfiguration Interface implementation
    bool OVRMixedRealityCaptureConfiguration.enableMixedReality
    {
        get => enableMixedReality;
        set => enableMixedReality = value;
    }

    LayerMask OVRMixedRealityCaptureConfiguration.extraHiddenLayers
    {
        get => extraHiddenLayers;
        set => extraHiddenLayers = value;
    }

    LayerMask OVRMixedRealityCaptureConfiguration.extraVisibleLayers
    {
        get => extraVisibleLayers;
        set => extraVisibleLayers = value;
    }

    bool OVRMixedRealityCaptureConfiguration.dynamicCullingMask
    {
        get => dynamicCullingMask;
        set => dynamicCullingMask = value;
    }

    OVRManager.CompositionMethod OVRMixedRealityCaptureConfiguration.compositionMethod
    {
        get => compositionMethod;
        set => compositionMethod = value;
    }

    Color OVRMixedRealityCaptureConfiguration.externalCompositionBackdropColorRift
    {
        get => externalCompositionBackdropColorRift;
        set => externalCompositionBackdropColorRift = value;
    }

    Color OVRMixedRealityCaptureConfiguration.externalCompositionBackdropColorQuest
    {
        get => externalCompositionBackdropColorQuest;
        set => externalCompositionBackdropColorQuest = value;
    }

    OVRManager.CameraDevice OVRMixedRealityCaptureConfiguration.capturingCameraDevice
    {
        get => capturingCameraDevice;
        set => capturingCameraDevice = value;
    }

    bool OVRMixedRealityCaptureConfiguration.flipCameraFrameHorizontally
    {
        get => flipCameraFrameHorizontally;
        set => flipCameraFrameHorizontally = value;
    }

    bool OVRMixedRealityCaptureConfiguration.flipCameraFrameVertically
    {
        get => flipCameraFrameVertically;
        set => flipCameraFrameVertically = value;
    }

    float OVRMixedRealityCaptureConfiguration.handPoseStateLatency
    {
        get => handPoseStateLatency;
        set => handPoseStateLatency = value;
    }

    float OVRMixedRealityCaptureConfiguration.sandwichCompositionRenderLatency
    {
        get => sandwichCompositionRenderLatency;
        set => sandwichCompositionRenderLatency = value;
    }

    int OVRMixedRealityCaptureConfiguration.sandwichCompositionBufferedFrames
    {
        get => sandwichCompositionBufferedFrames;
        set => sandwichCompositionBufferedFrames = value;
    }

    Color OVRMixedRealityCaptureConfiguration.chromaKeyColor
    {
        get => chromaKeyColor;
        set => chromaKeyColor = value;
    }

    float OVRMixedRealityCaptureConfiguration.chromaKeySimilarity
    {
        get => chromaKeySimilarity;
        set => chromaKeySimilarity = value;
    }

    float OVRMixedRealityCaptureConfiguration.chromaKeySmoothRange
    {
        get => chromaKeySmoothRange;
        set => chromaKeySmoothRange = value;
    }

    float OVRMixedRealityCaptureConfiguration.chromaKeySpillRange
    {
        get => chromaKeySpillRange;
        set => chromaKeySpillRange = value;
    }

    bool OVRMixedRealityCaptureConfiguration.useDynamicLighting
    {
        get => useDynamicLighting;
        set => useDynamicLighting = value;
    }

    OVRManager.DepthQuality OVRMixedRealityCaptureConfiguration.depthQuality
    {
        get => depthQuality;
        set => depthQuality = value;
    }

    float OVRMixedRealityCaptureConfiguration.dynamicLightingSmoothFactor
    {
        get => dynamicLightingSmoothFactor;
        set => dynamicLightingSmoothFactor = value;
    }

    float OVRMixedRealityCaptureConfiguration.dynamicLightingDepthVariationClampingValue
    {
        get => dynamicLightingDepthVariationClampingValue;
        set => dynamicLightingDepthVariationClampingValue = value;
    }

    OVRManager.VirtualGreenScreenType OVRMixedRealityCaptureConfiguration.virtualGreenScreenType
    {
        get => virtualGreenScreenType;
        set => virtualGreenScreenType = value;
    }

    float OVRMixedRealityCaptureConfiguration.virtualGreenScreenTopY
    {
        get => virtualGreenScreenTopY;
        set => virtualGreenScreenTopY = value;
    }

    float OVRMixedRealityCaptureConfiguration.virtualGreenScreenBottomY
    {
        get => virtualGreenScreenBottomY;
        set => virtualGreenScreenBottomY = value;
    }

    bool OVRMixedRealityCaptureConfiguration.virtualGreenScreenApplyDepthCulling
    {
        get => virtualGreenScreenApplyDepthCulling;
        set => virtualGreenScreenApplyDepthCulling = value;
    }

    float OVRMixedRealityCaptureConfiguration.virtualGreenScreenDepthTolerance
    {
        get => virtualGreenScreenDepthTolerance;
        set => virtualGreenScreenDepthTolerance = value;
    }

    OVRManager.MrcActivationMode OVRMixedRealityCaptureConfiguration.mrcActivationMode
    {
        get => mrcActivationMode;
        set => mrcActivationMode = value;
    }

    OVRManager.InstantiateMrcCameraDelegate OVRMixedRealityCaptureConfiguration.instantiateMixedRealityCameraGameObject
    {
        get;
        set;
    }


#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN // Rift MRC only
    private const string configFileName = "mrc.config";

    public void WriteToConfigurationFile()
    {
        var text = JsonUtility.ToJson(this, true);
        try
        {
            var configPath = Path.Combine(Application.dataPath, configFileName);
            Debug.Log("Write OVRMixedRealityCaptureSettings to " + configPath);
            File.WriteAllText(configPath, text);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Exception caught " + e.Message);
        }
    }

    public void CombineWithConfigurationFile()
    {
        try
        {
            var configPath = Path.Combine(Application.dataPath, configFileName);
            if (File.Exists(configPath))
            {
                Debug.Log("MixedRealityCapture configuration file found at " + configPath);
                var text = File.ReadAllText(configPath);
                Debug.Log("Apply MixedRealityCapture configuration");
                JsonUtility.FromJsonOverwrite(text, this);
            }
            else
            {
                Debug.Log("MixedRealityCapture configuration file doesn't exist at " + configPath);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Exception caught " + e.Message);
        }
    }
#endif
}
#endif