using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatManager : MonoBehaviour
{
    public float Speed;

    public Sprite[] CatSprites;

    public DialUpConnector DialUpConnector;

    public AudioClip[] Meows;
    public AudioSource AudioSource;

    Vector3 _startPos;
    bool _runningAway = false;
    float _approchTimer;
    float _knockoffTimer = 1;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        _approchTimer = Random.Range(2.5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!DialUpConnector.Unpluged && !GameManager.GameEnded)
        {
            _approchTimer -= Time.deltaTime;
            if (transform.localPosition.z >= 2.1f)
            {
                GetComponent<SpriteRenderer>().sprite = CatSprites[1];
                if (_knockoffTimer <= 0)
                {
                    //knock out plug
                    DialUpConnector.Unplug();
                    
                    ShooCat();
                }
                else
                {
                    _knockoffTimer -= Time.deltaTime;
                }
            }
            else if (_approchTimer <= 0 && !_runningAway)
            {
                Vector3 position = transform.position;
                position.z += Speed * Time.deltaTime;
                transform.position = position;
            }
        }
        
    }

    public void ShooCat()
    {
        if (_runningAway)
            return;

        if(Random.Range(0, 100) == 0)
        {
            AudioSource.PlayOneShot(Meows.Last());
        }
        else
        {
            AudioSource.PlayOneShot(Meows[Random.Range(0, Meows.Length - 1)]);
        }

        GetComponent<SpriteRenderer>().sprite = CatSprites[0];
        _runningAway = true;
        DOTween.Sequence().Append(transform.DORotate(new Vector3(0, -90, 6.431f), 0.5f))
                          .Append(transform.DOMove(_startPos, 1))
                          .Append(transform.DORotate(new Vector3(0, 90, 6.431f), 0.5f))
                          .OnComplete(() =>
                          {
                              _runningAway = false;
                              _approchTimer = Random.Range(2.5f, 15f);
                              _knockoffTimer = 1;
                          });
    }
}
