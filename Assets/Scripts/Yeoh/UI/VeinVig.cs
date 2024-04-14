using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeinVig : MonoBehaviour
{
    public void PlayBeatSfx()
    {
        AudioManager.Current.PlaySFX(SFXManager.Current.sfxUIHeartbeat, transform.position, false);
    }
}
