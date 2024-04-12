using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anomaly : MonoBehaviour
{
    public SpriteBillboard billboard;
    
    [HideInInspector] public Room currentRoom;
    [HideInInspector] public Transform currentSpot;

    void OnEnable()
    {
        EventManager.Current.AnomalySpawnEvent += OnAnomalySpawn;
        EventManager.Current.AnomalyTeleportEvent += OnAnomalyTeleport;
    }
    void OnDisable()
    {
        EventManager.Current.AnomalySpawnEvent -= OnAnomalySpawn;
        EventManager.Current.AnomalyTeleportEvent -= OnAnomalyTeleport;
    }

    void Start()
    {
        AnomalySpawner.Current.activeAnomalies.Add(gameObject);
    }
    void OnDestroy()
    {
        AnomalySpawner.Current.activeAnomalies.Remove(gameObject);

        RoomManager.Current.UnoccupySpot(currentSpot);
    }    
    
    void OnAnomalySpawn(GameObject spawned, Room room, Transform spot)
    {
        if(spawned!=gameObject) return;

        OnAnomalyTeleport(gameObject, room, spot);
    }

    void OnAnomalyTeleport(GameObject teleportee, Room newRoom, Transform newSpot)
    {
        if(teleportee!=gameObject) return;
        
        currentRoom = newRoom;

        RoomManager.Current.UnoccupySpot(currentSpot);

        transform.position = newSpot.position;
        transform.rotation = newSpot.rotation;

        currentSpot = newSpot;

        RoomManager.Current.OccupySpot(currentSpot);

        billboard.faceCamera = newRoom.roomCam.transform;
    }
}
