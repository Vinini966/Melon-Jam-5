using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float OverAllTime = 0;
    public static bool GameEnded = false;

    public Camera MainCamera;
    public Camera ScreenCamera;
    public Canvas EndScreen;
    public RenderTexture ScreenTexture;
    public TMP_Text TotalTime;
    bool _mainCamActive = true;

    // Start is called before the first frame update
    void Start()
    {
        DownloadBar.FileDownloaded += GameEnd;
        ScreenTexture = ScreenCamera.targetTexture;
        EndScreen.gameObject.SetActive(false);
        GameEnded = false;
        OverAllTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameEnded)
        {
            OverAllTime += Time.deltaTime;
        }
        else
        {
            if (!_mainCamActive)
                SwapCamera();

            ConvertSeconds(OverAllTime);
            EndScreen.gameObject.SetActive(true);
        }
        if (!_mainCamActive)
        {
            if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                SwapCamera();
            }
        }
    }

    void GameEnd()
    {
        GameEnded = true;
    }

    public void SwapCamera()
    {
        if (_mainCamActive)
        {
            ScreenCamera.targetTexture = null;
            MainCamera.gameObject.SetActive(false);
            _mainCamActive = false;
        }
        else
        {
            MainCamera.gameObject.SetActive(true);
            ScreenCamera.targetTexture = ScreenTexture;
            _mainCamActive = true;
        }
    }

    void ConvertSeconds(float totalSeconds)
    {

        int days = Mathf.FloorToInt(totalSeconds / (24 * 3600));
        totalSeconds %= 24 * 3600;
        long hours = Mathf.FloorToInt(totalSeconds / 3600);
        totalSeconds %= 3600;
        long minutes = Mathf.FloorToInt(totalSeconds / 60);
        long seconds = Mathf.FloorToInt(totalSeconds % 60);

        string timeLeft = "";
        if (days > 0)
            timeLeft += $"{days} Days, ";
        if (hours > 0)
            timeLeft += $"{hours} Hours, ";
        if (minutes > 0)
            timeLeft += $"{minutes} Minutes, ";

        timeLeft += $"{seconds} Seconds";

        TotalTime.text = timeLeft;
    }

    public void PlayAgain()
    {
        SceneChange.i.sceneToChange = "Game";
        SceneChange.i.StartChange();
    }

    public void MainMenu()
    {
        SceneChange.i.sceneToChange = "Main Menu";
        SceneChange.i.StartChange();
    }
}
