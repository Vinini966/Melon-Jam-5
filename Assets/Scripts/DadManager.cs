using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadManager : MonoBehaviour
{
    public Phone Phone;

    public AudioSource AskPhone;

    Vector3 _startPos;
    bool _runningAway = false;
    bool _triggered = false;
    float _approchTimer;
    float _knockoffTimer = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        _approchTimer = Random.Range(10f, 15f);
        AskPhone = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Phone.Unhooked && !GameManager.GameEnded)
        {
            _approchTimer -= Time.deltaTime;
            if (_triggered)
            {
                if (_knockoffTimer <= 0)
                {
                    //knock out plug
                    Phone.Unhook();
                    ShooDad();
                }
                else
                {
                    _knockoffTimer -= Time.deltaTime;
                }
            }
            else if (_approchTimer <= 0 && !_runningAway)
            {
                _approchTimer = 999;
                AskPhone.Play();
                transform.DORotate(new Vector3(0, -90, 0), 0.5f).OnComplete(()=>_triggered = true);
                
            }
        }

    }

    public void ShooDad()
    {
        _runningAway = true;
        DOTween.Sequence().Append(transform.DORotate(new Vector3(0, 0, 0), 0.25f))
                          .Append(transform.DORotate(new Vector3(0, 90, 0), 0.25f))
                          .OnComplete(() =>
                          {
                              _runningAway = false;
                              _triggered = false;
                              _approchTimer = Random.Range(15f, 30f);
                              _knockoffTimer = 1;
                          });
    }
}
