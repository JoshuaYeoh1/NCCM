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
    public event Action<int> ChangeCameraEvent;

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
    public void OnChangeCamera(int camNumber)
    {
        ChangeCameraEvent?.Invoke(camNumber);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public event Action<GameObject, Room, Transform> AnomalySpawnEvent;
    public event Action<GameObject, Room, Transform> AnomalyTeleportEvent;
    public event Action AnomalyReachedWindowEvent;

    public void OnAnomalySpawn(GameObject spawned, Room room, Transform spot)
    {
        AnomalySpawnEvent?.Invoke(spawned, room, spot);

        Debug.Log($"{spawned.name} spawned in {room.name}");
    }
    public void OnAnomalyTeleport(GameObject teleportee, Room newRoom, Transform newSpot)
    {
        AnomalyTeleportEvent?.Invoke(teleportee, newRoom, newSpot);

        Debug.Log($"{teleportee.name} teleported to {newRoom.name}");
    }
    public void OnAnomalyReachedWindow(GameObject anomaly)
    {
        AnomalyReachedWindowEvent?.Invoke();

        Debug.Log($"{anomaly.name} reached Player Window");
    }
    
}