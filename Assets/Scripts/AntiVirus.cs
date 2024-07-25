using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntiVirus : MonoBehaviour
{
    public static float TotalNegitiveMod;

    public Canvas Canvas;

    public GameObject AntivirusWindow;
    public Slider ProgressSlider;

    public GameObject PopupAd;
    public AudioSource PopupSound;

    float _negModRef;

    bool _running = false;

    public float PopupTimerBase = 10;
    [SerializeField]
    float _popupTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        AntivirusWindow.SetActive(false);
    }

    private void Update()
    {
        if (!_running)
        {
            _popupTimer += Time.deltaTime;
            if (_popupTimer >= PopupTimerBase && !GameManager.GameEnded)
            {
                ShowPopup();
                _popupTimer = 0;
                PopupTimerBase = Random.Range(2, 10);
            }
        }
        else
        {
            TotalNegitiveMod -= Time.deltaTime;
            //slider value
            float barValue = 1f - (TotalNegitiveMod / _negModRef);
            ProgressSlider.value = barValue;

            if (TotalNegitiveMod <= 0)
            {
                TotalNegitiveMod = 0;
                _running = false;

                AntivirusWindow.SetActive(false);
            }

            
        }
       
    }

    void ShowPopup()
    {
        PopupSound.Play();
        GameObject go = Instantiate(PopupAd, Canvas.transform);
        RectTransform rectTransform = Canvas.GetComponent<RectTransform>();
        float randX = Random.Range(rectTransform.rect.xMin + 50, rectTransform.rect.xMax - 50);
        float randY = Random.Range(rectTransform.rect.yMin + 75, rectTransform.rect.yMax - 75);
        go.GetComponent<RectTransform>().localPosition = new Vector3(randX, randY, 0);
    }

    public void RunAntiVirus()
    {
        _negModRef = TotalNegitiveMod;
        _running = true;

        AntivirusWindow.SetActive(true);
        ProgressSlider.value = 0;
    }
}
