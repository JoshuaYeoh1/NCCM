using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamStatic : MonoBehaviour
{
    public GameObject[] staticCanvases;

    public Vector2 staticTime = new Vector2(.1f, .2f);
    float currentStaticTime;

    void OnEnable()
    {
        EventManager.Current.ChangeCameraEvent += OnChangeCamera;
        EventManager.Current.AnomalySpawnEvent += OnAnomalySpawn;
        EventManager.Current.AnomalyTeleportEvent += OnAnomalyTeleport;
        EventManager.Current.AnomalyExpelEvent += OnAnomalyExpel;
        EventManager.Current.AnomalyDespawnEvent += OnAnomalyDespawn;
        EventManager.Current.ExorcistDeathEvent += OnExorcistDeath;

        EventManager.Current.CamStaticEvent += OnCamStatic;
    }
    void OnDisable()
    {
        EventManager.Current.ChangeCameraEvent -= OnChangeCamera;
        EventManager.Current.AnomalySpawnEvent -= OnAnomalySpawn;
        EventManager.Current.AnomalyTeleportEvent -= OnAnomalyTeleport;
        EventManager.Current.AnomalyExpelEvent -= OnAnomalyExpel;
        EventManager.Current.AnomalyDespawnEvent -= OnAnomalyDespawn;
        EventManager.Current.ExorcistDeathEvent -= OnExorcistDeath;

        EventManager.Current.CamStaticEvent -= OnCamStatic;
    }

    void OnChangeCamera(int index)
    {
        //EventManager.Current.OnCamStatic(Random.Range(staticTime.x, staticTime.y));
    }

    void OnAnomalySpawn(GameObject spawned, Room room, Transform spot)
    {
        EventManager.Current.OnCamStatic(Random.Range(staticTime.x, staticTime.y));
    }
    
    void OnAnomalyTeleport(GameObject teleportee, Room newRoom, Transform newSpot)
    {
        EventManager.Current.OnCamStatic(Random.Range(staticTime.x, staticTime.y));
    }

    void OnAnomalyExpel(GameObject anomaly)
    {
        EventManager.Current.OnCamStatic(Random.Range(staticTime.x, staticTime.y));
    }

    void OnAnomalyDespawn(GameObject anomaly)
    {
        EventManager.Current.OnCamStatic(Random.Range(staticTime.x, staticTime.y)*10);
    }

    void OnExorcistDeath(ExorcistType exorcistType)
    {
        EventManager.Current.OnCamStatic(Random.Range(staticTime.x, staticTime.y)*15);
    }

    AudioSource staticLoop;

    void OnCamStatic(float staticTime)
    {
        if(staticTime <= currentStaticTime) return;

        currentStaticTime = staticTime;

        AudioManager.Current.StopLoop(staticLoop);
        CancelInvoke(nameof(DisableStatic));

        EnableStatic();

        Invoke(nameof(DisableStatic), staticTime);
    }

    void EnableStatic()
    {
        foreach(GameObject staticc in staticCanvases)
        {
            staticc.SetActive(true);
        }

        staticLoop = AudioManager.Current.LoopSFX(gameObject, SFXManager.Current.sfxStaticLoop, true);
    }

    void DisableStatic()
    {
        foreach(GameObject staticc in staticCanvases)
        {
            staticc.SetActive(false);
        }

        AudioManager.Current.StopLoop(staticLoop);

        currentStaticTime=0;
    }
}
