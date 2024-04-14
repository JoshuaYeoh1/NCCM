using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Current;

    void Awake()
    {
        if(!Current) Current=this;
    }

    ////////////////////////////////////////////////////////////////////////////////////

    [Header("FEAR")]
    public AudioClip[] sfxCage;
    public AudioClip[] sfxCageBreak;
    public AudioClip[] sfxDoor;
    public AudioClip[] sfxUIExpel;
    public AudioClip[] sfxShutter;

    [Header("L4D2")]
    public AudioClip[] sfxCageBtn;
    public AudioClip[] sfxFlashBtn;
    public AudioClip[] sfxLightSwitch;
    public AudioClip[] sfxShutterBreak;
    public AudioClip[] sfxShutterHit;
    public AudioClip[] sfxSoundPitchBtn;

    [Header("Minecraft")]
    public AudioClip[] sfxUIAppearAtWindow;
    public AudioClip[] sfxUIExorcise;

    [Header("PvZ")]
    public AudioClip[] sfxUIPaper;

    [Header("Yeoh")]
    public AudioClip[] sfxSoundPitchLoop;
    public AudioClip[] sfxToyolTrollLoop;
    public AudioClip[] sfxExorciseFailScream;
    public AudioClip[] sfxStaticLoop;
    public AudioClip[] sfxJumpscare;
}
