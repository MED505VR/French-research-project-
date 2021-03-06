/************************************************************************************
Copyright : Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.

Your use of this SDK or tool is subject to the Oculus SDK License Agreement, available at
https://developer.oculus.com/licenses/oculussdk/

Unless required by applicable law or agreed to in writing, the Utilities SDK distributed
under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
ANY KIND, either express or implied. See the License for the specific language governing
permissions and limitations under the License.
************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using BoneId = OVRSkeleton.BoneId;

[CustomEditor(typeof(OVRCustomSkeleton))]
public class OVRCustomSkeletonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawPropertiesExcluding(serializedObject, new string[] { "_customBones" });
        serializedObject.ApplyModifiedProperties();

        var skeleton = (OVRCustomSkeleton)target;
        var skeletonType = skeleton.GetSkeletonType();

        if (skeletonType == OVRSkeleton.SkeletonType.None)
        {
            EditorGUILayout.HelpBox("Please select a SkeletonType.", MessageType.Warning);
        }
        else
        {
            if (GUILayout.Button("Auto Map Bones"))
            {
                skeleton.TryAutoMapBonesByName();
                EditorUtility.SetDirty(skeleton);
                EditorSceneManager.MarkSceneDirty(skeleton.gameObject.scene);
            }

            EditorGUILayout.LabelField("Bones", EditorStyles.boldLabel);
            var start = skeleton.GetCurrentStartBoneId();
            var end = skeleton.GetCurrentEndBoneId();
            if (start != BoneId.Invalid && end != BoneId.Invalid)
                for (var i = (int)start; i < (int)end; ++i)
                {
                    var boneName = BoneLabelFromBoneId(skeletonType, (BoneId)i);
                    skeleton.CustomBones[i] = (Transform)EditorGUILayout.ObjectField(boneName, skeleton.CustomBones[i],
                        typeof(Transform), true);
                }
        }
    }

    // force aliased enum values to the more appropriate value
    private static string BoneLabelFromBoneId(OVRSkeleton.SkeletonType skeletonType, BoneId boneId)
    {
        if (skeletonType == OVRSkeleton.SkeletonType.HandLeft || skeletonType == OVRSkeleton.SkeletonType.HandRight)
            switch (boneId)
            {
                case BoneId.Hand_WristRoot:
                    return "Hand_WristRoot";
                case BoneId.Hand_ForearmStub:
                    return "Hand_ForearmStub";
                case BoneId.Hand_Thumb0:
                    return "Hand_Thumb0";
                case BoneId.Hand_Thumb1:
                    return "Hand_Thumb1";
                case BoneId.Hand_Thumb2:
                    return "Hand_Thumb2";
                case BoneId.Hand_Thumb3:
                    return "Hand_Thumb3";
                case BoneId.Hand_Index1:
                    return "Hand_Index1";
                case BoneId.Hand_Index2:
                    return "Hand_Index2";
                case BoneId.Hand_Index3:
                    return "Hand_Index3";
                case BoneId.Hand_Middle1:
                    return "Hand_Middle1";
                case BoneId.Hand_Middle2:
                    return "Hand_Middle2";
                case BoneId.Hand_Middle3:
                    return "Hand_Middle3";
                case BoneId.Hand_Ring1:
                    return "Hand_Ring1";
                case BoneId.Hand_Ring2:
                    return "Hand_Ring2";
                case BoneId.Hand_Ring3:
                    return "Hand_Ring3";
                case BoneId.Hand_Pinky0:
                    return "Hand_Pinky0";
                case BoneId.Hand_Pinky1:
                    return "Hand_Pinky1";
                case BoneId.Hand_Pinky2:
                    return "Hand_Pinky2";
                case BoneId.Hand_Pinky3:
                    return "Hand_Pinky3";
                case BoneId.Hand_ThumbTip:
                    return "Hand_ThumbTip";
                case BoneId.Hand_IndexTip:
                    return "Hand_IndexTip";
                case BoneId.Hand_MiddleTip:
                    return "Hand_MiddleTip";
                case BoneId.Hand_RingTip:
                    return "Hand_RingTip";
                case BoneId.Hand_PinkyTip:
                    return "Hand_PinkyTip";
                default:
                    return "Hand_Unknown";
            }
        else
            return "Skeleton_Unknown";
    }
}