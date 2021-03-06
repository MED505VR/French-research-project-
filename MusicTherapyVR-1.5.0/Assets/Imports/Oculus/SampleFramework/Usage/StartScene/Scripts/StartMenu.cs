using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Create menu of all scenes included in the build.
public class StartMenu : MonoBehaviour
{
    public OVROverlay overlay;
    public OVROverlay text;
    public OVRCameraRig vrRig;

    private void Start()
    {
        DebugUIBuilder.instance.AddLabel("Select Sample Scene");

        var n = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        for (var i = 0; i < n; ++i)
        {
            var path = UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i);
            var sceneIndex = i;
            DebugUIBuilder.instance.AddButton(Path.GetFileNameWithoutExtension(path), () => LoadScene(sceneIndex));
        }

        DebugUIBuilder.instance.Show();
    }

    private void LoadScene(int idx)
    {
        DebugUIBuilder.instance.Hide();
        Debug.Log("Load scene: " + idx);
        UnityEngine.SceneManagement.SceneManager.LoadScene(idx);
    }
}