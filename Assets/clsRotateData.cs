using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clsRotateData
{
    public string RobotName { get; set; }
    public string PartsName { get; set; }
    public Vector3 RotateDestination { get; set; }
    public Vector3 RotateVelocity { get; set; } = Vector3.zero;
    public float Time { get; set; }
}
