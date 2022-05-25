using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clsSetCollision : MonoBehaviour
{
    public void OnClick()
    {
        GameObject _obj = GameObject.Find("Robot1");
        foreach (Transform _child in _obj.transform)
        {
            clsSetCollider.SetMeshCollider("Robot1", _child.name);
        }

        foreach (Transform _child in _obj.transform)
        {
            clsSetRigidBody.Set("Robot1", _child.name);
        }
    }
}
