using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toyol : MonoBehaviour
{
    Anomaly anomaly;

    void Awake()
    {
        anomaly=GetComponent<Anomaly>();
    }

    void OnEnable()
    {
        EventManager.Current.AnomalyJumpscareEvent += OnAnomalyJumpscare;
    }
    void OnDisable()
    {
        EventManager.Current.AnomalyDespawnEvent -= OnAnomalyJumpscare;
    }

    void OnAnomalyJumpscare(GameObject predator)
    {
        if(predator!=gameObject) return;

        StartTroll();
    }

    AudioSource trollLoopSound;

    void StartTroll()
    {
        trollLoopSound = AudioManager.Current.LoopSFX(gameObject, SFXManager.Current.sfxToyolTrollLoop, true, false);

        transform.position = RoomManager.Current.toyolSpot.position;
        transform.rotation = RoomManager.Current.toyolSpot.rotation;

        currentTrollTime = Random.Range(trollTime.x, trollTime.y);

        trollingRt = StartCoroutine(Trolling(currentTrollTime));
        trollingLightRt = StartCoroutine(TrollingLight());
        trollingShutterRt = StartCoroutine(TrollingShutter());
        trollingCamRt = StartCoroutine(TrollingCam());
    }

    public Vector2 trollTime = new Vector2(10, 15);
    float currentTrollTime;

    Coroutine trollingRt;
    IEnumerator Trolling(float t)
    {
        yield return new WaitForSeconds(t);
        EndTroll();
    }

    public Vector2 lightTrollInterval = new Vector2(.1f, .5f);

    Coroutine trollingLightRt;
    IEnumerator TrollingLight()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(lightTrollInterval.x, lightTrollInterval.y));
            
            EventManager.Current.OnToggleLights(!LevelManager.Current.lightsOn);
        }
    }

    public Vector2 shutterTrollInterval = new Vector2(.3f, .7f);

    Coroutine trollingShutterRt;
    IEnumerator TrollingShutter()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(shutterTrollInterval.x, shutterTrollInterval.y));
            
            EventManager.Current.OnToggleShutter(!LevelManager.Current.shutterClosed);
        }
    }

    public Vector2 camTrollInterval = new Vector2(.1f, .3f);

    Coroutine trollingCamRt;
    IEnumerator TrollingCam()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(camTrollInterval.x, camTrollInterval.y));
            
            EventManager.Current.OnChangeCamera(Random.Range(0,4));
        }
    }

    void EndTroll()
    {
        AudioManager.Current.StopLoop(trollLoopSound);

        StopCoroutine(trollingLightRt);
        StopCoroutine(trollingShutterRt);
        StopCoroutine(trollingCamRt);

        anomaly.exposure.canJumpscare=false;

        EventManager.Current.OnAnomalyTeleportRandom(gameObject);
        
        //EventManager.Current.OnAnomalyDespawn(predator);
    }
}
