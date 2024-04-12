using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyMove : MonoBehaviour
{
    public SpriteBillboard billboard;

    public Vector2 moveDelay = new Vector2(10, 15);
    public Vector2Int moveCycles = new Vector2Int(2, 4);

    int currentMoveCycle;

    [HideInInspector] public Room currentRoom;
    [HideInInspector] public Transform currentSpot;

    void OnEnable()
    {
        EventManager.Current.AnomalySpawnEvent += OnAnomalySpawn;
        EventManager.Current.AnomalyTeleportEvent += OnAnomalyTeleport;
        EventManager.Current.AnomalyExpelEvent += OnAnomalyExpel;
        EventManager.Current.AnomalyDespawnEvent += OnAnomalyDisappear;
    }
    void OnDisable()
    {
        EventManager.Current.AnomalySpawnEvent -= OnAnomalySpawn;
        EventManager.Current.AnomalyTeleportEvent -= OnAnomalyTeleport;
        EventManager.Current.AnomalyExpelEvent -= OnAnomalyExpel;
        EventManager.Current.AnomalyDespawnEvent -= OnAnomalyDisappear;
    }

    void OnAnomalySpawn(GameObject spawned, Room room, Transform spot)
    {
        if(spawned!=gameObject) return;

        OnAnomalyTeleport(gameObject, room, spot); // starting position
    }

    public void StartPatrol()
    {
        currentMoveCycle = Random.Range(moveCycles.x, moveCycles.y);

        StopPatrol();

        teleportingRt = StartCoroutine(Teleporting());
    }

    public void StopPatrol()
    {
        if(teleportingRt!=null) StopCoroutine(teleportingRt);
    }

    Coroutine teleportingRt;
    
    IEnumerator Teleporting()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(moveDelay.x, moveDelay.y));

            if(currentMoveCycle>0)
            {
                currentMoveCycle--;

                Room room = RoomManager.Current.GetRandomRoom(currentRoom);

                Teleport(room);
            }
            else
            {
                if(!IsAtWindow())
                {
                    Teleport(RoomManager.Current.GetPlayerRoom());

                    EventManager.Current.OnAnomalyReachedWindow(gameObject);
                }  
            }
        }
    }

    void Teleport(Room newRoom)
    {
        Transform newSpot = RoomManager.Current.GetRandomSpot(newRoom, currentSpot);

        if(!newSpot) return;

        EventManager.Current.OnAnomalyTeleport(gameObject, newRoom, newSpot);
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

    void OnAnomalyExpel(GameObject victim)
    {
        if(victim!=gameObject) return;

        Room room = RoomManager.Current.GetRandomRoom(currentRoom);

        Teleport(room);
    }

    void OnAnomalyDisappear(GameObject anomaly)
    {
        if(anomaly!=gameObject) return;

        StopPatrol();

        RoomManager.Current.UnoccupySpot(currentSpot);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public bool IsAtWindow()
    {
        return currentRoom==RoomManager.Current.GetPlayerRoom();
    }
}
