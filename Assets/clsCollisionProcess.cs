using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clsCollisionProcess : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root == gameObject.transform.root)
        {
            return;
        }

        if (collision.rigidbody != null)
        {
            collision.rigidbody.constraints = RigidbodyConstraints.None;
            collision.rigidbody.isKinematic = false;
            collision.rigidbody.useGravity = true;
        }
    }
}
