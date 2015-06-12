using UnityEngine;
using System.Collections;

public class BHR_Debuffer : BHR_Base
{

	// Use this for initialization
	void Start () 
	{
		ID = 3;
	}
	
	// Update is called once per frame
	public override void Update() 
	{
		base.Update();
	}
	public override void PerformBehavior()
	{
		Debug.Log("Debuffer Activated!");
	}
}
