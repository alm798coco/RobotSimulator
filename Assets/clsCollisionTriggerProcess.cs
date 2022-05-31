using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clsCollisionTriggerProcess : MonoBehaviour
{
    private Color m_defaultColor;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root == gameObject.transform.root)
        {
            return;
        }

        Rigidbody _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            return;
        }

        if (_rigidbody.isKinematic)
        {
            return;
        }

        m_defaultColor = collision.transform.GetComponent<Renderer>().material.color;
        ColorChange(collision.transform.root, Color.red);
    }

    private void OnCollisionStay(Collision collision)
    {

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.root == gameObject.transform.root)
        {
            return;
        }

        collision.transform.GetComponent<Renderer>().material.color = m_defaultColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.transform.root == gameObject.transform.root)
        //{
        //    return;
        //}

        //m_defaultColor = other.GetComponent<Renderer>().material.color;
        //other.GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.transform.root == gameObject.transform.root)
        //{
        //    return;
        //}

        //other.GetComponent<Renderer>().material.color = m_defaultColor;
    }

    private void ColorChange(Transform parentTransform, Color color)
    {
        foreach (Transform _child in parentTransform)
        {
            Renderer _renderer = _child.GetComponent<Renderer>();
            if (_renderer != null)
            {
                _renderer.material.color = color;
            }

            if (_child.childCount > 0)
            {
                ColorChange(_child, color);
            }
        }
    }
}
