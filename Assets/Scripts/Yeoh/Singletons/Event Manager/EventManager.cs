using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using EasyUI.Toast;

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
    public void OnChangeCamera(int index)
    {
        ChangeCameraEvent?.Invoke(index);

        Debug.Log($"Current Camera is {index+1}");
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

    public event Action<bool> ToggleSoundPitchEvent;
    public event Action<bool> ToggleFlashEvent;
    public event Action<bool> ToggleCageEvent;
    public event Action<bool> ToggleLightsEvent;
    public event Action<bool> ToggleShutterEvent;
    public event Action ShutterBreakEvent;
    
    public void OnToggleSoundPitch(bool toggle)
    {
        ToggleSoundPitchEvent?.Invoke(toggle);
    }
    public void OnToggleFlash(bool toggle)
    {
        ToggleFlashEvent?.Invoke(toggle);
    }
    public void OnToggleCage(bool toggle)
    {
        ToggleCageEvent?.Invoke(toggle);
    }
    public void OnToggleLights(bool toggle)
    {
        ToggleLightsEvent?.Invoke(toggle);
    }
    public void OnToggleShutter(bool toggle)
    {
        ToggleShutterEvent?.Invoke(toggle);
    }
    public void OnShutterBreak()
    {
        ShutterBreakEvent?.Invoke();
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public event Action<GameObject, Room, Transform> AnomalySpawnEvent;
    public event Action<GameObject, Room, Transform> AnomalyTeleportEvent;
    public event Action<GameObject> AnomalyTeleportRandomEvent;
    public event Action<GameObject> AnomalyEnterExposingEvent;
    public event Action<GameObject> AnomalyExitExposingEvent;
    public event Action<GameObject, float> AnomalyAttackEvent;
    public event Action<GameObject> AnomalyJumpscareEvent;
    public event Action<GameObject> AnomalyAlertOtherEvent;
    public event Action<GameObject> AnomalyStunEvent;
    public event Action<GameObject> AnomalyRecoverEvent;
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
    public void OnAnomalyTeleportRandom(GameObject teleportee)
    {
        AnomalyTeleportRandomEvent?.Invoke(teleportee);
    }
    public void OnAnomalyEnterExposing(GameObject anomaly)
    {
        AnomalyEnterExposingEvent?.Invoke(anomaly);
    }
    public void OnAnomalyExitExposing(GameObject anomaly)
    {
        AnomalyExitExposingEvent?.Invoke(anomaly);
    }
    public void OnAnomalyAttack(GameObject attacker, float dmg)
    {
        AnomalyAttackEvent?.Invoke(attacker, dmg);

        Debug.Log($"{attacker.name} attacked");
    }
    public void OnAnomalyJumpscare(GameObject predator)
    {
        AnomalyJumpscareEvent?.Invoke(predator);

        Debug.Log($"{predator.name} JUMPSCARE AHAHAHAHA");
    }
    public void OnAnomalyAlertOther(GameObject alerter)
    {
        AnomalyAlertOtherEvent?.Invoke(alerter);
    }
    public void OnAnomalyStun(GameObject victim)
    {
        AnomalyStunEvent?.Invoke(victim);
    }
    public void OnAnomalyRecover(GameObject subject)
    {
        AnomalyRecoverEvent?.Invoke(subject);
    }
    public void OnAnomalyExpel(GameObject anomaly)
    {
        AnomalyExpelEvent?.Invoke(anomaly);

        string msg = $"<b>{anomaly.name}</b> has <color=green>left</color>";
        Debug.Log(msg);
        Toast.Show(msg, 3, toastColor, ToastPosition.TopCenter);
    }
    public void OnAnomalyDespawn(GameObject anomaly)
    {
        AnomalyDespawnEvent?.Invoke(anomaly);

        Debug.Log($"{anomaly.name} disappeared");
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    public event Action<ExorcistType> ReportEvent;
    public event Action<ExorcistType, GameObject> ExorciseSuccessEvent;
    public event Action<ExorcistType> ExorcistDeathEvent;
    
    public void OnReport(ExorcistType exorcistType)
    {
        ReportEvent?.Invoke(exorcistType);
    }
    public void OnExorciseSuccess(ExorcistType exorcistType, GameObject anomaly)
    {
        ExorciseSuccessEvent?.Invoke(exorcistType, anomaly);

        string msg = $"<b>{exorcistType}</b> <color=green>exorcised</color> <b>{anomaly.name}</b>";
        Debug.Log(msg);
        Toast.Show(msg, 3, toastColor, ToastPosition.TopCenter);
    }
    public void OnExorcistDeath(ExorcistType exorcistType)
    {
        ExorcistDeathEvent?.Invoke(exorcistType);

        string msg = $"<b>Wrong call!</b> {exorcistType} was <color=red>killed</color>";
        Debug.Log(msg);
        Toast.Show(msg, 3, toastColor, ToastPosition.TopCenter);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public event Action<GameObject, float, float> UIBarUpdateEvent;
    public event Action<float> CamStaticEvent;

    public void OnUIBarUpdate(GameObject owner, float value, float valueMax)
    {
        UIBarUpdateEvent?.Invoke(owner, value, valueMax);
    }
    public void OnCamStatic(float staticTime)
    {
        CamStaticEvent?.Invoke(staticTime);
    }
    
    public Color toastColor = new Color(0,0,0,.75f);
}