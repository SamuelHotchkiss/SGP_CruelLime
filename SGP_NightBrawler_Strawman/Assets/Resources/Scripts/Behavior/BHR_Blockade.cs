using UnityEngine;
using System.Collections;

public class BHR_Blockade : BHR_Base {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    //public void Update () 
    //{
    //    base.Update();
    //}

	public override void PerformBehavior()
	{
		Debug.Log("Blockade Activated!");
	}
}
