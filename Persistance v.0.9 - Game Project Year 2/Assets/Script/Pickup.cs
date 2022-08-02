using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

    public int pickupType;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void OnCollisionEnter (Collision collision)
    {
        // Checks if the player collides with the pickup and gives it the appropriate powerup if so
        if (collision.gameObject.tag == "Player")
        {
            if (pickupType == 1 && collision.gameObject.GetComponent<PlayerController>().health < collision.gameObject.GetComponent<PlayerController>().maxHealth)
            {
                collision.gameObject.GetComponent<PlayerController>().health += 1;
            }
            else if (pickupType == 2)
            {
                collision.gameObject.GetComponent<PlayerController>().attackDowntime -= 0.25f;
            }
            else if (pickupType == 3)
            {
                collision.gameObject.GetComponent<PlayerController>().damage *= 1.05f;
            }
            else
            {
                collision.gameObject.GetComponent<PlayerController>().maxHealth += 1;
            }
        }
    }
}
