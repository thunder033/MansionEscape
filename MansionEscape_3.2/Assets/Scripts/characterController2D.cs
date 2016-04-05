using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Inventory))]
public class characterController2D : MonoBehaviour {

    public float health = 100;
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
    List<zombie> attackers;

    public Weapon weapon = null;

    void Start()
    {
        anim = GetComponent<Animator>();
        inventory = GetComponent<Inventory>();
        attackers = new List<zombie>();
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

        //}

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
            anim.SetBool("Climbing", true);
        }
        else if (climbDown.onRope == false)
        {
            anim.SetBool("Climbing", false);
        }
    }

    void Update()
    {
        if(attackCooldown < 1.0f && weapon != null)
        {
            weapon.gameObject.SetActive(false);
        }

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
            anim.SetBool("Attacking", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && attackCooldown <= 0)
        {
            anim.SetBool("Attacking", true);
        }

        if (climbDown.onRope == true) {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            if (Input.GetKeyDown(KeyCode.S)) {
                transform.Translate(new Vector3(0, -1, 0));
            }

            if (Input.GetKeyDown(KeyCode.W)) {
                transform.Translate(new Vector3(0, 1, 0));
            }
        } else if (climbDown.onRope == false) {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            if (grounded && Input.GetKeyDown(KeyCode.W)) {
                anim.SetBool("Ground", false);
                jumping = true;
                jumpTimeout = .25f;
            }

            if (Input.GetKeyDown(KeyCode.E) && Item.colliding != null) {

                if (Item.colliding.activeSelf && inventory.addItem(Item.colliding.GetComponent<Item>()))
                {
                    Item.colliding.SetActive(false);
                    Item.guiEnable = false;
                }

            }
        }

        anim.SetBool("Damaged", false);
        attackers.ForEach(zombie =>
        {
            if (zombie.isDamaging())
            {
                health -= zombie.damage * Time.deltaTime;
                anim.SetBool("Damaged", true);
                attackCooldown = 0.5f;
            }
        });

        //DIE
        if (health <= 0)
        {
            Application.LoadLevel("Menu1");
        }

    }

    public void Attack()
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(true);
            weapon.Attack();
        }
        attackCooldown = 2.0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        zombie zombie = other.gameObject.GetComponent<zombie>();
        if (zombie != null) attackers.Add(zombie);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        zombie zombie = other.gameObject.GetComponent<zombie>();
        if (zombie != null) attackers.Remove(zombie);
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
