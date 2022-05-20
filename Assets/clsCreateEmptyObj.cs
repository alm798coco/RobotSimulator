using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class clsCreateEmptyObj
{
    public static void Create(string objName, Vector3 position, Quaternion quaternion, bool setParent = false, string parentName = null)
    {
        GameObject _newObj = new GameObject(objName);
        _newObj.transform.position = position;
        _newObj.transform.rotation = quaternion;

        if (setParent)
        {
            GameObject _parentObj = GameObject.Find(parentName);
            if (_parentObj != null)
            {
                _newObj.transform.parent = _parentObj.transform;
            }
        }        
    }
}
