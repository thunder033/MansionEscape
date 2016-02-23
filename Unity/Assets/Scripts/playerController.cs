using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	//player related
	public float maxSpeed = 10f;
	public float jumpForce = .05f;
	bool facingRight = true;
	bool grounded = false;
	Animator anim;

	//used for checking groudned bool
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();
	}
	

	void FixedUpdate () 
	{
		//checks if any ground is below player
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool("Ground", grounded);

		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

		float move = Input.GetAxis ("Horizontal");

		anim.SetFloat ("Speed", Mathf.Abs (move));

		GetComponent<Rigidbody2D>().velocity = new Vector3 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y, 0);

		//if we change direction
		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
	}

	void Update()
	{
		if (grounded && Input.GetKeyDown (KeyCode.Space)) 
		{
			anim.SetBool ("Ground", false);
			GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, jumpForce));
		}

		if (Input.GetKeyDown (KeyCode.W) && this.transform.position.z < 1) 
		{
			transform.Translate (new Vector3 (0, 0, 1));
		}

		if (Input.GetKeyDown (KeyCode.S) && this.transform.position.z > 0) 
		{
			transform.Translate (new Vector3 (0, 0, -1));
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
