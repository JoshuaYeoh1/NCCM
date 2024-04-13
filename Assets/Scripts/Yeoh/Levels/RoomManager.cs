using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room
{
    public string name;
    public GameObject roomObj;
    public GameObject roomCam;
    public List<Transform> spawnpoints = new();
}

public class RoomManager : MonoBehaviour
{
    public static RoomManager Current;

    void Awake()
    {
        Current=this;
    }
        
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    public GameObject playerRoomObj;

    public List<Room> rooms = new();

    public List<Transform> occupiedSpots = new();

    public Room GetRandomRoom(Room currentRoom=null)
    {
        Room room;

        do
        {
            room = rooms[Random.Range(0, rooms.Count)];

        } while(IsPlayerRoom(room) || room.roomObj==currentRoom?.roomObj || IsRoomFull(room));

        return room;
    }
    
    public Transform GetRandomSpot(Room room, Transform prevSpot=null, int retries=20)
    {
        Transform newSpot=null;

        for(int i=0; i<retries; i++)
        {
            newSpot = room.spawnpoints[Random.Range(0, room.spawnpoints.Count)];

            if(!occupiedSpots.Contains(newSpot)) break;
        }

        return newSpot;
    }

    public void OccupySpot(Transform spot)
    {
        if(spot)
        {
            if(!occupiedSpots.Contains(spot))
            {
                occupiedSpots.Add(spot);
            }
        }
    }

    public void UnoccupySpot(Transform spot)
    {
        if(spot)
        {
            if(occupiedSpots.Contains(spot))
            {
                occupiedSpots.Remove(spot);
            }
        }
    }

    public bool IsPlayerRoom(Room room)
    {
        return room.roomObj==playerRoomObj;
    }

    public Room GetPlayerRoom()
    {
        foreach(Room room in rooms)
        {
            if(room.roomObj==playerRoomObj) return room;
        }
        return null;
    }

    public bool IsRoomFull(Room room)
    {
        foreach(Transform spot in room.spawnpoints)
        {
            if(!occupiedSpots.Contains(spot)) return false;
        }
        return true;
    }
}
