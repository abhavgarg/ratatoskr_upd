using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFiniteStateMachine : MonoBehaviour {

	public float patrolSpeed = 0f;
	public float maxSpeed = 6f;
	public float triggerRange = 9.5f;
	bool facingRight = true;
	Animator anim;
	bool movementAllowed = true;
	private float speedMultiplier = 1f;

	bool grounded = false;
	public GameObject groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;

	public GameObject stateBanner;
	public Sprite chaseSprite;
	public Sprite patrolSprite;
	public Sprite deadSprite;
	public Sprite waitSprite;


	private enum State {Patrol = 0, Chase = 1, Dead = 2, Wait = 3};
	private State aiState = State.Patrol;

	GameObject player;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		player = GameObject.Find("Player");

	}

	public void StopBots()
	{
		maxSpeed = 0f;
		patrolSpeed = 0f;
		triggerRange = 0f;
		movementAllowed = false;
		Rigidbody2D rigidBodyComp = this.GetComponent<Rigidbody2D>();
		rigidBodyComp.velocity = new Vector2(0f, rigidBodyComp.velocity.y);
		anim.SetFloat("speed", 0f);
		aiState = State.Wait; 
	}

	// Called at set interval each time. Good for physics
	void FixedUpdate()
	{
		BaseMovementAndAnimation();

		switch(aiState)
		{
		case State.Chase:
			ChasePlayer();
			break;
		case State.Patrol:
			PerformPatrol();
			break;
		case State.Dead:       
			break;
		case State.Wait:
			break;
		default:
			break;
		}
	}

	private void BaseMovementAndAnimation()
	{
		Rigidbody2D rigidBodyComp = GetComponent<Rigidbody2D>();
 
		grounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundRadius, whatIsGround);
		anim.SetBool("ground", grounded);

        if ((facingRight && (this.transform.position.x > player.transform.position.x)) || (!facingRight && (this.transform.position.x < player.transform.position.x)))
        {
            
            FlipTransform();
        }

        if (movementAllowed)
		{
			PerformMovement(aiState == State.Chase ? maxSpeed : patrolSpeed);
		}

		if(rigidBodyComp.position.y <= -5.5f)
		{
			aiState = State.Dead;
			Dead();
		}
	}

	void PerformPatrol()
	{
		Vector3 offset = new Vector3(patrolSpeed, 0f);
		bool grounded = Physics2D.OverlapCircle(groundCheck.transform.position + offset, groundRadius, whatIsGround);
		if(!grounded)
		{
			FlipMovement();
		}

		if((this.transform.position - player.transform.position).magnitude < triggerRange)
		{
			aiState = State.Chase;
		}
	}

	void ChasePlayer()
	{
        Vector3 offset = new Vector3(patrolSpeed, 0f);
        float horizontalDifference = this.transform.position.x - player.transform.position.x;
        bool grounded = Physics2D.OverlapCircle(groundCheck.transform.position + offset, groundRadius, whatIsGround);
        if (!grounded)
        {
            FlipMovement();
        }
        if (((maxSpeed > 0f) && (horizontalDifference > 0f)) || ((maxSpeed < 0f) && (horizontalDifference < 0f)))
		{
			speedMultiplier = 3f;
			FlipMovement();
		}

		if((this.transform.position - player.transform.position).magnitude > triggerRange*1.5f)
		{
			aiState = State.Patrol;
		}
	}

	void FlipMovement()
	{
		patrolSpeed *= -1;
		maxSpeed *= -1;
	}

	void FlipTransform()
	{
		speedMultiplier = 1f;

		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	void PerformMovement(float speed)
	{
		float absSpeed = Mathf.Abs(speed);
		Rigidbody2D rigidBodyComp = GetComponent<Rigidbody2D>();
		float newXVelocity = rigidBodyComp.velocity.x + speed*speedMultiplier*Time.fixedDeltaTime;
		rigidBodyComp.velocity = new Vector2(Mathf.Clamp(newXVelocity, -absSpeed, absSpeed), rigidBodyComp.velocity.y);
		anim.SetFloat("speed", speed/maxSpeed);
	}

	private void Dead()
	{
		anim.SetBool("dead", true);
		this.GetComponent<Rigidbody2D>().gravityScale = 0f;
		Destroy(this.GetComponent<Collider2D>());
		movementAllowed = false;
	}
}
