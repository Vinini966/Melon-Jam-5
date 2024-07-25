using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupAd : MonoBehaviour
{

    public Sprite[] Ads;

    float _openBandwith;

    // Start is called before the first frame update
    void Start()
    {
        float bandwith = Random.Range(0.01f, 0.3f);
        AntiVirus.TotalNegitiveMod += bandwith;
        _openBandwith = bandwith / 2;
        DownloadBar.Modifier -= _openBandwith;

        GetComponent<Image>().sprite = Ads[Random.Range(0, Ads.Length)];
    }

    public void Close()
    {
        DownloadBar.Modifier += _openBandwith;
        Destroy(gameObject);
    }
}
