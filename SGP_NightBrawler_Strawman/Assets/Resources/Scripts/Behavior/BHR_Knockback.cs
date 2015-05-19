using UnityEngine;
using System.Collections;

public class BHR_Knockback : BHR_Base
{
    int Knck_Direction;
    float Knck_Cooldown;

	// Use this for initialization
	void Start () {
        Knck_Cooldown = 10.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        Knck_Cooldown -= Time.deltaTime;

        if (owner.Act_facingRight)
            Knck_Direction = 1;
        else
            Knck_Direction = -1;
	}
	public override void PerformBehavior()
	{
        if (Knck_Cooldown <= 0)
        {
            float Force = (float)(owner.Act_currPower * Knck_Direction);
            owner.target.GetComponent<PlayerController>().ApplyKnockBack(Force);
           // owner.target.GetComponent<PlayerController>().party[owner.target.GetComponent<PlayerController>().currChar].ChangeHP(-owner.Act_currPower);
            Debug.Log("KnockBack Activated!");
            Knck_Cooldown = 10.0f;
        }
	}
}
