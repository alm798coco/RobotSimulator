using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clsStepControle : MonoBehaviour
{
    private Vector3 m_firstAng;
    private bool m_setFirst = false;
    private int m_currentStepInd;
    private List<Vector3> m_vectors = new List<Vector3>();
    private bool m_stepCont = false;
    private bool m_Record = false;
    private float m_timeSpan = 0.1f;
    private float m_time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (m_setFirst)
        {
            m_setFirst = false;
            transform.localEulerAngles = m_firstAng;            
            m_currentStepInd = 0;

            m_Record = false;
        }

        if (m_stepCont)
        {
            m_stepCont = false;
            transform.localEulerAngles = m_vectors[m_currentStepInd];
        }

        if (m_Record)
        {
            m_time += Time.deltaTime;
            if (m_time >= m_timeSpan)
            {
                m_vectors.Add(transform.localEulerAngles);
            }
        }
    }

    public void StartRecord()
    {
        if (!m_Record)
        {
            m_time = 0.0f;
            m_Record = true;
        }        
    }

    public void SetFirstAng(Vector3 ang)
    {
        m_firstAng = ang;
    }

    public void SetReadyStep()
    {
        m_setFirst = true;
    }

    public void SetStepAng(Vector3 ang)
    {
        m_vectors.Add(ang);
    }

    public void NextStep()
    {
        if (m_currentStepInd + 20 >= m_vectors.Count)
        {
            return;
        }

        m_currentStepInd += 20;
        m_stepCont = true;
    }

    public void PrevStep()
    {
        if (m_currentStepInd - 20 <= 0)
        {
            return;
        }

        m_currentStepInd -= 20;
        m_stepCont = true;
    }
}
