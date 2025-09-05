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

    public float activeTime=6;
    public float cooldownTime=12.5f;

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
            EventManager.Current.OnToggleSoundPitch(true);

            LevelManager.Current.soundPitchActive=true;

            Invoke(nameof(DisableSoundPitch), activeTime);

            AudioManager.Current.PlaySFX(SFXManager.Current.sfxSoundPitchBtn, transform.position);
        }
        else if(buttonType==ButtonType.Flash)
        {
            EventManager.Current.OnToggleFlash(true);

            LevelManager.Current.flashActive=true;

            Invoke(nameof(DisableFlash), activeTime);

            AudioManager.Current.PlaySFX(SFXManager.Current.sfxFlashBtn, transform.position);
        }
        else if(buttonType==ButtonType.Cage)
        {
            EventManager.Current.OnToggleCage(true);

            LevelManager.Current.cageActive=true;

            Invoke(nameof(DisableCage), activeTime);

            AudioManager.Current.PlaySFX(SFXManager.Current.sfxCageBtn, transform.position);
        }

        Invoke(nameof(Cooldown), activeTime + cooldownTime);
    }

    void DisableSoundPitch()
    {
        LevelManager.Current.soundPitchActive=false;

        EventManager.Current.OnToggleSoundPitch(false);
    }
    void DisableFlash()
    {
        LevelManager.Current.flashActive=false;

        EventManager.Current.OnToggleFlash(false);
    }
    void DisableCage()
    {
        LevelManager.Current.cageActive=false;

        EventManager.Current.OnToggleCage(false);
    }

    void Cooldown()
    {
        canPress=true;
    }
}
