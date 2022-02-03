using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public abstract class Button : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler,IPointerDownHandler,IPointerUpHandler
{
    [Header("Colors")]
    public Color IdleColor;
    public Color HoverColor;
    public Color SelectedColor;

    protected Image _background;

    void Start()
    {
        _background = GetComponent<Image>();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
