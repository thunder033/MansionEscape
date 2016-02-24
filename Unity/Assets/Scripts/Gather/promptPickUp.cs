using UnityEngine;
using System.Collections;

public class promptPickUp : MonoBehaviour 
{
	public static GameObject pickMe = null;
	public GameObject pickMeTest = null;


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player")) 
		{
			pickMe = gameObject;
			pickMeTest = gameObject;
			Debug.Log ("YES " + pickMe.ToString());
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player")) 
		{
			pickMe = null;
			pickMeTest = null;
			Debug.Log ("NO " + pickMe.ToString());
		}
	}

	void OnGUI()
	{
		GUI.contentColor = Color.white;
		if (pickMe != null) 
		{
			GUI.Label (new Rect (this.transform.position.x, this.transform.position.y + 1, 25, 10), "Press E to pick up ???");
		}
	}


}
