using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    [System.Serializable]
    public class Room
    {
        public GameObject roomObject;
        public bool isAnomaly;
    }

    public Room[] rooms;
    public int startingRoomIndex = 0;
    public GameManager gameManager;

    private bool isFirstRoomShown = false;
    private int lastRoomIndex = -1;

    public void ShowRoom()
    {
        // Hide all rooms
        foreach (Room room in rooms)
        {
            if (room.roomObject != null)
                room.roomObject.SetActive(false);
        }

        int roomIndex;

        // First room logic
        if (!isFirstRoomShown)
        {
            roomIndex = startingRoomIndex;
            isFirstRoomShown = true;
        }
        else
        {
            do
            {
                roomIndex = Random.Range(0, rooms.Length);
            }
            while (roomIndex == lastRoomIndex || roomIndex == startingRoomIndex);
        }

        Room chosenRoom = rooms[roomIndex];
        chosenRoom.roomObject.SetActive(true);
        gameManager.SetAnomalyState(chosenRoom.isAnomaly);
        lastRoomIndex = roomIndex;
    }

    public void ResetRoomState()
    {
        isFirstRoomShown = false;
    }
}