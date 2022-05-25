using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class clsSetParent
{
    public static bool Set(string objName, string parentPartsName, string childPartsName)
    {
        try
        {
            GameObject _sourceObj = GameObject.Find(objName);

            Transform _parentTransForm = SearchTransform(parentPartsName, _sourceObj.transform);
            Transform _childTransForm = SearchTransform(childPartsName, _sourceObj.transform);

            if (_parentTransForm == null || _childTransForm == null)
            {
                return false;
            }

            _childTransForm.parent = _parentTransForm;

            return true;
        }
        catch (System.Exception)
        {
            return false;
        }               
    }

    public static Transform SearchTransform(string searchPartsName, Transform searchTransform)
    {
        foreach (Transform _child in searchTransform)
        {
            if (_child.gameObject.name == searchPartsName)
            {
                return _child;
            }

            if (_child.childCount > 0)
            {
                Transform _childOfChild = SearchTransform(searchPartsName, _child);
                if (_childOfChild?.gameObject?.name == searchPartsName)
                {
                    return _childOfChild;
                }
            }
        }

        return null;
    }
}
