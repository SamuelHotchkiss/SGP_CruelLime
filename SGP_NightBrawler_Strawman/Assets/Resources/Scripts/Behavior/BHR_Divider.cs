using UnityEngine;
using System.Collections;

public class BHR_Divider : BHR_Base 
{
	// Use this for initialization
	void Start () 
	{
		ID = 9;
	}
	
	// Update is called once per frame
	void Update () 
	{
		base.Update();
	}

	public override void PerformBehavior()
	{
		Divide();
		Debug.Log("Divider Activated!");
		//owner.state = ACT_Enemy.STATES.IDLE;
	}

	public void Divide()
	{
		if (owner.numGeneration > 0)
		{
			owner.numGeneration--;
			owner.Act_baseHP /= owner.numQuotient;
			owner.Act_basePower /= owner.numQuotient;
			if (owner.Act_basePower <= 0)
			{
				owner.Act_basePower = 1;
			}
			owner.transform.localScale /= owner.numQuotient;

			for (int i = 0; i < owner.numQuotient; i++)
			{
				ACT_Enemy clone = Instantiate(owner, owner.transform.position, Quaternion.identity) as ACT_Enemy;
				clone.Act_currHP = clone.Act_baseHP;
				clone.Act_currPower = clone.Act_basePower;
				clone.state = ACT_Enemy.STATES.IDLE;
			}
		}
	}

    public void ModDivide(int _HpMod = 1, int _PowerMod = 1, int _ScaleMod = 1, ACT_Enemy.STATES _StateMod = ACT_Enemy.STATES.IDLE)
    {
        if (owner.numGeneration > 0)
        {
            owner.numGeneration--;
            owner.Act_baseHP /= _HpMod;
            owner.Act_basePower /= _PowerMod;
            if (owner.Act_basePower <= 0)
            {
                owner.Act_basePower = 1;
            }
            owner.transform.localScale /= _ScaleMod;

            for (int i = 0; i < owner.numQuotient; i++)
            {
                ACT_Enemy clone = Instantiate(owner, owner.transform.position, Quaternion.identity) as ACT_Enemy;
                clone.Act_currHP = clone.Act_baseHP;
                clone.Act_currPower = clone.Act_basePower;
                clone.state = _StateMod;
            }
        }
    }

}
