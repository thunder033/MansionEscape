using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Combatant))]
[RequireComponent(typeof(Animator))]
public class zombie : MonoBehaviour {

    public float speed;

    Combatant combatant;
    Animator anim;

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
            velocity = new Vector3(speed, 0, 0) * Mathf.Sin(moveTimer);
        //}
        
        anim.SetFloat("MoveSpeed", velocity.magnitude);
        transform.position += velocity;
	}
}
