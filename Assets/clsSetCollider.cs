using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class clsSetCollider
{
    public static void SetMeshCollider(string objName, string partsName = null)
    {
        GameObject _obj = GameObject.Find(objName);

        if (!string.IsNullOrEmpty(partsName))
        {
            _obj = clsSetParent.SearchTransform(partsName, _obj.transform).gameObject;
        }

        _obj.AddComponent<MeshCollider>();

        MeshCollider _mesh = _obj.GetComponent<MeshCollider>();
        _mesh.convex = true;
    }
}
