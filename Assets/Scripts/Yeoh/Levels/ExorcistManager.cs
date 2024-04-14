using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExorcistType
{
    Hunter,
    Shaman,
    Priest,
}

public class ExorcistManager : MonoBehaviour
{
    public static ExorcistManager Current;

    void Awake()
    {
        Current=this;
    }
        
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public bool hunterAlive=true;
    public bool shamanAlive=true;
    public bool priestAlive=true;

    public Vector2 exorciseDelay = new Vector2(5, 7.5f);

    public bool busy;

    public Vector2 cooldownTime = new Vector2(45, 60);

    void OnEnable()
    {
        EventManager.Current.ReportEvent += OnReport;
    }
    void OnDisable()
    {
        EventManager.Current.ReportEvent -= OnReport;
    }
    
    void OnReport(ExorcistType exorcistType)
    {
        if(busy) return;

        if(exorcistType==ExorcistType.Hunter && hunterAlive)
        {
            StartExorcise(ExorcistType.Hunter);
        }
        else if(exorcistType==ExorcistType.Shaman && shamanAlive)
        {
            StartExorcise(ExorcistType.Shaman);
        }
        else if(exorcistType==ExorcistType.Priest && priestAlive)
        {
            StartExorcise(ExorcistType.Priest);
        }
    }

    void StartExorcise(ExorcistType exorcistType)
    {
        busy=true;
        StartCoroutine(ExorciseDelaying(exorcistType));
    }

    IEnumerator ExorciseDelaying(ExorcistType exorcistType)
    {
        yield return new WaitForSeconds(Random.Range(exorciseDelay.x, exorciseDelay.y));
        TryExorcise(exorcistType);
    }

    void TryExorcise(ExorcistType exorcistType)
    {
        CreatureType targetType = CreatureType.Monster;

        if(exorcistType==ExorcistType.Hunter)
        {
            targetType=CreatureType.Monster;
        }
        else if(exorcistType==ExorcistType.Shaman)
        {
            targetType=CreatureType.Anthropoid;
        }
        else if(exorcistType==ExorcistType.Priest)
        {
            targetType=CreatureType.Ghost;
        }

        List<GameObject> correctTypes = new();
        List<GameObject> wrongTypes = new();

        foreach(GameObject anomalyObj in AnomalySpawner.Current.activeAnomalies.Keys)
        {
            Anomaly anomaly = anomalyObj.GetComponent<Anomaly>();

            if(anomaly.creatureType==targetType)
            {
                correctTypes.Add(anomalyObj);
            }
            else
            {
                wrongTypes.Add(anomalyObj);
            }
        }

        if(correctTypes.Count>0)
        {
            GameObject randomTarget = correctTypes[Random.Range(0, correctTypes.Count)];

            EventManager.Current.OnExorciseSuccess(exorcistType, randomTarget);

            EventManager.Current.OnAnomalyDespawn(randomTarget);

            AudioManager.Current.PlaySFX(SFXManager.Current.sfxUIExorcise, transform.position, false, true, Random.Range(-1f, 1f));

            AudioManager.Current.PlaySFX(SFXManager.Current.sfxUIExorcistUpdate, transform.position, false);
        }
        else if(wrongTypes.Count>0)
        {
            if(exorcistType==ExorcistType.Hunter)
            {
                hunterAlive=false;
            }
            else if(exorcistType==ExorcistType.Shaman)
            {
                shamanAlive=false;
            }
            else if(exorcistType==ExorcistType.Priest)
            {
                priestAlive=false;
            }

            EventManager.Current.OnExorcistDeath(exorcistType);

            AudioManager.Current.PlaySFX(SFXManager.Current.sfxExorciseFailScream, transform.position, false, true, Random.Range(-1f, 1f));

            AudioManager.Current.PlaySFX(SFXManager.Current.sfxUIExorcistUpdate, transform.position, false);
        }
        else Debug.Log($"No anomaly found. You prank called the {exorcistType} wtf");

        Invoke(nameof(Cooldown), Random.Range(cooldownTime.x, cooldownTime.y));
    }

    void Cooldown()
    {
        busy=false;
    }
}
