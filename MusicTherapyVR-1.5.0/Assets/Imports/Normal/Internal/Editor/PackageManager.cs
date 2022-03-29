using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;
using UnityEngine;
using Normal.Internal.SimpleJson;

namespace Normal
{
    [InitializeOnLoad]
    public class PackageManager
    {
        private const string __registryURL = "https://normcore-registry.normcore.io";
        private const string __bundleID = "com.normalvr.normcore";
        private static bool __debugLogging = false;

        private static ListRequest __listRequest;
        private static AddRequest __addRequest;

        static PackageManager()
        {
            // Check if the package is already to the project
            CheckIfPackageExists();
        }

        private static void CheckIfPackageExists()
        {
            if (__listRequest != null)
            {
                Debug.LogError("Normcore Package Manager: List request already running. Ignoring.");
                return;
            }

            if (__debugLogging) Debug.Log("Normcore Package Manager: Checking if Normcore is installed");
            __listRequest = Client.List(true);
            EditorApplication.update += CheckListRequestProgress;
        }

        private static void CheckListRequestProgress()
        {
            if (__listRequest == null)
            {
                EditorApplication.update -= CheckListRequestProgress;
                return;
            }

            if (__listRequest.IsCompleted)
            {
                if (__listRequest.Status == StatusCode.Success)
                {
                    var normcoreFound = __listRequest.Result.Any(p => p.name == __bundleID);
                    if (__debugLogging) Debug.Log("Normcore Package Manager: Normcore found: " + normcoreFound);

                    if (!normcoreFound)
                        TryAddNormcore();
                }

                __listRequest = null;
            }
        }

        private static void TryAddNormcore()
        {
            if (AddScopedRegistryIfNeeded())
                AddPackage();
        }

        private static bool AddScopedRegistryIfNeeded()
        {
            // Load packages.json
            var packageManifestPath = Application.dataPath.Replace("/Assets", "/Packages/manifest.json");
            var packageManifestJSON = File.ReadAllText(packageManifestPath);

            // Deserialize
            SimpleJson.TryDeserializeObject(packageManifestJSON, out var packageManifestObject);
            var packageManifest = packageManifestObject as JsonObject;
            if (packageManifest == null)
            {
                Debug.LogError(
                    "Normcore Package Manager: Failed to read project package manifest. Unable to add Normcore.");
                return false;
            }


            // Create scoped registries array if needed
            packageManifest.TryGetValue("scopedRegistries", out var scopedRegistriesObject);
            var scopedRegistries = scopedRegistriesObject as JsonArray;
            if (scopedRegistries == null)
                packageManifest["scopedRegistries"] = scopedRegistries = new JsonArray();

            // Check for Normal registry
            var normalRegistryFound = scopedRegistries.Any(registryObject =>
            {
                var registry = registryObject as JsonObject;
                if (registry == null) return false;

                return registry.TryGetValue("url", out var registryURL) && registryURL as string == __registryURL;
            });
            if (normalRegistryFound)
            {
                if (__debugLogging) Debug.Log("Normcore Package Manager: Found Normal registry");
                return true;
            }

            // Add Normal registry
            var normalRegistry = new JsonObject();
            normalRegistry["name"] = "Normal";
            normalRegistry["url"] = __registryURL;
            normalRegistry["scopes"] = new JsonArray { "com.normalvr", "io.normcore" };
            scopedRegistries.Add(normalRegistry);

            // Serialize and save
            var packageManifestJSONUpdated = SimpleJson.SerializeObject(packageManifest);
            File.WriteAllText(packageManifestPath, packageManifestJSONUpdated);
            if (__debugLogging) Debug.Log("Normcore Package Manager: Added Normal registry");

            return true;
        }

        private static void AddPackage()
        {
            if (__addRequest != null)
            {
                Debug.LogError("Normcore Package Manager: Add request already running. Ignoring.");
                return;
            }

            Debug.Log("Normcore Package Manager: Adding Normcore package to project.");
            __addRequest = Client.Add(__bundleID);
            EditorApplication.update += CheckAddRequestProgress;
        }

        private static void CheckAddRequestProgress()
        {
            if (__addRequest == null)
            {
                EditorApplication.update -= CheckListRequestProgress;
                return;
            }

            if (__addRequest.IsCompleted)
            {
                if (__addRequest.Status == StatusCode.Success)
                {
                    if (__debugLogging) Debug.Log("Normcore Package Manager: Success!");
                }
                else if (__addRequest.Status >= StatusCode.Failure)
                {
                    Debug.LogError("Normcore Package Manager: Failed to add normcore package: " +
                                   __addRequest.Error.message);
                }

                __addRequest = null;
            }
        }
    }
}