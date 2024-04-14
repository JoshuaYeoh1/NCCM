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

        InvokeRepeating(nameof(CheckAnomaliesAtWindow), 1, .5f);
    }

    ////////////////////////////////////////////////////////////////////////////////////

    void OnEnable()
    {
        EventManager.Current.AnomalyTeleportEvent += OnAnomalyTeleport;
        EventManager.Current.AnomalyReachedWindowEvent += OnAnomalyReachedWindow;
        EventManager.Current.AnomalyDespawnEvent += OnAnomalyDespawn;
    }
    void OnDisable()
    {
        EventManager.Current.AnomalyTeleportEvent -= OnAnomalyTeleport;
        EventManager.Current.AnomalyReachedWindowEvent -= OnAnomalyReachedWindow;
        EventManager.Current.AnomalyDespawnEvent -= OnAnomalyDespawn;
    }
    
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

    public List<GameObject> anomaliesNearby = new();

    void OnAnomalyTeleport(GameObject teleportee, Room newRoom, Transform newSpot)
    {
        if(newRoom==RoomManager.Current.GetPlayerRoom())
        {
            anomaliesNearby.Add(teleportee);
        }
        else 
        {
            if(anomaliesNearby.Contains(teleportee))
            {
                anomaliesNearby.Remove(teleportee);
            }
        }
    }

    void OnAnomalyReachedWindow(GameObject anomaly)
    {
        AudioManager.Current.PlaySFX(SFXManager.Current.sfxUIAppearAtWindow, anomaly.transform.position, false);
    }

    void OnAnomalyDespawn(GameObject anomaly)
    {
        if(anomaliesNearby.Contains(anomaly))
        {
            anomaliesNearby.Remove(anomaly);
        }
    }
    
    void CheckAnomaliesAtWindow()
    {
        if(anomaliesNearby.Count>0)
        {
            MusicManager.Current.ChangeLayer(1);
        }
        else
        {
            MusicManager.Current.ChangeLayer(0);
        }
    }
}
