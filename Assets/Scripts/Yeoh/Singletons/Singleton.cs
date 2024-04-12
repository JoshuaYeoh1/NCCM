using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Current;

    void Awake()
    {
        if(!Current)
        {
            Current=this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        
        //Invoke("UnlockFPS", .1f); // 45-60FPS FREEZES MY S10 AFTER PLAYING A WHILE

        AwakeValues();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void UnlockFPS()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public bool IsWindows()
    {
        return Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor;
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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

    public float shutterHp=3;
    float shutterHpMax;

    void AwakeValues()
    {
        shutterHpMax=shutterHp;
    }

    public void DamageShutter(float dmg)
    {
        shutterHp-=dmg;

        if(shutterHp<=0)
        {
            EventManager.Current.OnShutterBreak();
        }
    }
    
}