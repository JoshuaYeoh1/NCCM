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

    // clone as key, prefab as value
    public Dictionary<GameObject, GameObject> activeAnomalies = new();
    public int maxActiveAnomalies=1;

    public Vector2 spawnTime = new Vector2(20, 30);

    void OnEnable()
    {
        EventManager.Current.ClockInEvent += OnClockIn;
        EventManager.Current.AnomalyDespawnEvent += OnAnomalyDespawn;
    }
    void OnDisable()
    {
        EventManager.Current.ClockInEvent -= OnClockIn;
        EventManager.Current.AnomalyDespawnEvent -= OnAnomalyDespawn;
    }
    
    void OnClockIn()
    {
        StartCoroutine(SpawningAnomaly());
    }

    IEnumerator SpawningAnomaly()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(spawnTime.x, spawnTime.y));

            if(anomalies.Count>0)
            {
                if(activeAnomalies.Count < maxActiveAnomalies)
                {
                    GameObject prefab = GetRandomInactivePrefab();

                    SpawnAnomaly(prefab);                
                }
            }
        }
    }

    List<GameObject> inactivePrefabs = new();

    void UpdateInactivePrefabs()
    {
        inactivePrefabs.Clear();

        foreach(GameObject prefab in anomalies)
        {
            if(!activeAnomalies.ContainsValue(prefab))
            {
                inactivePrefabs.Add(prefab);
            }
        }
    }

    GameObject GetRandomInactivePrefab()
    {
        UpdateInactivePrefabs();

        if(inactivePrefabs.Count>0)
        {
            return inactivePrefabs[Random.Range(0, inactivePrefabs.Count)];
        }
        return null;
    }

    void SpawnAnomaly(GameObject prefab)
    {
        if(!prefab) return;

        Room room = RoomManager.Current.GetRandomRoom();

        Transform spot = RoomManager.Current.GetRandomSpot(room);

        if(room==null || !spot) return;

        GameObject spawned = Instantiate(prefab);

        EventManager.Current.OnAnomalySpawn(spawned, room, spot);

        activeAnomalies[spawned] = prefab;
    }

    void OnAnomalyDespawn(GameObject anomaly)
    {
        if(!activeAnomalies.ContainsKey(anomaly)) return;

        activeAnomalies.Remove(anomaly);
    } 
}
