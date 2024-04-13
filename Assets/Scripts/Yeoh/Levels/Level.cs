using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Current;

    void Awake()
    {
        if(!Current) Current=this;
        else Destroy(gameObject);
    }

    ////////////////////////////////////////////////////////////////////////////////////

    public bool canView=true;

    public void ViewCooldown(float time=.5f)
    {
        canView=false;
        Invoke(nameof(EnableView), time);
    }
    void EnableView()
    {
        canView=true;
    }

    public bool annOSView;

    public bool soundPitchActive;
    public bool flashActive;
    public bool cageActive;
    public bool doorLocked;
    public bool lightsOn=true;
    public bool shutterClosed;
}
