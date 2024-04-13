using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPitchSpeaker : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.Current.ToggleSoundPitchEvent += OnToggleSoundPitch;
    }
    void OnDisable()
    {
        EventManager.Current.ToggleSoundPitchEvent -= OnToggleSoundPitch;
    }

    AudioSource soundPitchLoop;
    
    void OnToggleSoundPitch(bool toggle)
    {
        if(toggle)
        {
            soundPitchLoop = AudioManager.Current.LoopSFX(gameObject, SFXManager.Current.sfxSoundPitchLoop, false);
        }
        else
        {
            if(soundPitchLoop) AudioManager.Current.StopLoop(soundPitchLoop);
        }
    }
}
