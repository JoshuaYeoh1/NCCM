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

    public Room GetRandomRoom(Room currentRoom=null, int retries=20)
    {
        Room room=null;

        for(int i=0; i<retries; i++)
        {
            room = rooms[Random.Range(0, rooms.Count)];

            if(!IsPlayerRoom(room) && !IsRoomFull(room))
            {
                if(room.roomObj!=currentRoom?.roomObj) break;
            }
        }

        return room;
    }
    
    public Transform GetRandomSpot(Room room)
    {
        Transform newSpot;

        List<Transform> unoccupiedSpots = GetUnoccupiedSpots(room);

        newSpot = GetRandomUnoccupiedSpot(unoccupiedSpots);

        return newSpot;
    }

    List<Transform> GetUnoccupiedSpots(Room room)
    {
        List<Transform> unoccupiedSpots = new();

        foreach(Transform spot in room.spawnpoints)
        {
            if(!occupiedSpots.Contains(spot))
            {
                unoccupiedSpots.Add(spot);
            }
        }

        return unoccupiedSpots;
    }

    Transform GetRandomUnoccupiedSpot(List<Transform> unoccupiedSpots)
    {
        if(unoccupiedSpots.Count>0)
        {
            return unoccupiedSpots[Random.Range(0, unoccupiedSpots.Count)];
        }
        return null;
    }

    public void OccupySpot(Transform spot)
    {
        if(!spot) return;

        if(!occupiedSpots.Contains(spot))
        {
            occupiedSpots.Add(spot);
        }
    }

    public void UnoccupySpot(Transform spot)
    {
        if(!spot) return;

        if(occupiedSpots.Contains(spot))
        {
            occupiedSpots.Remove(spot);
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

    public Transform toyolSpot;
}
