using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class clsIsKineticChange
{
    public static void ChangeIsKinetic(Transform root, bool cValue)
    {
        foreach (Transform _child in root)
        {
            Rigidbody _rigidbody = _child.GetComponent<Rigidbody>();
            if (_rigidbody != null)
            {
                _rigidbody.isKinematic = cValue;
            }

            if (_child.childCount > 0)
            {
                ChangeIsKinetic(_child, cValue);
            }
        }
    }
}
