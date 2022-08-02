using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	protected int hp = 3;
	int damage;
	protected float speed = 10f;
	protected float patrolSpeed = 7f;
	float maxSpeed;
	protected float range = 5f;
	public GameObject player;
	protected Rigidbody2D rigidbody;
	private float movementCounter = 60f;
	protected bool isAttacking;
	protected float attackCounter = 30f;
	protected bool startCounter = false;
	private int rndNumber;
	private float jumpCounter = 500f;

	// Movement control variables -Thea
	private RaycastHit2D LeftPlatformRay;
    private RaycastHit2D RightPlatformRay;
    private float horizontal;
    private bool facingRight = false;
    private GameObject sprite;
    public Sprite originalSprite;
    private bool isFollowingPlayer = false;

	void Start () 
	{
        sprite = gameObject.transform.FindChild("SpriteHolder").gameObject;
		rigidbody = GetComponent<Rigidbody2D>(); // Gives the rigidbody component a different varible to make it easier to type
	}
	
	void Update () 
	{
        // Makes sure the player variable is always the player in the level
        player = GameObject.FindGameObjectWithTag("Player");

        transform.GetComponentInChildren<SpriteRenderer>().sprite = originalSprite;

        LeftPlatformRay = Physics2D.Raycast(new Vector2(transform.Find("Bottom Left").transform.position.x - 0.1f, transform.position.y),
            Vector2.down, 2f);
        RightPlatformRay = Physics2D.Raycast(new Vector2(transform.Find("Bottom Right").transform.position.x + 0.1f, transform.position.y),
            Vector2.down, 2f);

        if (Vector2.Distance(player.transform.position, transform.position) < range && !isAttacking)
        {
            isFollowingPlayer = true;
            if (player.transform.position.x <= transform.position.x)
            {
                rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
            }
            else
                rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
            // Checks distance between player and the enemy and move towards them if they're in range :Felix + Sebbe

        }
        else if (!isAttacking)
        {
            Patrol();
            StayOnPlatforms();
            isFollowingPlayer = false;
        }
		// If they can't find the player they insteads patrol around: Felix


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
	
	protected virtual void OnCollisionStay2D (Collision2D collision)
	{
		
		if (collision.gameObject.tag == "Player") 
		{
			isAttacking = true;
			rigidbody.velocity = new Vector2 (0f, 0f);
			startCounter = true;

		} 

	}

	protected virtual void OnTriggerEnter2D (Collider2D collision)
	{
		if (collision.gameObject.tag == "AttackBlock") 
		{
			hp -= 1;
		}
	}

	protected virtual void Attack()
	{
		player.GetComponent<PlayerController> ().TakeDamage ("Regular");
		gameObject.GetComponent<AudioSource>().Play ();

	}

	public virtual void Patrol()
	{
		movementCounter -= 1f;
		//jumpCounter -= 1f;

        if (movementCounter <= 0f) 
		{
			rigidbody.velocity = new Vector2 (Random.Range (-patrolSpeed, patrolSpeed), rigidbody.velocity.y);
			movementCounter = 60f;
		}
		// After a specific time it randomize if it should move forward or backwards in x :Felix

		// Flips the texture of the enemy depending och which direction it's facing -Thea
		if (rigidbody.velocity.x <= 0 && facingRight == true)
        {
            sprite.transform.localScale = new Vector2(-sprite.transform.localScale.x, sprite.transform.localScale.y);
            facingRight = false;
        }
        else if (rigidbody.velocity.x > 0 && facingRight == false)
        {
            sprite.transform.localScale = new Vector2(-sprite.transform.localScale.x, sprite.transform.localScale.y);
            facingRight = true;
        }
    }

	// Keeps the enemies from walking off platforms -Thea
	public void StayOnPlatforms()
    {
        if (LeftPlatformRay == false && !facingRight)
        {
            rigidbody.velocity = new Vector2(2.5f, rigidbody.velocity.y);
            facingRight = false;
        }
        else if (RightPlatformRay == false && facingRight)
        {
            rigidbody.velocity = new Vector2(-2.5f, rigidbody.velocity.y);
            facingRight = true;
        }
    }
}
