﻿using UnityEngine;
using System.Collections;


public class playGame : MonoBehaviour {

	public static bool onRope;
//start the game by loading the gather scene
	public void start()
	{
		Application.LoadLevel ("Gather_Rappel");
	}
}
