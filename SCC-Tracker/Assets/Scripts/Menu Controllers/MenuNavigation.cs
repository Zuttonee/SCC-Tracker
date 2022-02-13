using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class MenuNavigation : MonoBehaviour
{
    public TextMeshProUGUI header;
    public List<NavigationLink> links;

    [HideInInspector] public NavigationLink currentLink;
    [HideInInspector] public NavigationLink previousLink;

    // Start is called before the first frame update
    void Start()
    {
        // Set up main page
        CloseAllPage();
        links[0].page.SetActive(true);
        currentLink = links[0];

        // Set up onclick event on the buttons
        foreach (NavigationLink link in links)
        {
            //link.buttonTitle = link.page.name;
            link.button.GetComponentInChildren<Image>().sprite = link.image;
            link.button.GetComponentInChildren<TextMeshProUGUI>().text = link.buttonTitle;
            link.button.onClick.AddListener(delegate { OpenPage(link.page); });
            link.button.onClick.AddListener(delegate { header.text = link.buttonTitle; });
            link.button.onClick.AddListener(link.onClickedEvent.Invoke);
        }
    }

    #region Close Page Functions
    public void CloseAllPage()
    {
        foreach (NavigationLink link in links)
        {
            link.page.SetActive(false);
        }
    }

    public void ClosePage(string linkName)
    {
        foreach (NavigationLink link in links)
        {
            if (link.buttonTitle == linkName)
            {
                link.OnLeavePageEvent.Invoke();
                link.page.SetActive(false);
            }
        }
    }
    public void ClosePage(GameObject page)
    {
        foreach (NavigationLink link in links)
        {
            if (page == link.page)
            {
                link.OnLeavePageEvent.Invoke();
                page.SetActive(false);
            }
        }
    }
    #endregion

    #region Open Page Functions
    public void OpenPage(string linkName)
    {
        // Prevent opening the same page
        if (currentLink.buttonTitle == linkName) return;

        CloseAllPage();

        foreach (NavigationLink link in links)
        {
            if (link.buttonTitle == linkName)
            {
                link.page.SetActive(true);
                previousLink = currentLink;
                currentLink = link;
            }
        }
    }

    public void OpenPage(GameObject page)
    {
        // Prevent opening the same page
        if (currentLink.page == page) return;

        CloseAllPage();

        foreach (NavigationLink link in links)
        {
            if (page == link.page)
            {
                page.SetActive(true);
                previousLink = currentLink;
                currentLink = link;
            }
        }
    }
    #endregion
}

[Serializable]
public class NavigationLink
{
    public string buttonTitle = "new title";
    public Sprite image;
    [Space(10)]
    public Button button;
    public GameObject page;
    [Space(10)]
    public UnityEvent onClickedEvent;
    public UnityEvent OnLeavePageEvent;
}
