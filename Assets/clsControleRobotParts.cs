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

    public static void Move(string robotName, string partsName, Vector3 moveAmount, float speed)
    {
        GameObject _robot = GameObject.Find(robotName);
        Transform _partsTran = clsSetParent.SearchTransform(partsName, _robot.transform);
        Vector3 _startPosition = _partsTran.position;
        Vector3 _endPosition = _partsTran.position += moveAmount;

        if (_partsTran != null)
        {
            float _startTime = Time.time;
            float _journeyLength = Vector3.Distance(_startPosition, _endPosition);
            float _fracJourney = 0;
            while (_fracJourney < 1)
            {
                float _distCovered = (Time.time - _startTime) * speed;
                _fracJourney = _distCovered / _journeyLength;
                _partsTran.position = Vector3.Lerp(_startPosition, _endPosition, _fracJourney);
            }                                    
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

    public static void RoatateOri(string robotName, string partsName, Vector3 moveAmount, float speed)
    {
        //GameObject _robot = GameObject.Find(robotName);
        //Transform _partsTran = clsSetParent.SearchTransform(partsName, _robot.transform);
        //Vector3 _startPosition = Quaternion.Euler(_partsTran.rotation.x, _partsTran.rotation.y, _partsTran.rotation.z);
        //Vector3 _endPosition = _partsTran.position += moveAmount;

        //if (_partsTran != null)
        //{
        //    float _startTime = Time.time;
        //    float _journeyLength = Vector3.Distance(_startPosition, _endPosition);
        //    float _fracJourney = 0;
        //    while (_fracJourney < 1)
        //    {
        //        float _distCovered = (Time.time - _startTime) * speed;
        //        _fracJourney = _distCovered / _journeyLength;
        //        _partsTran.position = Vector3.Lerp(_startPosition, _endPosition, _fracJourney);
        //    }
        //}
    }
}
