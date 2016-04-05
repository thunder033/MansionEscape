using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Weapon : MonoBehaviour {

    public float damage;
    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    public void Attack()
    {
        anim.Play("attack");
    }
}
