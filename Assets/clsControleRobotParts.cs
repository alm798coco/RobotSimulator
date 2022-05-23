using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class clsControleRobotParts
{
    public static void Move(string robotName, string partsName, Vector3 moveAmount)
    {
        GameObject _robot = GameObject.Find(robotName);
        Transform _partsTran = clsSetParent.SearchTransform(partsName, _robot.transform);
        if (_partsTran != null)
        {
            _partsTran.transform.Translate(moveAmount);
        }        
    }

    public static void RoatateOri(string robotName, string partsName, Quaternion roatateAmount)
    {
        GameObject _robot = GameObject.Find(robotName);
        Transform _partsTran = clsSetParent.SearchTransform(partsName, _robot.transform);
        if (_partsTran != null)
        {
            _partsTran.transform.Rotate(roatateAmount.eulerAngles);
        }        
    }
}
