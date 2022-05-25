using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class clsSetRigidBody
{
    public static void Set(string objName, string partsName = null)
    {
        GameObject _obj = GameObject.Find(objName);

        if (!string.IsNullOrEmpty(partsName))
        {
            _obj = clsSetParent.SearchTransform(partsName, _obj.transform).gameObject;
        }

        _obj.AddComponent<Rigidbody>();

        Rigidbody _rigid = _obj.GetComponent<Rigidbody>();
        _rigid.useGravity = false;
        _rigid.isKinematic = false;
        _rigid.constraints = RigidbodyConstraints.FreezeAll;
    }
}
