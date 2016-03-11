using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class inventoryManagement : MonoBehaviour {

	public static List<GameObject> inventory;
	// Use this for initialization
	void Start () {
		inventory = new List<GameObject> ();
	}
	
	public static void addItem(GameObject other)
	{
		inventory.Add (other);
	}

	//will add error handling later
	//index out of range exception possibility
	public static void removeItem(int selector)
	{
		inventory.RemoveAt [selector] ();
	}
}
