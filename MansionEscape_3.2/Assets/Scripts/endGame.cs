using UnityEngine;
using System.Collections;

public class endGame : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			Application.LoadLevel("Menu1");
		}
	}
}
