using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutter : MonoBehaviour
{
    public bool isClosed;

    public Vector3 openPos;
    public Vector3 closePos;

    public float animTime=.5f;

    void OnEnable()
    {
        EventManager.Current.ShutterActivateEvent += OnShutterActivate;
    }
    void OnDisable()
    {
        EventManager.Current.ShutterActivateEvent -= OnShutterActivate;
    }
    
    void OnShutterActivate()
    {
        LeanTween.cancel(gameObject);

        if(isClosed)
        {
            isClosed=false;
            LeanTween.move(gameObject, openPos, animTime).setEaseInOutSine();
        }
        else
        {
            isClosed=true;
            LeanTween.move(gameObject, closePos, animTime).setEaseInOutSine();
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
    
    
}
