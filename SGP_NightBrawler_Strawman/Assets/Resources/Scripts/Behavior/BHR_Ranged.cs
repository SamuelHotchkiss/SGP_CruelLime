using UnityEngine;
using System.Collections;

public class BHR_Ranged : BHR_Base
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		base.Update();
	}
	public override void PerformBehavior()
	{


		Debug.Log("Ranged Activated!");
	}
}
