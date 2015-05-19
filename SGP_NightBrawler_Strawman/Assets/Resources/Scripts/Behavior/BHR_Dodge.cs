using UnityEngine;
using System.Collections;

public class BHR_Dodge : BHR_Base
{
	// Use this for initialization
	void Start () {
		
	}

	public override void PerformBehavior()
	{
		if (owner.target.GetComponent<PlayerController>().nextState == ACT_CHAR_Base.STATES.ATTACK_1 ||
			owner.target.GetComponent<PlayerController>().nextState == ACT_CHAR_Base.STATES.ATTACK_2 ||
			owner.target.GetComponent<PlayerController>().nextState == ACT_CHAR_Base.STATES.ATTACK_3 ||
			owner.target.GetComponent<PlayerController>().nextState == ACT_CHAR_Base.STATES.SPECIAL)
		{
			int direction = Random.Range(0, 2);
			if (direction == 0)
				direction = -3;
			else
				direction = 3;
			owner.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, direction);
		}
	}
}
