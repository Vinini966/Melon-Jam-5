using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    enum Directions { LEFT, FRONT, RIGHT}
    Directions _camFacingDirection = Directions.FRONT;

    public float MoveTime;

    public Tween CamMoveSequence;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameEnded)
            return;

        if (_camFacingDirection != Directions.LEFT && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)))
        {
            if(CamMoveSequence == null)
            {
                if (_camFacingDirection == Directions.RIGHT)
                {
                    CamMoveSequence = DOTween.Sequence().Append(transform.DORotate(new Vector3(0, 0, 0), MoveTime/2).SetEase(Ease.InQuad))
                                                        .Append(transform.DORotate(new Vector3(0, -90, 0), MoveTime/2).SetEase(Ease.OutQuad)).OnComplete(() => CamMoveSequence = null);
                }
                else
                {
                    CamMoveSequence = transform.DORotate(new Vector3(0, -90, 0), MoveTime).SetEase(Ease.InOutQuad).OnComplete(() => CamMoveSequence = null);
                }
                _camFacingDirection = Directions.LEFT;
            }
        }
        if (_camFacingDirection != Directions.FRONT && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
        {
            if (CamMoveSequence == null)
            {
                CamMoveSequence = transform.DORotate(new Vector3(0, 0, 0), MoveTime).SetEase(Ease.InOutQuad).OnComplete(() => CamMoveSequence = null);
                _camFacingDirection = Directions.FRONT;
            }
        }
        if (_camFacingDirection != Directions.RIGHT && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)))
        {
            if (CamMoveSequence == null)
            {
                if(_camFacingDirection == Directions.LEFT)
                {
                    CamMoveSequence = DOTween.Sequence().Append(transform.DORotate(new Vector3(0, 0, 0), MoveTime/2).SetEase(Ease.InQuad))
                                                        .Append(transform.DORotate(new Vector3(0, 90, 0), MoveTime/2).SetEase(Ease.OutQuad)).OnComplete(() => CamMoveSequence = null);
                }
                else
                {
                    CamMoveSequence = transform.DORotate(new Vector3(0, 90, 0), MoveTime).SetEase(Ease.InOutQuad).OnComplete(() => CamMoveSequence = null);
                }
                
                _camFacingDirection = Directions.RIGHT;
            }
        }
    }
}
