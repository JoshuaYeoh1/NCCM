using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutter : MonoBehaviour
{
    public Vector3 openPos;
    public Vector3 closePos;

    public float animTime=.5f;

    void OnEnable()
    {
        EventManager.Current.ShutterActivateEvent += OnShutterActivate;
        EventManager.Current.ShutterBreakEvent += OnShutterBreak;
    }
    void OnDisable()
    {
        EventManager.Current.ShutterActivateEvent -= OnShutterActivate;
        EventManager.Current.ShutterBreakEvent -= OnShutterBreak;
    }

    void Start()
    {
        OnShutterActivate(Singleton.Current.shutterClosed);
    }
    
    void OnShutterActivate(bool toggle)
    {
        if(Singleton.Current.shutterHp<=0) return;

        Singleton.Current.shutterClosed = toggle;

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

    void OnShutterBreak()
    {
        Singleton.Current.shutterClosed = false;

        Destroy(gameObject);
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
    
    
}
