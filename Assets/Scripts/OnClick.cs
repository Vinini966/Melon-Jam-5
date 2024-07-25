using HighlightPlus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnClick : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler,IPointerExitHandler
{
    public bool EnableGlow;
    [SerializeField]
    public UnityEvent OnObjectClicked;
    HighlightEffect _highlightEffect;

    private void Start()
    {
        _highlightEffect = GetComponent<HighlightEffect>();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        OnObjectClicked?.Invoke();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if(_highlightEffect != null && EnableGlow)
        {
            _highlightEffect.highlighted = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_highlightEffect != null && EnableGlow)
        {
            _highlightEffect.highlighted = false;
        }
    }
}
