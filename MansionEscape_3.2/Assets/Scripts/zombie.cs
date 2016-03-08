using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Combatant))]
[RequireComponent(typeof(Animator))]
public class zombie : MonoBehaviour {

    public float speed;

    Combatant combatant;
    Animator anim;
    int direction = 1;

    Vector3 velocity;
    float moveTimer = 0;
	// Use this for initialization
	void Start () {
        combatant = GetComponent<Combatant>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        moveTimer += Time.deltaTime;
        velocity = Vector3.zero;

        //if((moveTimer + 180) % 720 > 360)
        //{
            velocity = new Vector3(speed, 0, 0) * Mathf.Sign(Mathf.Sin(moveTimer/2)) * Time.deltaTime;
        //}
        
        anim.SetFloat("MoveSpeed", velocity.magnitude);
        transform.position += velocity;

        if(velocity.x/Mathf.Abs(velocity.x) != direction)
        {
            Flip();
        }
	}

    //flip character through scaling
    void Flip()
    {
        direction *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
