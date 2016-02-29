using UnityEngine;
using System.Collections;

public class climbDown : MonoBehaviour 
{

	public static bool onRope = false;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player")) 
		{
			onRope = true;
			Debug.Log ("YES");
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player")) 
		{
			onRope = false;
			Debug.Log ("NO");
		}
	}

}
