using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public static bool Unhooked = false;


    public Rigidbody Rigidbody;
    public OnClick OnClick;
    public AudioSource BusyTone;

    Vector3 _startPosition;
    Quaternion _startRot;

    private void Start()
    {
        _startPosition = transform.position;
        _startRot = transform.rotation;
    }

    public void Unhook()
    {
        Unhooked = true;
        OnClick.EnableGlow = true;
        OnClick.OnObjectClicked.AddListener(Hangup);
        Rigidbody.constraints = RigidbodyConstraints.None;
        BusyTone.Play();
    }

    public void Hangup()
    {
        Unhooked = false;

        BusyTone.Stop();

        OnClick.OnPointerExit(null);
        OnClick.EnableGlow = false;
        OnClick.OnObjectClicked.RemoveListener(Hangup);

        Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        DOTween.Sequence().Insert(0, transform.DORotateQuaternion(_startRot, 0.5f))
                          .Insert(0, transform.DOMove(_startPosition, 0.5f));
    }
}
