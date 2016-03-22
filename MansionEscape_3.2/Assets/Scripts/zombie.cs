using UnityEngine;
using System.Collections;

public class zombie : MonoBehaviour {

    public float speed = 1f;
    public GameObject target;
    Animator anim;
    int direction = 1;
    float attackCooldown = 0;
	public static bool touching = false;
	public static bool attacking = false;
	public int health = 100;

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
            velocity = new Vector3(speed, 0, 0) * Mathf.Sign(Mathf.Sin(moveTimer/period)) * Time.deltaTime;
        }
        
        anim.SetFloat("MoveSpeed", velocity.magnitude);
        transform.position += velocity;

        if (velocity.x / Mathf.Abs (velocity.x) != direction && velocity.x != 0) {
			Flip ();
		}

        Vector3 disp = this.transform.position - target.transform.position;
        if (disp.magnitude < 2 && attackCooldown <= 0)
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

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			touching = true;
		}

		else if (other.CompareTag ("PlayerWeapon")) 
		{
			health -= 20;
			Debug.Log ("Zom Health " + health);
		}
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			touching = false;
		}
	}
	

    //flip character through scaling
    void Flip()
    {
        direction *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
