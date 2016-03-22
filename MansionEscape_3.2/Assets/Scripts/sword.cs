using UnityEngine;
using System.Collections;

public class sword : MonoBehaviour {

	//can only animate if true
	public static bool attack;
	public static Animator swordAnimator;

	// Use this for initialization
	void Start () {
		swordAnimator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	public static void Attack () {
		swordAnimator.SetTrigger ("attack");
	}
}
