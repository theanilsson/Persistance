using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    // Variables -Felix
    private UnityEngine.UI.Text lifeText;
	public GameObject player;

    // Level spawning variables -Thea
    public GameObject spawnRoom;
    public GameObject[] roomsWithLeftEntrance;
    public GameObject[] roomsWithTopEntrance;
    public GameObject[] roomsWithBottomEntrance;
    public Vector3 origin;
    public GameObject lastRoomSpawned;
    protected int nextRightRoom;
    protected int nextTopRoom;
    protected int nextBottomRoom;

	// Use this for initialization
	void Start () 
	{
        // Spawns the level when the scene is opened -Thea
        SpawnLevel();

        // Spawns the player when the level is loaded -Thea
        Instantiate(player, GameObject.FindGameObjectWithTag("PlayerSpawn").transform.position, new Quaternion());
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

    // Spawns a randomized level when the method is called -Thea
    public void SpawnLevel()
    {
        // Places the start room -Thea
        lastRoomSpawned = Instantiate(spawnRoom, origin, new Quaternion()) as GameObject;

        for (int i = 0; i < 15; i++)
        {
            // Places rooms to the right if the exit faces that direction -Thea
            if (lastRoomSpawned.GetComponent<RoomVariables>().hasRightExit)
            {
                nextRightRoom = Random.Range(0, 7);
                lastRoomSpawned = Instantiate(roomsWithLeftEntrance[nextRightRoom], lastRoomSpawned.GetComponent<RoomVariables>().rightDoorCorner.transform.position -
                    roomsWithLeftEntrance[nextRightRoom].GetComponent<RoomVariables>().leftDoorCorner.transform.localPosition, new Quaternion()) as GameObject;
            }

            // Places rooms upwards if the exit faces that direction -Thea
            if (lastRoomSpawned.GetComponent<RoomVariables>().hasTopExit)
            {
                nextTopRoom = Random.Range(0, 4);
                lastRoomSpawned = Instantiate(roomsWithBottomEntrance[nextTopRoom], lastRoomSpawned.GetComponent<RoomVariables>().topDoorCorner.transform.position -
                    roomsWithBottomEntrance[nextTopRoom].GetComponent<RoomVariables>().bottomDoorCorner.transform.localPosition, new Quaternion()) as GameObject;
            }

            // Places rooms downwards if the exit faces that direction -Thea
            if (lastRoomSpawned.GetComponent<RoomVariables>().hasBottomExit)
            {
                nextBottomRoom = Random.Range(0, 4);
                lastRoomSpawned = Instantiate(roomsWithTopEntrance[nextBottomRoom], lastRoomSpawned.GetComponent<RoomVariables>().bottomDoorCorner.transform.position -
                    roomsWithTopEntrance[nextBottomRoom].GetComponent<RoomVariables>().topDoorCorner.transform.localPosition, new Quaternion()) as GameObject;
            }
        }
    }
}
