using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyTeleport : MonoBehaviour
{
    public Anomaly anomaly;

    [HideInInspector] public Room currentRoom;
    [HideInInspector] public Transform currentSpot;

    public Vector2 tpTime = new Vector2(10, 15);
    public Vector2Int tpCount = new Vector2Int(2, 4);

    int tpLeft;

    void Start()
    {
        tpLeft = Random.Range(tpCount.x, tpCount.y);
    }

    void OnEnable()
    {
        StartCoroutine(TeleportingAnomaly());
    }

    IEnumerator TeleportingAnomaly()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(tpTime.x, tpTime.y));

            if(tpLeft>0)
            {
                tpLeft--;

                Room room = RoomManager.Current.GetRandomRoom(currentRoom);

                Teleport(room);
            }
            else
            {
                if(currentRoom!=RoomManager.Current.GetPlayerRoom())
                {
                    Teleport(RoomManager.Current.GetPlayerRoom());
                }

                // temp destroy
                Destroy(gameObject, Random.Range(tpTime.x, tpTime.y));
            }
        }
    }

    void Teleport(Room room)
    {
        Transform newSpot = RoomManager.Current.GetRandomSpot(room);

        if(!newSpot) return;

        currentRoom = room;

        transform.position = newSpot.position;
        transform.rotation = newSpot.rotation;

        RoomManager.Current.occupiedSpots.Remove(currentSpot);

        currentSpot = newSpot;

        anomaly.billboard.faceCamera = room.roomCam.transform;

        Debug.Log($"{anomaly.gameObject.name} teleported to {room.name}");
    }

    void OnDestroy()
    {
        RoomManager.Current.occupiedSpots.Remove(currentSpot);
    }
}
