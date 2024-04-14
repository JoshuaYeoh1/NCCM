using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AnnOS : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.Current.ClickEvent += OnClick;
        EventManager.Current.ChangeCameraEvent += OnChangeCamera;
    }
    void OnDisable()
    {
        EventManager.Current.ClickEvent -= OnClick;
        EventManager.Current.ChangeCameraEvent -= OnChangeCamera;
    }
    
    public GameObject backBtn;

    [Header("Cam Screen")]
    public GameObject camScreen;
    public GameObject[] camBtns;
    public GameObject[] camViews;
    public int currentCam=1;
    public GameObject reportBtn;

    [Header("Report Screen")]
    public GameObject reportScreen;
    public GameObject reportBackBtn;
    public GameObject hunterBtn;
    public GameObject shamanBtn;
    public GameObject priestBtn;

    void Update()
    {
        CheckExorcistManager();
    }

    void OnClick(GameObject target)
    {
        if(!LevelManager.Current.annOSView)
        {
            if(target==gameObject)
            {
                ToggleAnnOSView(true);

                AudioManager.Current.PlaySFX(SFXManager.Current.sfxPcBtn, transform.position);
            }
        }
        else
        {
            if(target==backBtn)
            {
                ToggleAnnOSView(false);

                AudioManager.Current.PlaySFX(SFXManager.Current.sfxPcBtn, transform.position);
            }

            if(camScreen.activeSelf)
            {
                for(int i=0; i<camBtns.Length; i++)
                {
                    if(target==camBtns[i])
                    {
                        if(currentCam != i+1)
                        {
                            EventManager.Current.OnChangeCamera(i);

                            AudioManager.Current.PlaySFX(SFXManager.Current.sfxPcBtn, transform.position);
                        }
                    }
                }

                if(target==reportBtn)
                {
                    SwitchScreen("Report Screen");

                    AudioManager.Current.PlaySFX(SFXManager.Current.sfxPcBtn, transform.position);
                }
            }
            else if(reportScreen.activeSelf)
            {
                if(target==reportBackBtn)
                {
                    SwitchScreen("Camera Screen");

                    AudioManager.Current.PlaySFX(SFXManager.Current.sfxPcBtn, transform.position);
                }
                else if(target==hunterBtn)
                {
                    SwitchScreen("Camera Screen");

                    EventManager.Current.OnReport(ExorcistType.Hunter);

                    AudioManager.Current.PlaySFX(SFXManager.Current.sfxPcBtn, transform.position);

                    AudioManager.Current.PlaySFX(SFXManager.Current.sfxReportBtn, transform.position);
                }
                else if(target==shamanBtn)
                {
                    SwitchScreen("Camera Screen");

                    EventManager.Current.OnReport(ExorcistType.Shaman);

                    AudioManager.Current.PlaySFX(SFXManager.Current.sfxPcBtn, transform.position);

                    AudioManager.Current.PlaySFX(SFXManager.Current.sfxReportBtn, transform.position);
                }
                else if(target==priestBtn)
                {
                    SwitchScreen("Camera Screen");

                    EventManager.Current.OnReport(ExorcistType.Priest);

                    AudioManager.Current.PlaySFX(SFXManager.Current.sfxPcBtn, transform.position);

                    AudioManager.Current.PlaySFX(SFXManager.Current.sfxReportBtn, transform.position);
                }
            }
            
        }
    }

    public CinemachineVirtualCamera annOSCam;

    void ToggleAnnOSView(bool toggle)
    {
        if(LevelManager.Current.annOSView!=toggle)
        {
            LevelManager.Current.annOSView=toggle;

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

    void OnChangeCamera(int index)
    {
        if(!camScreen.activeSelf) return;

        if(currentCam==index+1) return;

        for(int i=0; i<camViews.Length; i++)
        {
            if(i==index)
            {
                camViews[i].SetActive(true);

                currentCam = index+1;
            }
            else camViews[i].SetActive(false);
        }
        
        AudioManager.Current.PlaySFX(SFXManager.Current.sfxCamBtn, transform.position);
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

    void CheckExorcistManager()
    {
        reportBtn.SetActive(!ExorcistManager.Current.busy);

        hunterBtn.SetActive(!ExorcistManager.Current.busy && ExorcistManager.Current.hunterAlive);
        shamanBtn.SetActive(!ExorcistManager.Current.busy && ExorcistManager.Current.shamanAlive);
        priestBtn.SetActive(!ExorcistManager.Current.busy && ExorcistManager.Current.priestAlive);
    }
}
