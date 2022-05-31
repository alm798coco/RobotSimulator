using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clsCreateCollisionRigid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform _child in transform)
        {
            clsSetCollider.SetMeshCollider(transform.name, _child.name);
        }

        foreach (Transform _child in transform)
        {
            clsSetRigidBody.Set(transform.name, _child.name);
            _child.gameObject.AddComponent<clsCollisionTriggerProcess>();
        }
    }
}
