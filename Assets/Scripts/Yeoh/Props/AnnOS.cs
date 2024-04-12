using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AnnOS : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.Current.ClickEvent += OnClick;
    }
    void OnDisable()
    {
        EventManager.Current.ClickEvent -= OnClick;
    }
    
    public GameObject backBtn;

    [Header("Cam Screen")]
    public GameObject camScreen;
    public GameObject[] camBtns;
    public GameObject[] camViews;
    public GameObject reportBtn;

    [Header("Report Screen")]
    public GameObject reportScreen;
    public GameObject reportBackBtn;
    public GameObject hunterBtn;
    public GameObject shamanBtn;
    public GameObject priestBtn;

    void OnClick(GameObject target)
    {
        if(!Singleton.Current.annOSView)
        {
            if(target==gameObject)
            {
                ToggleAnnOSView(true);
            }
        }
        else
        {
            if(target==backBtn)
            {
                ToggleAnnOSView(false);
            }

            if(camScreen.activeSelf)
            {
                for(int i=0; i<camBtns.Length; i++)
                {
                    if(target==camBtns[i])
                    {
                        SwitchCamView(i);
                    }
                }

                if(target==reportBtn)
                {
                    SwitchScreen("Report Screen");
                }
            }
            else if(reportScreen.activeSelf)
            {
                if(target==reportBackBtn)
                {
                    SwitchScreen("Camera Screen");
                }
                else if(target==hunterBtn)
                {
                    SwitchScreen("Camera Screen");
                }
                else if(target==shamanBtn)
                {
                    SwitchScreen("Camera Screen");
                }
                else if(target==priestBtn)
                {
                    SwitchScreen("Camera Screen");
                }
            }
            
        }
    }

    public CinemachineVirtualCamera annOSCam;

    void ToggleAnnOSView(bool toggle)
    {
        if(Singleton.Current.annOSView!=toggle)
        {
            Singleton.Current.annOSView=toggle;

            EventManager.Current.OnToggleFirstPerson(!toggle);

            if(toggle)
            {
                CameraManager.Current.ChangeCamera(annOSCam);
            }
            else
            {
                CameraManager.Current.ChangeCameraToDefault();
            }
        }
    }

    int currentCam=1;

    void SwitchCamView(int num)
    {
        for(int i=0; i<camViews.Length; i++)
        {
            if(i==num)
            {
                camViews[i].SetActive(true);

                currentCam=num+1;

                EventManager.Current.OnChangeCamera(num+1);
            }
            else camViews[i].SetActive(false);
        }
    }

    void SwitchScreen(string type)
    {
        if(type=="Camera Screen")
        {
            camScreen.SetActive(true);
            reportScreen.SetActive(false);
        }
        else if(type=="Report Screen")
        {
            reportScreen.SetActive(true);
            camScreen.SetActive(false);
        }
    }
}
