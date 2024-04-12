using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalySpawner : MonoBehaviour
{
    public static AnomalySpawner Current;

    void Awake()
    {
        Current=this;        
    }
        
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public List<GameObject> anomalies = new();

    public List<GameObject> activeAnomalies = new();
    public int maxActiveAnomalies=1;

    public Vector2 spawnTime = new Vector2(20, 30);

    void OnEnable()
    {
        EventManager.Current.ClockInEvent += OnClockIn;
    }
    void OnDisable()
    {
        EventManager.Current.ClockInEvent -= OnClockIn;
    }
    
    void OnClockIn()
    {
        StartCoroutine(SpawningAnomaly());
    }

    IEnumerator SpawningAnomaly()
    {
        while(true)
        {
            GameObject prefab=null;

            if(anomalies.Count>0)
            {
                prefab = anomalies[Random.Range(0, anomalies.Count)];
            }

            yield return new WaitForSeconds(Random.Range(spawnTime.x, spawnTime.y));

            if(activeAnomalies.Count < maxActiveAnomalies)
            {
                SpawnAnomaly(prefab);                
            }
        }
    }

    void SpawnAnomaly(GameObject prefab)
    {
        if(!prefab) return;

        Room room = RoomManager.Current.GetRandomRoom();

        Transform spot = RoomManager.Current.GetRandomSpot(room);

        if(!spot) return;

        GameObject spawned = Instantiate(prefab);

        EventManager.Current.OnAnomalySpawn(spawned, room, spot);
    }
}
