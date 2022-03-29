using System;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEditor;

public class BuildAssetBundles : MonoBehaviour
{
    [MenuItem("Oculus/Tools/Build Mobile-Quest Expansion File", false, 100000)]
    public static void BuildBundles()
    {
        // Create expansion file directory and call build asset bundles
        var path = Application.dataPath + "/../Asset Bundles/";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);

        // Rename asset bundle file to the proper obb string
        if (File.Exists(path + "Asset Bundles"))
        {
            var expansionName = "main." + PlayerSettings.Android.bundleVersionCode + "." +
                                PlayerSettings.applicationIdentifier + ".obb";
            try
            {
                if (File.Exists(path + expansionName)) File.Delete(path + expansionName);
                File.Move(path + "Asset Bundles", path + expansionName);
                Debug.Log("OBB expansion file " + expansionName + " has been successfully created at " + path);

                UpdateAndroidManifest();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }

    public static void UpdateAndroidManifest()
    {
        var manifestFolder = Application.dataPath + "/Plugins/Android";
        try
        {
            // Load android manfiest file
            var doc = new XmlDocument();
            doc.Load(manifestFolder + "/AndroidManifest.xml");

            string androidNamepsaceURI;
            var element = (XmlElement)doc.SelectSingleNode("/manifest");
            if (element == null)
            {
                Debug.LogError("Could not find manifest tag in android manifest.");
                return;
            }

            // Get android namespace URI from the manifest
            androidNamepsaceURI = element.GetAttribute("xmlns:android");
            if (!string.IsNullOrEmpty(androidNamepsaceURI))
            {
                // Check if the android manifest already has the read external storage permission
                var nodeList = doc.SelectNodes("/manifest/application/uses-permission");
                foreach (XmlElement e in nodeList)
                {
                    var attr = e.GetAttribute("name", androidNamepsaceURI);
                    if (attr == "android.permission.READ_EXTERNAL_STORAGE")
                    {
                        Debug.Log("Android manifest already has the proper permissions.");
                        return;
                    }
                }

                element = (XmlElement)doc.SelectSingleNode("/manifest/application");
                if (element != null)
                {
                    // Insert read external storage permission
                    var newElement = doc.CreateElement("uses-permission");
                    newElement.SetAttribute("name", androidNamepsaceURI, "android.permission.READ_EXTERNAL_STORAGE");
                    element.AppendChild(newElement);

                    doc.Save(manifestFolder + "/AndroidManifest.xml");
                    Debug.Log("Successfully modified android manifest with external storage permission.");
                    return;
                }
            }

            Debug.LogError("Could not find android naemspace URI in android manifest.");
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
}