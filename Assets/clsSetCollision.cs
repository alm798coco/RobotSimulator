using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class clsSetCollision : MonoBehaviour
{
    public void OnClick()
    {
        UnityParentWindow.Program.Launch();
        //GameObject _obj = GameObject.Find("Robot1");
        //foreach (Transform _child in _obj.transform)
        //{
        //    clsSetCollider.SetMeshCollider("Robot1", _child.name);
        //}

        //foreach (Transform _child in _obj.transform)
        //{
        //    clsSetRigidBody.Set("Robot1", _child.name);
        //}
    }
}
