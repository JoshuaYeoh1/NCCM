// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class AnomalyTeleportOld : MonoBehaviour
// {
//     public Anomaly anomaly;

//     public Vector2 moveDelay = new Vector2(10, 15);
//     public Vector2Int moveCycles = new Vector2Int(2, 4);

//     int currentMoveCycle;

//     void Start()
//     {
//         currentMoveCycle = Random.Range(moveCycles.x, moveCycles.y);
//     }

//     void OnEnable()
//     {
//         StartCoroutine(TeleportingAnomaly());
//     }

//     IEnumerator TeleportingAnomaly()
//     {
//         while(true)
//         {
//             yield return new WaitForSeconds(Random.Range(moveDelay.x, moveDelay.y));

//             if(currentMoveCycle>0)
//             {
//                 currentMoveCycle--;

//                 Room room = RoomManager.Current.GetRandomRoom(anomaly.move.currentRoom);

//                 Teleport(room);
//             }
//             else
//             {
//                 if(anomaly.move.currentRoom!=RoomManager.Current.GetPlayerRoom())
//                 {
//                     Teleport(RoomManager.Current.GetPlayerRoom());

//                     EventManager.Current.OnAnomalyReachedWindow(anomaly.gameObject);
//                 }  

//                 //use coroutine bodoh;

//                 // temp destroy
//                 Destroy(gameObject, Random.Range(moveDelay.x, moveDelay.y));

//                 EventManager.Current.OnAnomalyDisappear(anomaly.gameObject);
//             }
//         }
//     }

//     void Teleport(Room newRoom)
//     {
//         Transform newSpot = RoomManager.Current.GetRandomSpot(newRoom, anomaly.move.currentSpot);

//         if(!newSpot) return;

//         EventManager.Current.OnAnomalyTeleport(anomaly.gameObject, newRoom, newSpot);
//     }
// }
