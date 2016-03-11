using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class characterController2D : MonoBehaviour {

	public float maxSpeed = 10f;
    float speed = 0;
    float acceleration = 10;
	bool facingRight = true;

	Animator anim;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;

    bool jumping = false;
    float jumpTimeout = 0;
	public float jumpForce = 700f;

    float landTimeout = 0;
    float landingModifier = 25;
    float landingThreshold = .5f;

    void Start () 
	{
		anim = GetComponent<Animator> ();
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        anim.SetBool("Jumping", jumping);

        //speed up or down
        float vSpeed = GetComponent<Rigidbody2D>().velocity.y;
        anim.SetFloat("vSpeed", vSpeed);
        
        if(-vSpeed > landingThreshold && -vSpeed > landTimeout / landingModifier)
        {
            landTimeout = -vSpeed / landingModifier;
        }

        landTimeout -= grounded ? Time.deltaTime : 0;
        speed *= .88f;
        anim.speed = 1;

        if (grounded && landTimeout <= 0)
        {
            speed += Input.GetAxis("Horizontal") * Time.deltaTime * acceleration;
            speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);

            anim.SetFloat("Speed", Mathf.Abs(speed));

            if(Mathf.Abs(speed) > .25f)
            {
                anim.speed = Mathf.Abs(speed * 2 / maxSpeed);
            }

            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

            if (speed > 0 && !facingRight)
            {
                Flip();
            }
            else if (speed < 0 && facingRight)
            {
                Flip();
            }
        }
        else if(landTimeout > 0 && grounded)
        {
            speed = 0;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            anim.SetFloat("Speed", 0);
        }

        if (jumping && jumpTimeout > 0)
        {
            jumpTimeout -= Time.deltaTime;

            if (jumpTimeout <= 0)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce * (grounded ? 1 : 0)));
                jumpTimeout = 0;
                jumping = false;
            }
        }

        if (climbDown.onRope == true) {
			anim.SetBool ("Climbing", true);
		} 
		else if (climbDown.onRope == false) 
		{
			anim.SetBool("Climbing", false);
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
			if (grounded && Input.GetKeyDown (KeyCode.W)) {
				anim.SetBool ("Ground", false);
                jumping = true;
                jumpTimeout = .25f;
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
