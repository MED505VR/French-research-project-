using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;


namespace Assets.Oculus.VR.Editor
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public sealed class OVRPlatformToolSettings : ScriptableObject
    {
        private const string DEFAULT_RELEASE_CHANNEL = "Alpha";

        static OVRPlatformToolSettings()
        {
            // BuildPipeline.isBuildingPlayer cannot be called in a static constructor
            // Run Update once to call TryInitialize then remove delegate
            EditorApplication.update += Update;
        }

        private static void Update()
        {
            // Initialize the instance only if a build is not currently running.
            TryInitialize();
            // Stop running Update
            EditorApplication.update -= Update;
        }

        public static string AppID
        {
            get
            {
                if (Instance.targetPlatform < OVRPlatformTool.TargetPlatform.None &&
                    EditorPrefs.HasKey("OVRPlatformToolSettings_AppID" + (int)Instance.targetPlatform))
                    return EditorPrefs.GetString("OVRPlatformToolSettings_AppID" + (int)Instance.targetPlatform);
                else
                    return "";
            }
            set
            {
                if (Instance.targetPlatform < OVRPlatformTool.TargetPlatform.None)
                    EditorPrefs.SetString("OVRPlatformToolSettings_AppID" + (int)Instance.targetPlatform, value);
            }
        }

        public static string ReleaseNote
        {
            get
            {
                if (Instance.targetPlatform < OVRPlatformTool.TargetPlatform.None &&
                    EditorPrefs.HasKey("OVRPlatformToolSettings_ReleaseNote" + (int)Instance.targetPlatform))
                    return EditorPrefs.GetString("OVRPlatformToolSettings_ReleaseNote" + (int)Instance.targetPlatform);
                else
                    return "";
            }
            set
            {
                if (Instance.targetPlatform < OVRPlatformTool.TargetPlatform.None)
                    EditorPrefs.SetString("OVRPlatformToolSettings_ReleaseNote" + (int)Instance.targetPlatform, value);
            }
        }

        public static string ReleaseChannel
        {
            get
            {
                if (Instance.targetPlatform < OVRPlatformTool.TargetPlatform.None &&
                    EditorPrefs.HasKey("OVRPlatformToolSettings_ReleaseChannel" + (int)Instance.targetPlatform))
                    return EditorPrefs.GetString(
                        "OVRPlatformToolSettings_ReleaseChannel" + (int)Instance.targetPlatform);
                else
                    return "";
            }
            set
            {
                if (Instance.targetPlatform < OVRPlatformTool.TargetPlatform.None)
                    EditorPrefs.SetString("OVRPlatformToolSettings_ReleaseChannel" + (int)Instance.targetPlatform,
                        value);
            }
        }

        public static string ApkBuildPath
        {
            get
            {
                if (Instance.targetPlatform < OVRPlatformTool.TargetPlatform.None &&
                    EditorPrefs.HasKey("OVRPlatformToolSettings_ApkBuildPath" + (int)Instance.targetPlatform))
                    return EditorPrefs.GetString("OVRPlatformToolSettings_ApkBuildPath" + (int)Instance.targetPlatform);
                else
                    return "";
            }
            set
            {
                if (Instance.targetPlatform < OVRPlatformTool.TargetPlatform.None)
                    EditorPrefs.SetString("OVRPlatformToolSettings_ApkBuildPath" + (int)Instance.targetPlatform, value);
            }
        }

        public static string ObbFilePath
        {
            get
            {
                if (Instance.targetPlatform < OVRPlatformTool.TargetPlatform.None &&
                    EditorPrefs.HasKey("OVRPlatformToolSettings_ObbFilePath" + (int)Instance.targetPlatform))
                    return EditorPrefs.GetString("OVRPlatformToolSettings_ObbFilePath" + (int)Instance.targetPlatform);
                else
                    return "";
            }
            set
            {
                if (Instance.targetPlatform < OVRPlatformTool.TargetPlatform.None)
                    EditorPrefs.SetString("OVRPlatformToolSettings_ObbFilePath" + (int)Instance.targetPlatform, value);
            }
        }

        public static string AssetsDirectory
        {
            get
            {
                if (Instance.targetPlatform < OVRPlatformTool.TargetPlatform.None &&
                    EditorPrefs.HasKey("OVRPlatformToolSettings_AssetsDirectory" + (int)Instance.targetPlatform))
                    return EditorPrefs.GetString("OVRPlatformToolSettings_AssetsDirectory" +
                                                 (int)Instance.targetPlatform);
                else
                    return "";
            }
            set
            {
                if (Instance.targetPlatform < OVRPlatformTool.TargetPlatform.None)
                    EditorPrefs.SetString("OVRPlatformToolSettings_AssetsDirectory" + (int)Instance.targetPlatform,
                        value);
            }
        }

        public static string RiftBuildDirectory
        {
            get
            {
                if (EditorPrefs.HasKey("OVRPlatformToolSettings_RiftBuildDirectory"))
                    return EditorPrefs.GetString("OVRPlatformToolSettings_RiftBuildDirectory");
                else
                    return "";
            }
            set => EditorPrefs.SetString("OVRPlatformToolSettings_RiftBuildDirectory", value);
        }

        public static string RiftBuildVersion
        {
            get
            {
                if (EditorPrefs.HasKey("OVRPlatformToolSettings_RiftBuildVersion"))
                    return EditorPrefs.GetString("OVRPlatformToolSettings_RiftBuildVersion");
                else
                    return "";
            }
            set => EditorPrefs.SetString("OVRPlatformToolSettings_RiftBuildVersion", value);
        }

        public static string RiftLaunchFile
        {
            get
            {
                if (EditorPrefs.HasKey("OVRPlatformToolSettings_RiftLaunchFile"))
                    return EditorPrefs.GetString("OVRPlatformToolSettings_RiftLaunchFile");
                else
                    return "";
            }
            set => EditorPrefs.SetString("OVRPlatformToolSettings_RiftLaunchFile", value);
        }

        public static string RiftLaunchParams
        {
            get
            {
                if (EditorPrefs.HasKey("OVRPlatformToolSettings_RiftLaunchParams"))
                    return EditorPrefs.GetString("OVRPlatformToolSettings_RiftLaunchParams");
                else
                    return "";
            }
            set => EditorPrefs.SetString("OVRPlatformToolSettings_RiftLaunchParams", value);
        }

        public static string Rift2DLaunchFile
        {
            get
            {
                if (EditorPrefs.HasKey("OVRPlatformToolSettings_Rift2DLaunchFile"))
                    return EditorPrefs.GetString("OVRPlatformToolSettings_Rift2DLaunchFile");
                else
                    return "";
            }
            set => EditorPrefs.SetString("OVRPlatformToolSettings_Rift2DLaunchFile", value);
        }

        public static string Rift2DLaunchParams
        {
            get
            {
                if (EditorPrefs.HasKey("OVRPlatformToolSettings_Rift2DLaunchParams"))
                    return EditorPrefs.GetString("OVRPlatformToolSettings_Rift2DLaunchParams");
                else
                    return "";
            }
            set => EditorPrefs.SetString("OVRPlatformToolSettings_Rift2DLaunchParams", value);
        }

        public static bool RiftFirewallException
        {
            get
            {
                if (EditorPrefs.HasKey("OVRPlatformToolSettings_RiftFirewallException"))
                    return EditorPrefs.GetBool("OVRPlatformToolSettings_RiftFirewallException");
                else
                    return false;
            }
            set => EditorPrefs.SetBool("OVRPlatformToolSettings_RiftFirewallException", value);
        }

        public static OVRPlatformTool.GamepadType RiftGamepadEmulation
        {
            get
            {
                if (EditorPrefs.HasKey("OVRPlatformToolSettings_RiftGamepadEmulation"))
                    return (OVRPlatformTool.GamepadType)EditorPrefs.GetInt(
                        "OVRPlatformToolSettings_RiftGamepadEmulation");
                else
                    return OVRPlatformTool.GamepadType.OFF;
            }
            set => EditorPrefs.SetInt("OVRPlatformToolSettings_RiftGamepadEmulation", (int)value);
        }

        public static List<RedistPackage> RiftRedistPackages
        {
            get => Instance.riftRedistPackages;
            set => Instance.riftRedistPackages = value;
        }

        public static string LanguagePackDirectory
        {
            get => Instance.languagePackDirectory;
            set => Instance.languagePackDirectory = value;
        }

        public static List<AssetConfig> AssetConfigs
        {
            get => Instance.targetPlatform < OVRPlatformTool.TargetPlatform.None
                ? Instance.assetConfigs[(int)Instance.targetPlatform].configList
                : new List<AssetConfig>();
            set
            {
                if (Instance.targetPlatform < OVRPlatformTool.TargetPlatform.None)
                    Instance.assetConfigs[(int)Instance.targetPlatform].configList = value;
            }
        }

        public static OVRPlatformTool.TargetPlatform TargetPlatform
        {
            get => Instance.targetPlatform;
            set => Instance.targetPlatform = value;
        }

        public static bool RunOvrLint
        {
            get => Instance.runOvrLint;
            set => Instance.runOvrLint = value;
        }

        public static string DebugSymbolsDirectory
        {
            get
            {
                if (EditorPrefs.HasKey("OVRPlatformToolSettings_DebugSymbolsDirectory"))
                    return EditorPrefs.GetString("OVRPlatformToolSettings_DebugSymbolsDirectory");
                else
                    return "";
            }
            set => EditorPrefs.SetString("OVRPlatformToolSettings_DebugSymbolsDirectory", value);
        }

        [SerializeField] private List<RedistPackage> riftRedistPackages;

        [SerializeField] private string languagePackDirectory = "";

        [SerializeField]
        private AssetConfigList[] assetConfigs = new AssetConfigList[(int)OVRPlatformTool.TargetPlatform.None];

        [SerializeField] private OVRPlatformTool.TargetPlatform targetPlatform = OVRPlatformTool.TargetPlatform.None;

        [SerializeField] private bool runOvrLint = true;

        public static bool TryInitialize()
        {
            // If not initialized and Build Player is current running, UnityEditor.AssetDatabase.CreateAsset
            // is unsafe to call and will cause a crash. Only load the resource if it already exists.
            if (instance == null && BuildPipeline.isBuildingPlayer)
            {
                instance = Resources.Load<OVRPlatformToolSettings>("OVRPlatformToolSettings");
                return instance != null;
            }

            // Otherwise create/load the resource instance normally.
            return Instance != null;
        }

        private static OVRPlatformToolSettings instance;

        public static OVRPlatformToolSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<OVRPlatformToolSettings>("OVRPlatformToolSettings");

                    if (instance == null)
                    {
                        if (BuildPipeline.isBuildingPlayer)
                            // UnityEditor.AssetDatabase.CreateAsset is unsafe to call during a build and
                            // may cause a crash.
                            // This should be rare as the asset is created in the static constructor and should
                            // usually exist.
                            throw new UnityEditor.Build.BuildFailedException(
                                "Cannot create OVRPlatformToolSettings asset while building.");
                        instance = CreateInstance<OVRPlatformToolSettings>();

                        var properPath = Path.Combine(Application.dataPath, "Resources");
                        if (!Directory.Exists(properPath)) AssetDatabase.CreateFolder("Assets", "Resources");

                        var fullPath = Path.Combine(
                            Path.Combine("Assets", "Resources"),
                            "OVRPlatformToolSettings.asset"
                        );
                        AssetDatabase.CreateAsset(instance, fullPath);

                        // Initialize cross platform default values for the new instance of OVRPlatformToolSettings here
                        if (instance != null)
                        {
                            for (var i = 0; i < (int)OVRPlatformTool.TargetPlatform.None; i++)
                            {
                                EditorPrefs.SetString("OVRPlatformToolSettings_ReleaseChannel" + i,
                                    DEFAULT_RELEASE_CHANNEL);
                                instance.assetConfigs[i] = new AssetConfigList();
                            }

                            instance.riftRedistPackages = new List<RedistPackage>();
                        }
                    }
                }

                return instance;
            }
            set => instance = value;
        }
    }

    // Wrapper for asset config list so that it can be serialized properly
    [System.Serializable]
    public class AssetConfigList
    {
        public List<AssetConfig> configList;

        public AssetConfigList()
        {
            configList = new List<AssetConfig>();
        }
    }

    [System.Serializable]
    public class AssetConfig
    {
        public enum AssetType
        {
            DEFAULT,
            STORE,
            LANGUAGE_PACK
        };

        public string name;
        public bool required;
        public AssetType type;
        public string sku;

        private bool foldout;

        public AssetConfig(string assetName)
        {
            name = assetName;
        }

        public bool GetFoldoutState()
        {
            return foldout;
        }

        public void SetFoldoutState(bool state)
        {
            foldout = state;
        }
    }

    [System.Serializable]
    public class RedistPackage
    {
        public bool include = false;
        public string name;
        public string id;

        public RedistPackage(string pkgName, string pkgId)
        {
            name = pkgName;
            id = pkgId;
        }
    }
}