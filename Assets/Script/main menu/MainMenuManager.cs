using Fungus;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    public enum camActive {cam_title, cam_menu, cam_setting};
    public camActive thisCam = camActive.cam_title;
    public GameObject camTitle, camMenu, camSetting;

    public AudioMixer audioMixer;

    [SerializeField]
    public GameObject titleText, panelTutorial, panelCredits, menuGroup;
    public enum panelActive { none, panel_tutorial, panel_credits};
    public panelActive thisPanel = panelActive.none;

    [SerializeField]
    //public GameObject tutorial_page1, tutorial_page2;
    public GameObject[] panelPage;
    public bool lastPage;

    // Start is called before the first frame update
    void Start()
    {
        thisCam = camActive.cam_title;
        thisPanel = panelActive.none;
    }

    // Update is called once per frame
    void Update()
    {
        camControl();
        panelControl();
    }

    void camControl()
    {
        switch (thisCam)
        {
            case camActive.cam_title:
                camTitle.SetActive(true);
                camMenu.SetActive(false);
                camSetting.SetActive(false);
                titleText.SetActive(true);
                break;

            case camActive.cam_menu:
                camTitle.SetActive(false);
                camMenu.SetActive(true);
                camSetting.SetActive(false);
                break;

            case camActive.cam_setting:
                camTitle.SetActive(false);
                camMenu.SetActive(false);
                camSetting.SetActive(true);
                break;
        }
    }

    void panelControl()
    {
        switch (thisPanel)
        {
            case panelActive.none:
                panelCredits.SetActive(false);
                panelTutorial.SetActive(false);
                menuGroup.SetActive(true);
                break;

            case panelActive.panel_credits:
                panelCredits.SetActive(true);
                menuGroup.SetActive(false);
                break;

            case panelActive.panel_tutorial:
                panelTutorial.SetActive(true);
                menuGroup.SetActive(false);
                break;
                
        }
    }

    public void setAudioVolume(float volume)
    {
        audioMixer.SetFloat("MainAudio", volume);
    }

    public void closePanel()
    {
        thisPanel = panelActive.none;
    }

    public void openTutorial(TextMeshProUGUI text)
    {
        thisPanel = panelActive.panel_tutorial;
        panelPage[0].SetActive(true);
        panelPage[1].SetActive(false);
        text.text = "next";
        lastPage = false;
    }

    public void openCredits()
    {
        thisPanel = panelActive.panel_credits;
    }

    public void nextPage(TextMeshProUGUI text)
    {
        if (lastPage == false)
        {
            panelPage[0].SetActive(false);
            panelPage[1].SetActive(true);
            text.text = "close";
            lastPage = true;
        }
        else
        {
            closePanel();
            lastPage = true;
        }
    }

    public void toMenu()
    {
        thisCam = camActive.cam_menu;   
    }

    public void toSetting()
    {
        thisCam = camActive.cam_setting;
    }
}
