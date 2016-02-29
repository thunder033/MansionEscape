using UnityEngine;
using System.Collections;

public class characterController2D : MonoBehaviour {

	public float maxSpeed = 10f;
	bool facingRight = true;

	Animator anim;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;

	public float jumpForce = 700f;
	
	void Start () 
	{
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		//speed up or down
		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D> ().velocity.y);

		float move = Input.GetAxis("Horizontal");

		anim.SetFloat ("Speed", Mathf.Abs (move));

		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if (move > 0 && !facingRight) {
			Flip ();
		} else if (move < 0 && facingRight) {
			Flip ();
		}
	}

	void Update()
	{
		if (climbDown.onRope == true) {
			gameObject.GetComponent<Rigidbody2D> ().gravityScale = 0;
			if (Input.GetKeyDown (KeyCode.S)) {
				transform.Translate (new Vector3 (0, -1, 0));
			}
			
			if (Input.GetKeyDown (KeyCode.W)) {
				transform.Translate (new Vector3 (0, 1, 0));
			}
		} else if (climbDown.onRope == false) {
			gameObject.GetComponent<Rigidbody2D> ().gravityScale = 1;
			if (grounded && Input.GetKeyDown (KeyCode.Space)) {
				anim.SetBool ("Ground", false);
				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
			}

			if (Input.GetKeyDown (KeyCode.E) && promptPickUp.pickMe != null) {
				Destroy (promptPickUp.pickMe.gameObject);
				promptPickUp.guiEnable = false;
			}
		}
	}
	//flip character through scaling
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		//Debug.Log ("Flipped");
	}
}
