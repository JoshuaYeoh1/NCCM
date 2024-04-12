using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyTeleport : MonoBehaviour
{
    public Anomaly anomaly;

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

                Room room = RoomManager.Current.GetRandomRoom(anomaly.currentRoom);

                Teleport(room);
            }
            else
            {
                if(anomaly.currentRoom!=RoomManager.Current.GetPlayerRoom())
                {
                    Teleport(RoomManager.Current.GetPlayerRoom());

                    EventManager.Current.OnAnomalyReachedWindow(anomaly.gameObject);
                }

                // temp destroy
                Destroy(gameObject, Random.Range(tpTime.x, tpTime.y));
            }
        }
    }

    void Teleport(Room newRoom)
    {
        Transform newSpot = RoomManager.Current.GetRandomSpot(newRoom, anomaly.currentSpot);

        if(!newSpot) return;

        EventManager.Current.OnAnomalyTeleport(anomaly.gameObject, newRoom, newSpot);
    }
}
