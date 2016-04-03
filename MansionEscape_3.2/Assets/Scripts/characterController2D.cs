using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Inventory))]
public class characterController2D : MonoBehaviour {

	public int health = 100;
	public float maxSpeed = 10f;
	bool facingRight = true;

	Animator anim;
    Inventory inventory;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;

    bool jumping = false;
    float jumpTimeout = 0;

	public float jumpForce = 700f;
	public float attackCooldown = 0f;
	
	void Start () 
	{
		anim = GetComponent<Animator> ();
        inventory = GetComponent<Inventory>();
	}

    // Update is called once per frame
    void FixedUpdate()
    {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        jumping = !(jumpTimeout == 0 && grounded);
        anim.SetBool("Jumping", jumping);

        //speed up or down
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

        //if (grounded)
       // {
            float move = Input.GetAxis("Horizontal");
            anim.SetFloat("Speed", Mathf.Abs(move));

            GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

            if (move > 0 && !facingRight)
            {
                Flip();
            }
            else if (move < 0 && facingRight)
            {
                Flip();
            }

     //   }

        if (jumping && jumpTimeout > 0)
        {
            jumpTimeout -= Time.deltaTime;

            if (jumpTimeout <= 0)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce * (grounded ? 1 : 0)));
                jumpTimeout = 0;
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
		if (Input.GetKeyDown(KeyCode.Space))
		{
			sword.Attack();

		}

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

			if (Input.GetKeyDown (KeyCode.E) && Item.colliding != null) {

				if(Item.colliding.activeSelf && inventory.addItem(Item.colliding.GetComponent<Item>()))
                {
                    Debug.Log(inventory.getSize());

                    //test purposes
                    //foreach (Item g in inventory.inventory)
                    //{
                    //	Debug.Log ("Inventory Contains: " + g);
                    //}

                    Item.colliding.SetActive(false);
                    Item.guiEnable = false;
                }
                
			}
		}

		if (zombie.touching && zombie.attacking) 
		{
			health -= 10;
			Debug.Log (health);
		}

		//DIE
		if (health == 0) 
		{
			Application.LoadLevel("Menu1");
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
