using UnityEngine;
using System.Collections;

public class BHR_Base : MonoBehaviour {

	public ACT_Enemy owner;
	// Use this for initialization
	void Start () 
	{
		owner = GetComponent<ACT_Enemy>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public virtual void PerformBehavior()
	{

	}


}
