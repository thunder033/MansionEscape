using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawnItem : MonoBehaviour {

	public Vector3 spawnPoint;
	public int selector;
	public List<GameObject> options;
	public int numberOfOptions = 5;
	public GameObject itemToSpawn;
	public GameObject option1;
	public GameObject option2;
	public GameObject option3;
	public GameObject option4;
	public GameObject option5;
	// Use this for initialization
	void Start () {
		options = new List<GameObject>();
		spawnPoint.x = transform.position.x;
		spawnPoint.y = transform.position.y;
		spawnPoint.z = 0;

		options.Add (option1);
		options.Add (option2);
		options.Add (option3);
		options.Add (option4);
		options.Add (option5);
		Spawn ();
	}

 	void Spawn () {
		selector = Random.Range(0, numberOfOptions);
		itemToSpawn = options[selector];
		Debug.Log (itemToSpawn.name);
		Instantiate (itemToSpawn, spawnPoint, Quaternion.identity);
	}
}
