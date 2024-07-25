using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DownloadBar : MonoBehaviour
{
    public static float Modifier = 1;
    public float TotalTime = 60;

    public static Action FileDownloaded;

    public Slider DownloadProgressBar;
    public TMP_Text TimeLeft;

    float _timer = 0;
    float _update = 0;

    private void Start()
    {
        DownloadProgressBar.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float TotalMod = CalculateTotalMod();

        _timer += Time.deltaTime * TotalMod * NetworkTraffic.NetworkTrafficMod;
        _update += Time.deltaTime;

        DownloadProgressBar.value = _timer / TotalTime;

        if(_update > 1)
        {
            ConvertSeconds((TotalTime / TotalMod / NetworkTraffic.NetworkTrafficMod) - _timer);
            _update = 0;
        }
        

        if(_timer >= TotalTime)
        {
            FileDownloaded?.Invoke();
        }
    }

    void ConvertSeconds(float totalSeconds)
    {
        float TotalMod = CalculateTotalMod();

        if (GameManager.GameEnded)
        {
            TimeLeft.text = "Completed!";
            return;
        }

        if(TotalMod <= 0)
        {
            TimeLeft.text = "Interrupted...";
            return;
        }

        int days = Mathf.FloorToInt(totalSeconds / (24 * 3600));
        totalSeconds %= 24 * 3600;
        long hours = Mathf.FloorToInt(totalSeconds / 3600);
        totalSeconds %= 3600;
        long minutes = Mathf.FloorToInt(totalSeconds / 60);
        long seconds = Mathf.FloorToInt(totalSeconds % 60);

        string timeLeft = "Time Left: ";
        if (days > 0)
            timeLeft += $"{days} Days, ";
        if (hours > 0)
            timeLeft += $"{hours} Hours, ";
        if (minutes > 0)
            timeLeft += $"{minutes} Minutes, ";

        timeLeft += $"{seconds} Seconds";

        TimeLeft.text = timeLeft;
    }

    float CalculateTotalMod()
    {
        float TotalMod = Mathf.Clamp(Modifier - AntiVirus.TotalNegitiveMod, 0.01f, 1);
        TotalMod = DialUpConnector.Unpluged ? 0 : TotalMod;
        TotalMod = Phone.Unhooked ? 0 : TotalMod;

        return TotalMod;
    }
}
