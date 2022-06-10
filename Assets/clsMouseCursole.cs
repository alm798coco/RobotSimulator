using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class clsMouseCursole : MonoBehaviour
{
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);

    public GameObject camera;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var targetScreenPos = camera.GetComponent<Camera>().WorldToScreenPoint(target.transform.position);

        SetCursorPos((int)targetScreenPos.x, (int)targetScreenPos.y);
    }
}
