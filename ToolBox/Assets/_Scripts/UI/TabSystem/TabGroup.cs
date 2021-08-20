using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    List<TabButton> TabButtons;

    [Header("Colors")]
    public Color TabIdle;
    public Color TabHover;
    public Color TabSelected;

    TabButton _selectedTab;

    [Space]
    public List<GameObject> ObjectsToSwap;

    public void Subscribe(TabButton button)
    {
        if (TabButtons == null)
            TabButtons = new List<TabButton>();

        TabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();

        if (_selectedTab != button)
            button.Background.color = TabHover;
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        if(_selectedTab!=null)
        {
            _selectedTab.Deselect();
        }

        _selectedTab = button;
        _selectedTab.Select();

        ResetTabs();

        button.Background.color = TabSelected;

        int index = button.transform.GetSiblingIndex();
        for(int i =0;i<ObjectsToSwap.Count;i++)
        {
            if(i==index)
            {
                ObjectsToSwap[i].SetActive(true);
            }
            else
            {
                ObjectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach(TabButton button in TabButtons)
        {
            if (_selectedTab == button)
                continue;

            button.Background.color = TabIdle;
        }
    }
}
