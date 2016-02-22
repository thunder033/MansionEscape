using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class playGame : MonoBehaviour {

//start the game by loading the gather scene
	public void start()
	{
		SceneManager.LoadScene ("Gather");
	}
}
