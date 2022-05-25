using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    public void OnClick()
    {
        Vector3 _vector = new Vector3(-96.66f, 1.4f, 99.79f);
        clsCreateEmptyObj.Create("trail", _vector, Quaternion.identity);
        GameObject _trail = GameObject.Find("trail");
        _trail.AddComponent<TrailRenderer>();
        TrailRenderer _trailRenderer = _trail.GetComponent<TrailRenderer>();
        Material mat = (Material)Resources.Load("CFX_RayRounded");
        //Shader shader = (Shader)Resources.Load("CFX3 Multiply Color");
        //mat.shader = shader;
        _trailRenderer.material = mat;
        _trailRenderer.time = 2.0f;


        GameObject _robot = GameObject.Find("Robot1");
        Transform _transform = clsSetParent.SearchTransform("V6S051", _robot.transform);
        _trail.transform.parent = _transform;
    }
}
