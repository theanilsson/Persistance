using UnityEngine;
using System.Collections;

public class AttackBlockManager : MonoBehaviour {

    float timeSinceSpawned = 0f;
    float maxTimeSpawned = 0.5f;

	void Start ()
    {
	
	}
	
	void Update ()
    {
        // Destroys the attackblock if it has been spawned but not destroyed properly from the attacktimer in playercontroller -Thea
        timeSinceSpawned += Time.deltaTime;
        if (timeSinceSpawned >= maxTimeSpawned)
            Destroy(gameObject);
	}
}
