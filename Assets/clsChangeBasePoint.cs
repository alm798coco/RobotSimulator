using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clsChangeBasePoint : MonoBehaviour
{
    public void OnClick()
    {
        GameObject _robo = GameObject.Find("Robot1");
        Vector3 _changePoint = _robo.transform.position + (new Vector3(2.0f, 1.0f, 0.0f));
        string _newname = _robo.name + "_child";
        _robo.name = _newname;
        clsCreateEmptyObj.Create("Robot1", _changePoint, Quaternion.identity);
        _robo = GameObject.Find("Robot1");
        _robo.transform.Rotate(90.0f, 0.0f, 0.0f);
        GameObject _roboChild = GameObject.Find(_newname);
        _roboChild.transform.parent = _robo.transform;
    }
}
