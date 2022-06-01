using HSVPicker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clsSetColor : MonoBehaviour
{
    public ColorPicker picker;

    private void Awake()
    {
        picker.onValueChanged.AddListener(color =>
        {
            SetColor(transform, color);
        });
    }

    private void SetColor(Transform parentTransform, Color color)
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
                SetColor(_child, color);
            }
        }
    }
}
