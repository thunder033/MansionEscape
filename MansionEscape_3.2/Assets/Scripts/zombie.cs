using UnityEngine;
using System.Collections;

public class zombie : MonoBehaviour {

    public float speed = 1f;
    public GameObject target;
    Animator anim;
    int direction = 1;
    float attackCooldown = 0;
	bool touching = false;
	bool attacking = false;
	public int health = 100;
    public const float damage = .55f;

    Vector3 velocity;
    float moveTimer = 0;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		speed = 1f;
        if(attackCooldown > 0)
        {
            anim.SetBool("Attacking", false);
			attacking = false;
            attackCooldown -= Time.deltaTime;
        }
        else
        {
            moveTimer += Time.deltaTime;
        }
        
        velocity = Vector3.zero;
        float period = 3;

        if((moveTimer + period/2) % (period * 2) > period && attackCooldown <= 0)
        {
            velocity = new Vector3(speed, 0, 0) * Mathf.Sign(Mathf.Sin(moveTimer/period));
        }

        Vector3 disp = this.transform.position - target.transform.position;
        if (disp.magnitude < 8f && Mathf.Sign(disp.x) != direction && attackCooldown == 0)
        {
            velocity = new Vector3(speed, 0, 0) * Mathf.Sign(disp.x) * -1;
        }

        velocity *= Time.deltaTime;
        anim.SetFloat("MoveSpeed", velocity.magnitude);
        transform.position += velocity;

        if (velocity.x / Mathf.Abs (velocity.x) != direction && velocity.x != 0) {
			Flip ();
		}

        if (disp.magnitude < 2.75f && attackCooldown <= 0)
        {
            anim.SetBool("Attacking", true);
			attacking = true;
            attackCooldown = 3;
            if(Mathf.Sign(disp.x) == direction)
            {
                Flip();
            }
        }
            
		if (health <= 0) {
			Destroy(gameObject);
		}
    }

    public void TookDamage()
    {
        anim.SetBool("Damaged", false);
    }

	public void OnTriggerEnter2D(Collider2D other)
	{
        Weapon weapon = other.GetComponent<Weapon>();
        if(weapon != null)
        {
            health -= (int)(weapon.damage);
            anim.SetBool("Damaged", true);
            attackCooldown = .75f;
        }
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			touching = false;
		}
	}

    public bool isDamaging()
    {
        return attackCooldown > .75f && attackCooldown < 2.5f;
    }
	

    //flip character through scaling
    void Flip()
    {
        direction *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
