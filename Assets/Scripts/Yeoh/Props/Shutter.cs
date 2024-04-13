using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutter : MonoBehaviour
{
    public Vector3 openPos;
    public Vector3 closePos;

    public float animTime=.5f;

    public float shutterHp=3;
    float shutterHpMax;

    void Awake()
    {
        shutterHpMax=shutterHp;
    }

    void OnEnable()
    {
        EventManager.Current.ToggleShutterEvent += OnToggleShutter;
        EventManager.Current.AnomalyAttackEvent += OnAnomalyAttack;
        EventManager.Current.ShutterBreakEvent += OnShutterBreak;
    }
    void OnDisable()
    {
        EventManager.Current.ToggleShutterEvent -= OnToggleShutter;
        EventManager.Current.AnomalyAttackEvent -= OnAnomalyAttack;
        EventManager.Current.ShutterBreakEvent -= OnShutterBreak;
    }
    
    void Start()
    {
        OnToggleShutter(LevelManager.Current.shutterClosed);
    }
    
    void OnToggleShutter(bool toggle)
    {
        if(shutterHp<=0) return;

        LevelManager.Current.shutterClosed = toggle;

        LeanTween.cancel(gameObject);

        if(toggle)
        {
            LeanTween.move(gameObject, closePos, animTime).setEaseInOutSine();
        }
        else
        {
            LeanTween.move(gameObject, openPos, animTime).setEaseInOutSine();
        }
    }

    [ContextMenu("Record Open Position")]
    void RecordOpenPos()
    {
        openPos = transform.position;
    }
    [ContextMenu("Record Close Position")]
    void RecordClosePos()
    {
        closePos = transform.position;
    }
    
    [ContextMenu("Go to Open Position")]
    void GoToOpenPos()
    {
        transform.position = openPos;
    }
    [ContextMenu("Go to Close Position")]
    void GoToClosePos()
    {
        transform.position = closePos;
    }

    public void OnAnomalyAttack(GameObject attacker, float dmg)
    {
        if(dmg<=0) return;

        shutterHp-=dmg;

        if(shutterHp<=0)
        {
            shutterHp=0;

            EventManager.Current.OnShutterBreak();
        }

        ModelManager.Current.FlashColor(gameObject, .5f, -.5f, -.5f);

        CameraManager.Current.Shake();

        EventManager.Current.OnUIBarUpdate(LevelManager.Current.gameObject, shutterHp, shutterHpMax);

        EventManager.Current.OnAnomalyExpel(attacker);
    }

    void OnShutterBreak()
    {
        LevelManager.Current.shutterClosed = false;

        Destroy(gameObject);
    }
}
