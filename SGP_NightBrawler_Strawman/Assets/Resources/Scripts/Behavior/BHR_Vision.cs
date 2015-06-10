using UnityEngine;
using System.Collections;

public class BHR_Vision : BHR_Base
{

	// Use this for initialization
	void Start () 
	{
		ID = 1;
	}
	
	// Update is called once per frame
	public override void Update()
	{
		base.Update();
	}

	public override void PerformBehavior()
	{
		//owner.state = ACT_Enemy.STATES.IDLE;

		if (!owner.look)
		{
			owner.look = true;
			owner.visionTimer = owner.visionTimerBase;
			owner.Act_facingRight = !owner.Act_facingRight;
		}
	}
}
