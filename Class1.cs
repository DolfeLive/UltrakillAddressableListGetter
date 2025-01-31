using ULTRAKILL;
using UnityEngine;
using BepInEx;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace UltrakillAddressableGetter;

[BepInPlugin("UltrakillAddressableGetter", "UltrakillAddressableGetter", "1.0.0")]
public class Class1 : BaseUnityPlugin
{

    string allAssets = "";
    bool ran = false;
    int index = 0;
    int totalAssets = 0;
    public void Start()
    {
        SceneManager.sceneLoaded += (s, lsm) => {
            index++;
            if (!ran && index > 1)
                OnSceneLoaded();
        };
        
    }

    void OnSceneLoaded()
    {
        ran = true;

        foreach (var locator in Addressables.ResourceLocators)
        {
            foreach (var key in locator.Keys)
            {
                if (locator.Locate(key, typeof(object), out IList<IResourceLocation> locations))
                {
                    foreach (var location in locations)
                    {
                        allAssets += location.PrimaryKey + "\n";
                        totalAssets++;
                    }
                }
            }
        }
        SaveToFile();
    }

   

    void SaveToFile()
    {
        string path = Path.Combine(Paths.ExecutablePath, "Addressables.txt");
        File.WriteAllText(path, allAssets);
        print($"Done! Total Assets: {totalAssets}");

    }


}
