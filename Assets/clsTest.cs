using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clsTest : MonoBehaviour
{
    public GameObject test;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        test.transform.rotation = Quaternion.AngleAxis(0.2f, Vector3.forward) * test.transform.rotation;
    }
}
