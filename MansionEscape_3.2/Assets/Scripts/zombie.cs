using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Combatant))]
[RequireComponent(typeof(Animator))]
public class zombie : MonoBehaviour {

    public float speed;

    Combatant combatant;
    public Combatant target;
    Animator anim;
    int direction = 1;
    float attackCooldown = 0;

    Vector3 velocity;
    float moveTimer = 0;
	// Use this for initialization
	void Start () {
        combatant = GetComponent<Combatant>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if(attackCooldown > 0)
        {
            anim.SetBool("Attacking", false);
            attackCooldown -= Time.deltaTime;
        }
        else
        {
            moveTimer += Time.deltaTime;
        }
        
        velocity = Vector3.zero;
        float period = 5;

        if((moveTimer + period/2) % (period * 2) > period && attackCooldown <= 0)
        {
            velocity = new Vector3(speed, 0, 0) * Mathf.Sign(Mathf.Sin(moveTimer/period)) * Time.deltaTime;
        }
        
        anim.SetFloat("MoveSpeed", velocity.magnitude);
        transform.position += velocity;

        if(velocity.x/Mathf.Abs(velocity.x) != direction && velocity.x != 0)
        {
            Flip();
        }

        Vector3 disp = transform.position - target.transform.position;
        if (disp.magnitude < 2 && attackCooldown <= 0)
        {
            anim.SetBool("Attacking", true);
            attackCooldown = 5;
            if(Mathf.Sign(disp.x) == direction)
            {
                Flip();
            }
        }
            
	}

    //flip character through scaling
    void Flip()
    {
        direction *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
