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

    public AudioClip[] sfxSoundPitchLoop;
    public AudioClip[] sfxToyolTrollLoop;
    public AudioClip[] sfxExorciseSuccessScream;
    public AudioClip[] sfxExorciseFailScream;
    public AudioClip[] sfxStaticLoop;
    public AudioClip[] sfxJumpscare;

}
