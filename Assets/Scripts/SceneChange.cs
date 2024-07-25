using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

public class SceneChange : MonoBehaviour
{
    public static SceneChange i;
    public Image slider;
    [Range(0.0f, 1.0f)]
    public float timer;
    public bool started;
    public bool GO = false;
    public string sceneToChange;
    AsyncOperation comp;

    // Start is called before the first frame update
    void Start()
    {
        if (i == null)
        {
            DontDestroyOnLoad(this.gameObject);
            i = this;
        }
        slider = gameObject.transform.GetChild(0).GetComponent<Image>();
        slider.color = new Color(0f, 0f, 0f, 0f);
        slider.gameObject.SetActive(false);
    }
    public static SceneChange Instance()
    {
        return i;
    }

    public void StartChange()
    {
        if (!started)
        {
            started = true;
            gameObject.SetActive(true);
            slider.gameObject.SetActive(true);
            slider.DOColor(new Color(0, 0, 0, 1), timer).OnComplete(() =>
            {
                comp = SceneManager.LoadSceneAsync(sceneToChange);
                comp.completed += Undim;
            });

        }  
    }

    private void Undim(AsyncOperation obj)
    {
        
        comp.completed -= Undim;
        comp = null;
        slider.DOColor(new Color(0, 0, 0, 0), timer).OnComplete(() =>
        {
            slider.gameObject.SetActive(false);
            started = false;
        });
    }
}
