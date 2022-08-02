using UnityEngine;
using System.Collections;

public class GluttonyBoss : Enemy 
{
	private int bossAttackCount = 5;

	// Use this for initialization
	void Start () 
	{
		this.speed = 2f;
		base.rigidbody = GetComponent<Rigidbody2D>();
		attackCounter = 100f;
		isAttacking = false;
		this.hp = 30;

		//Has an extra heavy attack which if it hits regains part of it's HP, has a great delay
	
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (!isAttacking) 
		{
			if (player.transform.position.x <= transform.position.x) 
			{
				rigidbody.velocity = new Vector2 (-speed, rigidbody.velocity.y);
			} else
				rigidbody.velocity = new Vector2 (speed, rigidbody.velocity.y);
		}
		//Moves boss as long as it's not attacking :Felix
		Debug.Log(bossAttackCount);

		if (startCounter) 
		{
			attackCounter -= 1f;

			if (attackCounter <= 0f) 
			{
				attackCounter = 30f;
				startCounter = false;

				if (isAttacking && Vector2.Distance (player.transform.position, transform.position) < 8f) 
				{
					base.Attack ();
					bossAttackCount--;
				}

				if (isAttacking && Vector2.Distance (player.transform.position, transform.position) < 8f && bossAttackCount == 0) 
				{
					BossHeavy ();
					bossAttackCount = 5;
				}

				isAttacking = false;
			}
		}

		if (hp <= 0)
			gameObject.SetActive (false);

	
	}

	void BossHeavy()
	{
		player.GetComponent<PlayerController> ().TakeDamage ("Regular");
		gameObject.GetComponent<AudioSource>().Play ();
		this.hp++;

	}

	protected override void Attack()
	{
		player.GetComponent<PlayerController> ().TakeDamage ("Heavy");
		gameObject.GetComponent<AudioSource>().Play ();
	}
}
