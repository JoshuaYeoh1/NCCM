using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseButton : MonoBehaviour
{
    public enum ButtonType
    {
        SoundPitch,
        Flash,
        Cage,
    }

    public ButtonType buttonType;

    bool canPress=true;

    public float activeTime=5;
    public float cooldownTime=5;

    public Material buttonMat;
    public Color disabledColor;
    Color defColor;

    void Awake()
    {
        defColor = buttonMat.color;
    }

    void Update()
    {
        buttonMat.color = canPress ? defColor : disabledColor;
    }

    void OnEnable()
    {
        EventManager.Current.ClickEvent += OnClick;
    }
    void OnDisable()
    {
        EventManager.Current.ClickEvent -= OnClick;

        buttonMat.color = defColor;
    }
    
    void OnClick(GameObject target)
    {
        if(target!=gameObject) return;

        if(!canPress) return;

        canPress=false;

        if(buttonType==ButtonType.SoundPitch)
        {
            EventManager.Current.OnActivateSoundPitch(activeTime);

            Singleton.Current.soundPitchActive=true;

            Invoke(nameof(DisableSoundPitch), activeTime);
        }
        else if(buttonType==ButtonType.Flash)
        {
            EventManager.Current.OnActivateFlash(activeTime);

            Singleton.Current.flashActive=true;

            Invoke(nameof(DisableFlash), activeTime);
        }
        else if(buttonType==ButtonType.Cage)
        {
            EventManager.Current.OnActivateCage(activeTime);

            Singleton.Current.cageActive=true;

            Invoke(nameof(DisableCage), activeTime);
        }

        Invoke(nameof(Cooldown), activeTime + cooldownTime);
    }

    void DisableSoundPitch()
    {
        Singleton.Current.soundPitchActive=false;
    }
    void DisableFlash()
    {
        Singleton.Current.flashActive=false;
    }
    void DisableCage()
    {
        Singleton.Current.cageActive=false;
    }

    void Cooldown()
    {
        canPress=true;
    }
}
