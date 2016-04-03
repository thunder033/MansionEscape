using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class healthBar : MonoBehaviour {

    public characterController2D target;
    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = transform.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        anim.Play("bar", -1, target.health / 100.0f);
        anim.speed = 0;
	}
}
