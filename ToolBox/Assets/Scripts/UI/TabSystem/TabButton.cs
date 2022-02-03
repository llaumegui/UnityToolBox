using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup Group;

    [HideInInspector]public Image Background;

    public UnityEvent OnSelectedMethod;
    public UnityEvent OnDeselectedMethod;

    private void Start()
    {
        Background = GetComponent<Image>();
        Group.Subscribe(this);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Group.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Group.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Group.OnTabExit(this);
    }

    public void Select()
    {
        OnSelectedMethod.Invoke();
    }

    public void Deselect()
    {
        OnDeselectedMethod.Invoke();
    }
}
