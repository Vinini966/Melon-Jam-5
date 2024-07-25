using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialUpConnector : MonoBehaviour
{
    public static bool Unpluged;

    public OnClick OnClick;
    public Rigidbody Rigidbody;

    Vector3 _startPos;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void Unplug()
    {
        Unpluged = true;
        OnClick.EnableGlow = true;
        OnClick.OnObjectClicked.AddListener(PlugIn);
        Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void PlugIn()
    {
        Unpluged = false;

        OnClick.OnPointerExit(null);
        OnClick.EnableGlow = false;
        OnClick.OnObjectClicked.RemoveListener(PlugIn);
        
        Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        transform.DOMove(_startPos, 0.5f);
    }
}
