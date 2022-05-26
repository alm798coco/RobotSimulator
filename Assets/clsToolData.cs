using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clsToolData
{
    public clsConst.ToolControleMode Mode { get; set; }
    public string ToolFileName { get; set; }
    public string ToolName { get; set; }
    public Vector3 Transrate { get; set; }
    public Vector3 Rotate { get; set; }
    public Vector3 Scale { get; set; }
    public string LinkRobotName { get; set; }
    public string LinkPartuName { get; set; }
}
