﻿using UnityEngine;
using System.Collections;

public class BHR_Kamikaze : BHR_Base
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public override void PerformBehavior()
	{
		Debug.Log("Kamikaze Activated!");
	}
}
