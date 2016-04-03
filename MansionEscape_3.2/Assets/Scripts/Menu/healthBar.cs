using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class healthBar : MonoBehaviour {

    public characterController2D target;
    public Animator HUDAnimate;

    private Animator anim;
    private float lastHealth;

	// Use this for initialization
	void Start () {
        anim = transform.GetComponent<Animator>();
        lastHealth = target.health;
	}
	
	// Update is called once per frame
	void Update () {
        if(target.health < lastHealth)
        {
            lastHealth = target.health;
            HUDAnimate.Play("shake", 0, 0);
        }

        anim.Play("bar", -1, target.health / 100.0f);
        anim.speed = 0;
	}
}
