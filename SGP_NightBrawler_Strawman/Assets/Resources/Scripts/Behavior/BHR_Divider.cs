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

    public void ModDivide(int _HpMod = 1, int _PowerMod = 1, int _ScaleMod = 1, int _SpeedMod = 1,  ACT_Enemy.STATES _StateMod = ACT_Enemy.STATES.IDLE)
    {
        if (owner.numGeneration > 0)
        {
            for (int i = 0; i < owner.numQuotient; i++)
            {
                
                ACT_Enemy clone = Instantiate(owner, owner.transform.position, Quaternion.identity) as ACT_Enemy;
                clone.Act_Parent = owner.gameObject;
                clone.numGeneration = 1 - i;
                clone.Act_baseHP /= _HpMod;
                clone.Act_basePower /= _PowerMod;
                clone.Act_baseSpeed /= _SpeedMod;

                clone.Act_baseSpeed += Random.Range(1, 5);

                if (clone.Act_basePower <= 0)
                {
                    clone.Act_basePower = 1;
                }

                clone.transform.localScale /= _ScaleMod;
                clone.Act_currHP = clone.Act_baseHP;
                clone.Act_currPower = clone.Act_basePower;
                clone.Act_currSpeed = clone.Act_baseSpeed;
                clone.state = _StateMod;
                if (i == 0)
                    clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10, 0));
                else
                    clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(10, 0));


                
            }
        }
    }

}
