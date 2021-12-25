using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public float width;
    public float height;

    public int minRooms;
    public int maxRooms;
    public int totalRooms;

    Vector3 roomCoordinate = new Vector3();
    Vector3 newRoomCoordinate = new Vector3();

    public List<Vector2> direction = new List<Vector2> { new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0) };
    public List<Vector3> newRooms = new List<Vector3>();
    public List<GameObject> roomPreFabs = new List<GameObject>();

    public GameObject startRoom;
    public GameObject treasureRoom;

    bool doneGettingCoordinates = false;

    private void Start()
    {
        totalRooms = Random.Range(minRooms, maxRooms);
        roomCoordinate = new Vector3(0, 0, 0);
        newRooms.Add(roomCoordinate);

        RoomCoordinates();
    }
    private void Update()
    {
        if (doneGettingCoordinates)
        {
            CreateRooms();
            doneGettingCoordinates = false;
        }
    }

    void LoadRoom()
    {
        int dir = Random.Range(0, direction.Count);
        newRoomCoordinate = new Vector3(roomCoordinate.x + width * direction[dir].x, roomCoordinate.y, roomCoordinate.z + height * direction[dir].y);
        newRooms.Add(newRoomCoordinate);

        Vector2 lastDir = direction[dir];
        direction.Add(lastDir);
    }

    void RoomCoordinates()
    {
        while(newRooms.Count < totalRooms)
        {
            for (int i = newRooms.Count; i < totalRooms; i++)
            {
                LoadRoom();

                roomCoordinate = newRoomCoordinate;
            }
            newRooms = newRooms.Distinct().ToList();
        }
        if(newRooms.Count == totalRooms)
        {
            doneGettingCoordinates = true;
        }
    }

    void CreateRooms()
    {
        GameObject newRoom;
        for (int i = 0; i < totalRooms; i++)
        {
            if (i == 0)
            {
                newRoom = Instantiate(startRoom, newRooms[i], Quaternion.identity);
                newRoom.transform.parent = transform;
            }
            else if (i == Mathf.RoundToInt(totalRooms / 2))
            {
                newRoom = Instantiate(treasureRoom, newRooms[i], Quaternion.identity);
                newRoom.transform.parent = transform;
            }
            else
            {
                int rand = Random.Range(0, roomPreFabs.Count);
                newRoom = Instantiate(roomPreFabs[rand], newRooms[i], Quaternion.identity);
                newRoom.transform.parent = transform;
            }
        }
    }
}
