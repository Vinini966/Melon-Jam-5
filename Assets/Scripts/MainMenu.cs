using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject Camera;

    Vector3 _creditsPos = new Vector3(-6.25f, 3.14f, -3.23f);
    Vector3 _startPos;

    private void Start()
    {
        _startPos = Camera.transform.position;
    }

    public void Play()
    {
        SceneChange.i.sceneToChange = "Game";
        SceneChange.i.StartChange();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        DOTween.Sequence().Insert(0, Camera.transform.DOMove(_creditsPos, 1))
                          .Insert(0, Camera.transform.DORotate(new Vector3(0, -90, 0), 0.75f));
    }

    public void Return()
    {
        DOTween.Sequence().Insert(0, Camera.transform.DOMove(_startPos, 1))
                          .Insert(0, Camera.transform.DORotate(new Vector3(0, 0, 0), 0.75f));
    }

    public void GoToLink(string link)
    {
        Application.OpenURL(link);
    }
}
