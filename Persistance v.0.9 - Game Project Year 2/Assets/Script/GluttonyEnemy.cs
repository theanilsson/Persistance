using UnityEngine;
using System.Collections;

public class GluttonyEnemy : Enemy 
{

	// Use this for initialization

	void Start () 
	{


		base.rigidbody = GetComponent<Rigidbody2D>(); //Makes sure the rigidbody exists in child as well :Kajsa

		attackCounter = 100f;
		this.speed = 5f;
		this.patrolSpeed = 3f;
		this.hp = 4;
		//Sets slower speed :Felix
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Vector2.Distance (player.transform.position, transform.position) < range && !isAttacking) 
		{
			if (player.transform.position.x <= transform.position.x) 
			{
				rigidbody.velocity = new Vector2 (-speed, rigidbody.velocity.y);
			} else
				rigidbody.velocity = new Vector2 (speed, rigidbody.velocity.y);
			//Checks distance between player and the enemy and move towards them if they're in range :Felix + Sebbe

		}
		else if (!isAttacking)
		Patrol ();
		//If they can't find the player they insteads patrol around: Felix


		if (startCounter) 
	{
		attackCounter -= 1f;

		if (attackCounter <= 0f) 
		{
			attackCounter = 30f;
			startCounter = false;

			if (isAttacking && Vector2.Distance (player.transform.position, transform.position) < 3f)
				Attack ();

			isAttacking = false;
		}
	}

	if (hp <= 0)
		gameObject.SetActive (false);
		
	}

}
