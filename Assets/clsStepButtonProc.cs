using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clsStepButtonProc : MonoBehaviour
{
    private bool m_ready = false;
    private GameObject m_partsObj;
    private bool m_stepPrev = false;
    private bool m_stepNext = false;



    // Start is called before the first frame update
    void Start()
    {        


    }

    // Update is called once per frame
    void Update()
    {
        if (m_ready)
        {
            m_ready = false;
            Transform _roboTran = GameObject.Find("Robot").transform;
            Transform _partsTran = clsSetParent.SearchTransform("AxisV6S048", _roboTran);            
            _roboTran.GetComponent<clsControleManager>().Stop();
            _partsTran.GetComponent<clsStepControle>().SetReadyStep();
        }

        if (m_stepPrev)
        {
            m_stepPrev = false;

            Transform _roboTran = GameObject.Find("Robot").transform;
            Transform _partsTran = clsSetParent.SearchTransform("AxisV6S048", _roboTran);

            _partsTran.GetComponent<clsStepControle>().PrevStep();
        }

        if (m_stepNext)
        {
            m_stepNext = false;

            Transform _roboTran = GameObject.Find("Robot").transform;
            Transform _partsTran = clsSetParent.SearchTransform("AxisV6S048", _roboTran);

            _partsTran.GetComponent<clsStepControle>().NextStep();
        }
    }

    public void ReadyStep()
    {
        m_ready = true;
    }

    public void PrevStep()
    {
        m_stepPrev = true;        
    }

    public void NextStep()
    {
        m_stepNext = true;        
    }
}
