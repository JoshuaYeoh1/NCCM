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

    [Header("Sound Pitch")]
    public AudioClip[] sfxSoundPitchLoop;

    [Header("Toyol")]
    public AudioClip[] sfxToyolTrollLoop;
    
    [Header("Screams")]
    public AudioClip[] sfxExorciseSuccessScream;
    public AudioClip[] sfxExorciseFailScream;
}
