using UnityEngine;
using System.Collections;


public class promptEscape : MonoBehaviour {

	public static bool atWindow = false;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player")) 
		{
			atWindow = true;
			Debug.Log ("YES");
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player")) 
		{
			atWindow = false;
			Debug.Log ("NO");
		}
	}
}
