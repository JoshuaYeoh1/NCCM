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

        } while(IsPlayerRoom(room) || room.roomObj==currentRoom?.roomObj);

        return room;
    }
    
    public Transform GetRandomSpot(Room room)
    {
        Transform spot=null;

        for(int i=0; i<20; i++)
        {
            spot = room.spawnpoints[Random.Range(0, room.spawnpoints.Count)];

            if(!occupiedSpots.Contains(spot)) break;
        }

        if(spot) occupiedSpots.Add(spot);

        return spot;
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
}
