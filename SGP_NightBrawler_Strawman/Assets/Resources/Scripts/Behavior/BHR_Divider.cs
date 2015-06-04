using UnityEngine;
using System.Collections;

public class BHR_Divider : BHR_Base 
{
	public int numQuotient;
	public int numGeneration;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public override void PerformBehavior()
	{
		Divide();
		Debug.Log("Divider Activated!");
		owner.state = ACT_Enemy.STATES.IDLE;
	}

	public void Divide()
	{
		numQuotient--;


	}
}
