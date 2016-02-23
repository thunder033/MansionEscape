using UnityEngine;
using System.Collections;

public class promptPickUp : MonoBehaviour {


	public bool hasCollided = false;
	public string labelText;

	public void OnGUI()
	{
		if (hasCollided == true) 
		{
			GUI.Box (Rect (140, Screen.height - 50, Screen.width - 300, 120), (labelText));
		}
	}
	
	public void OnTriggerEnter(Collider2D myCollider)
	{
		if (myCollider.gameObject.tag == "Player") 
		{
			hasCollided = true;
			labelText = "Press E to pick up ???";
		}
	}

	public void OnTriggerExit(Collider2D otherCollider)
	{
		hasCollided = false;
	}
}
