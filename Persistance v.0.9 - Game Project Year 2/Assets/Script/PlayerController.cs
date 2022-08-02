using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rigidbody;
	private float horizontal;
	private float maxSpeed = 7f;
    private float speed = 60f;
    private bool facingRight;
	public int health = 5;
	public int maxHealth = 5;
	public float damage = 1f;
	public float attackDowntime;
	private bool isAlive = true;
	private float dodgeSpeed = 25f;
	private bool speedRestricted = true;
	public GameObject deathScreen;
    private GameObject sprite;
    private Animator animator;
    private bool grounded;
    public float jumpForce = 10;
    public GameObject spawnedAttackBlock;
    public GameObject attackBlock;


    // Sound related variables
    private AudioSource audioSource;
    public AudioClip[] audioClips;

	void Start () 
	{
		rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        sprite = GameObject.FindGameObjectWithTag("PlayerSprite");
        animator = sprite.GetComponent<Animator>();
    }
	
	void Update () 
	{

        if (rigidbody.velocity.x > maxSpeed && speedRestricted)
        rigidbody.velocity = new Vector2(maxSpeed, rigidbody.velocity.y);

        else if (rigidbody.velocity.x < -maxSpeed && speedRestricted)
            rigidbody.velocity = new Vector2(-maxSpeed, rigidbody.velocity.y);

		// Sets a max speed that the character cant exceed -Seb

		horizontal = Input.GetAxis ("Horizontal");

		if (horizontal != 0f && isAlive)
        {
			rigidbody.velocity += new Vector2 (horizontal, 0f) * speed * Time.deltaTime;

			if (horizontal >= 0 && facingRight == true)
            {
                sprite.transform.localScale = new Vector2(-sprite.transform.localScale.x, sprite.transform.localScale.y);
                facingRight = false;
			} else if (horizontal <= 0 && facingRight == false)
            {
                sprite.transform.localScale = new Vector2(-sprite.transform.localScale.x, sprite.transform.localScale.y);
                facingRight = true;
            }
		}
        // Jumps with raycast -Seb
        if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y) - new Vector2(0f, sprite.GetComponent<SpriteRenderer>().bounds.size.y / 2),
            new Vector2(0f, -1f), 0.15f) == true && Input.GetButtonDown ("Jump"))
        {
			rigidbody.velocity += new Vector2 (0f, jumpForce);
			PlaySound (0);
		}

        if (Input.GetButtonDown ("Attack") && isAlive)
        {
			if (sprite.transform.localScale.x > 0)
            {
                spawnedAttackBlock = Instantiate(attackBlock, sprite.transform.position + new Vector3(1.5f, 0f, 0f), new Quaternion()) as GameObject;
            }
            // Instantiates an attack block to the right if the player is facing that way and attacks -Thea & Seb
            else if (sprite.transform.localScale.x < 0 && isAlive)
            {
                spawnedAttackBlock = Instantiate(attackBlock, sprite.transform.position + new Vector3(-1.5f, 0f, 0f), new Quaternion()) as GameObject;
            }
            // Instantiates an attack block to the left if the player is facing that way and attacks -Thea & Seb

            PlaySound(1);
			//Swing attack
		}
        else if (Input.GetButtonUp ("Attack") )
			StartCoroutine(AttackFade(0.05f));
		// A basic temp version of the attack -Seb

		if (Input.GetButtonDown ("AttackHeavy") && isAlive) {
			PlaySound (1);
			//Heavy Attack
		}

		if (health <= 0) 
		{
			isAlive = false;
		}

		if (Input.GetButtonDown("Dodge"))
		{
			rigidbody.velocity = new Vector2(sprite.transform.localScale.x * dodgeSpeed, rigidbody.velocity.y);
			speedRestricted = false;
			StartCoroutine(DodgeTimer(0.25f));
		}

        Animate();

    }

	IEnumerator DodgeTimer(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		speedRestricted = true;
	}

	IEnumerator AttackFade(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
        Destroy(spawnedAttackBlock);
	}

    private void PlaySound(int soundNr)
    {
        audioSource.PlayOneShot(audioClips[soundNr], 1f);
    }

    // Holds all the animations -Seb
    private void Animate()
    {
        animator.SetFloat("Speed",Mathf.Abs(rigidbody.velocity.x));
        animator.SetFloat("PlaySpeed", Mathf.Abs(rigidbody.velocity.x / maxSpeed));

        // Checks if the player is on the ground -Seb
        grounded = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y) - new Vector2(0f, sprite.GetComponent<SpriteRenderer>().bounds.size.y / 2),
            new Vector2(0f, -1f), 0.15f);

        animator.SetBool("Grounded", grounded);
    }

    public void TakeDamage(string typeOfDamage)
    {
        if (typeOfDamage == "Regular")
            health -= 1;

        if (typeOfDamage == "Heavy")
            health -= 2;

        if (health <= 0)
            Die();
    }

    public void Die()
	{
		//deathScreen.SetActive (true);
	}








}