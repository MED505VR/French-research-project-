using UnityEngine;
using UnityEditor;
using System.Collections;

namespace OVR
{
/*
-----------------------

SoundFXPropertyDrawer

-----------------------
*/
    [CustomPropertyDrawer(typeof(SoundFX))]
    public class SoundFXPropertyDrawer : PropertyDrawer
    {
        private static float lineHeight = EditorGUIUtility.singleLineHeight + 2.0f;

        private static string[] props = new string[]
        {
            "name", "playback", "volume", "pitchVariance", "falloffDistance", "falloffCurve", "reverbZoneMix", "spread",
            "pctChanceToPlay", "priority", "delay", "looping", "ospProps", "soundClips"
        };

        /*
        -----------------------
        OnGUI()
        -----------------------
        */
        public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
        {
            EditorGUILayout.BeginVertical();
            for (var i = 0; i < props.Length; i++)
            {
                EditorGUI.indentLevel = 2;
                var property = prop.FindPropertyRelative(props[i]);
                if (props[i] == "reverbZoneMix")
                {
                    EditorGUILayout.BeginHorizontal();
                    var reverbCurve = prop.FindPropertyRelative("reverbZoneMix");
                    EditorGUILayout.PropertyField(reverbCurve, true, GUILayout.Width(Screen.width - 130.0f));
                    if (GUILayout.Button("Reset", GUILayout.Width(50.0f)))
                        reverbCurve.animationCurveValue = new AnimationCurve(new Keyframe[2]
                            { new Keyframe(0f, 1.0f), new Keyframe(1f, 1f) });
                    EditorGUILayout.EndHorizontal();
                }
                else
                {
                    EditorGUILayout.PropertyField(property, true, GUILayout.Width(Screen.width - 80.0f));
                    position.y += lineHeight + 4.0f;
                    if (props[i] == "falloffCurve")
                        if (property.enumValueIndex == (int)AudioRolloffMode.Custom)
                        {
                            EditorGUILayout.PropertyField(prop.FindPropertyRelative("volumeFalloffCurve"), true,
                                GUILayout.Width(Screen.width - 80.0f));
                            position.y += lineHeight + 4.0f;
                        }
                }
            }

            EditorGUILayout.EndVertical();
            GUILayout.Space(5.0f);
        }

        /*
        -----------------------
        GetPropertyHeight()
        -----------------------
        */
        public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
        {
            return base.GetPropertyHeight(prop, label);
        }
    }
} // namespace OVR