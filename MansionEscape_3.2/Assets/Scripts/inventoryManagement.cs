using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class inventoryManagement : MonoBehaviour {

	public static List<string> inventory;
	// Use this for initialization
	void Start () {
		inventory = new List<string> ();
	}
	
	public static void addItem(string other)
	{
		inventory.Add (other);
		other = null;
	}

	//will add error handling later
	//index out of range exception possibility
	public static void removeItem(int selector)
	{
		inventory.RemoveAt(selector);
	}
}
