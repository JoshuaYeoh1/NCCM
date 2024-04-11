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
    public int maxActiveAnomalies=2;

    public Vector2 spawnTime = new Vector2(20, 30);

    void OnEnable()
    {
        StartCoroutine(SpawningAnomaly());
    }

    IEnumerator SpawningAnomaly()
    {
        while(true)
        {
            GameObject prefab = anomalies[Random.Range(0, anomalies.Count)];

            yield return new WaitForSeconds(Random.Range(spawnTime.x, spawnTime.y));

            if(activeAnomalies.Count < maxActiveAnomalies)
            {
                SpawnAnomaly(prefab);                
            }
        }
    }

    void SpawnAnomaly(GameObject prefab)
    {
        Room room = RoomManager.Current.GetRandomRoom();

        Transform spot = RoomManager.Current.GetRandomSpot(room);

        if(!spot) return;

        GameObject spawned = Instantiate(prefab);

        Anomaly anomaly = spawned.GetComponent<Anomaly>();

        anomaly.tp.currentRoom = room;

        spawned.transform.position = spot.position;
        spawned.transform.rotation = spot.rotation;

        anomaly.tp.currentSpot = spot;

        anomaly.billboard.faceCamera = room.roomCam.transform;

        Debug.Log($"Spawned {spawned.name} in {room.name}");
    }
}
