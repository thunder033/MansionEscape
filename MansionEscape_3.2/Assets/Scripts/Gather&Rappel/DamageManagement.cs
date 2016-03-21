using UnityEngine;
using System.Collections;

public class DamageManagement : MonoBehaviour {

	public int health = 100;
	public int zomAttack = 10;
	public int playerAttack = 50;
	public bool alive = true;

	public void die ()
	{
		//play zombie die animation
		Destroy (gameObject);
	}

	public void takeDamage()
	{
		if (gameObject.CompareTag ("Player")) {
			health -= zomAttack;
		} 
		else if (gameObject.CompareTag ("Zombie")) {
			health -= playerAttack;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
