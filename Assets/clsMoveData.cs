using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clsMoveData
{
    public string RobotName { get; set; }
    public string PartsName { get; set; }
    public Vector3 MoveDestination { get; set; }
    public Vector3 MoveVelocity { get; set; } = Vector3.zero;
    public float Time { get; set; }
}
