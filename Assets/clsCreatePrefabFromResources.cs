using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class clsCreatePrefabFromResources : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            GameObject prefab = (GameObject)Resources.Load("Unnamed");
            bool prefabSuccess;
            PrefabUtility.SaveAsPrefabAssetAndConnect(prefab, "Assets/aaa.prefab", InteractionMode.UserAction, out prefabSuccess);
        }
        catch (System.Exception ex)
        {
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
