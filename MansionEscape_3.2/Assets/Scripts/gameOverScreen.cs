using UnityEngine;
using System.Collections;

public class gameOverScreen : MonoBehaviour {


	void Update () {
	if (Input.anyKey) {
			Application.LoadLevel("Menu1");
		}
	}
}
