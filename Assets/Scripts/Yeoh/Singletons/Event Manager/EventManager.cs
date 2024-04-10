using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Current;

    void Awake()
    {
        if(Current!=null && Current!=this)
        {
            Destroy(gameObject);
            return;
        }

        Current = this;
        //DontDestroyOnLoad(gameObject); // Persist across scene changes
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(Current!=this) Destroy(gameObject);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public event Action<GameObject> ClickEvent;
    public event Action<bool> ShutterActivateEvent;
    public event Action<bool> ToggleFirstPersonEvent;
    public event Action<bool> ToggleLightsEvent;

    public void OnClick(GameObject target)
    {
        ClickEvent?.Invoke(target);
    }
    public void OnShutterActivate(bool toggle)
    {
        ShutterActivateEvent?.Invoke(toggle);
    }
    public void OnToggleFirstPerson(bool toggle)
    {
        ToggleFirstPersonEvent?.Invoke(toggle);
    }
    public void OnToggleLights(bool toggle)
    {
        ToggleLightsEvent?.Invoke(toggle);
    }
    
    
}