using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    //public GameObject player;
    public SpriteRenderer playerSprite;
    private int sideTimer = 0;
    private bool swapSide;
    private float offsetX = 7f;
    private float offsetY = 1.2f;
    
    void Start ()
    {
        playerSprite = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<SpriteRenderer>();
        transform.position = new Vector3(playerSprite.transform.position.x + offsetX, playerSprite.transform.position.y + offsetY, -10f);
        // Sets the starting position -Seb
	}

	void Update ()
    {

        // Decides the delay for the camera -Seb
        if (playerSprite.transform.localScale.x >= 0)
        {
            if (sideTimer < 0)
                sideTimer = 0;

            sideTimer++;

            if (sideTimer >= 30)
            {
                sideTimer = 30;
                swapSide = true;
            }
        }

        else
        {
            if (sideTimer > 0)
                sideTimer = 0;

            sideTimer--;

            if (sideTimer <= -30)
            {
                sideTimer = -30;
                swapSide = false;
            }
        }
    }

    void FixedUpdate()
    {
        // Moves the camera depending on which way the player is facing after a delay -Seb
        if (swapSide)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3
                (playerSprite.transform.position.x + offsetX, playerSprite.transform.position.y + offsetY, -10f), 0.01f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3
                (playerSprite.transform.position.x - offsetX, playerSprite.transform.position.y + offsetY, -10f), 0.01f);
        }
    }
}
