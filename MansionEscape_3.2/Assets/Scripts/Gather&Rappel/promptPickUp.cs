using UnityEngine;
using System.Collections;

public class promptPickUp : MonoBehaviour 
{
	public static GameObject pickMe = null;
	public string pickMeTest = null;
	public static bool guiEnable = true;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player")) 
		{
			pickMe = gameObject;
			pickMeTest = gameObject.name;
			Debug.Log ("YES " + pickMeTest);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player")) 
		{
			pickMe = null;
			pickMeTest = null;
			Debug.Log ("NO");
		}
	}

	void OnGUI()
	{
		GUI.contentColor = Color.white;
		if (guiEnable == true) 
		{
			GUI.Label (new Rect (this.transform.position.x, this.transform.position.y + 1, 25, 10), "Press E to pick up ???");
		}
	}


}
