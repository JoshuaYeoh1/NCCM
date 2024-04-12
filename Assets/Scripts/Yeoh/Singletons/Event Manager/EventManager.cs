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
    public event Action<bool> ToggleFirstPersonEvent;
    public event Action<int> ChangeCameraEvent;
    public event Action ClockInEvent;
    public event Action<int> HourTickEvent;

    public void OnClick(GameObject target)
    {
        ClickEvent?.Invoke(target);
    }
    public void OnToggleFirstPerson(bool toggle)
    {
        ToggleFirstPersonEvent?.Invoke(toggle);
    }
    public void OnChangeCamera(int camNumber)
    {
        ChangeCameraEvent?.Invoke(camNumber);
    }
    public void OnClockIn()
    {
        ClockInEvent?.Invoke();

        Debug.Log($"Clocked in, enjoy your shift...");
    }
    public void OnHourTick(int hourNow)
    {
        HourTickEvent?.Invoke(hourNow);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public event Action<float> ActivateSoundPitchEvent;
    public event Action<float> ActivateFlashEvent;
    public event Action<float> ActivateCageEvent;
    public event Action<bool> ToggleLightsEvent;
    public event Action<bool> ShutterActivateEvent;
    public event Action ShutterBreakEvent;
    
    public void OnActivateSoundPitch(float activeTime)
    {
        ActivateSoundPitchEvent?.Invoke(activeTime);
    }
    public void OnActivateFlash(float activeTime)
    {
        ActivateFlashEvent?.Invoke(activeTime);
    }
    public void OnActivateCage(float activeTime)
    {
        ActivateCageEvent?.Invoke(activeTime);
    }
    public void OnToggleLights(bool toggle)
    {
        ToggleLightsEvent?.Invoke(toggle);
    }
    public void OnShutterActivate(bool toggle)
    {
        ShutterActivateEvent?.Invoke(toggle);
    }
    public void OnShutterBreak()
    {
        ShutterBreakEvent?.Invoke();
    }
    

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public event Action<GameObject, Room, Transform> AnomalySpawnEvent;
    public event Action<GameObject, Room, Transform> AnomalyTeleportEvent;
    public event Action<GameObject> AnomalyReachedWindowEvent;
    public event Action<GameObject> AnomalyAttackEvent;
    public event Action<GameObject> AnomalyJumpscareEvent;
    public event Action<GameObject> AnomalyStunEvent;
    public event Action<GameObject> AnomalyExpelEvent;
    public event Action<GameObject> AnomalyDespawnEvent;

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
        AnomalyReachedWindowEvent?.Invoke(anomaly);

        Debug.Log($"{anomaly.name} reached Player Window");
    }
    public void OnAnomalyAttack(GameObject attacker)
    {
        AnomalyAttackEvent?.Invoke(attacker);

        Debug.Log($"{attacker.name} attacked");
    }
    public void OnAnomalyJumpscare(GameObject predator)
    {
        AnomalyJumpscareEvent?.Invoke(predator);

        Debug.Log($"{predator.name} JUMPSCARE AHAHAHAHA");
    }
    public void OnAnomalyStun(GameObject victim)
    {
        AnomalyStunEvent?.Invoke(victim);
    }
    public void OnAnomalyExpel(GameObject victim)
    {
        AnomalyExpelEvent?.Invoke(victim);

        Debug.Log($"{victim.name} got expelled");
    }
    public void OnAnomalyDespawn(GameObject anomaly)
    {
        AnomalyDespawnEvent?.Invoke(anomaly);

        Debug.Log($"{anomaly.name} disappeared");
    }
    
}