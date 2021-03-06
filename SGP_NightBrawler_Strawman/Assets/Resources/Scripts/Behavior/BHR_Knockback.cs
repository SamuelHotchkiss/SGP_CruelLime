﻿using UnityEngine;
using System.Collections;

public class BHR_Knockback : BHR_Base
{
    public int Knck_Direction;
    public bool Knck_MirrorDirection;

	// Update is called once per frame

	void Start()
	{
		ID = 6;
	}

	public override void Update() 
    {
		base.Update();

        owner.Knck_Cooldown -= Time.deltaTime;

        if (!Knck_MirrorDirection)
        {
            if (owner.Act_facingRight)
                Knck_Direction = 1;
            else
                Knck_Direction = -1; 
        }
        else
        {
            if (owner.Act_facingRight)
                Knck_Direction = -1;
            else
                Knck_Direction = 1; 
        }
	}
	public override void PerformBehavior()
	{
        if (owner.target != null)
        {
            if (owner.Knck_Cooldown <= 0)
            {
                float Force = (float)(owner.Act_currPower * Knck_Direction);
                owner.target.GetComponent<PlayerController>().ApplyKnockBack(Force);
                //KnockBack Should not Deal full Damage the push ist self is bad enough 
                owner.target.GetComponent<PlayerController>().party[owner.target.GetComponent<PlayerController>().currChar].ChangeHP(-owner.Act_currPower / 5);     
                // Debug.Log("KnockBack Activated!");
                owner.Knck_Cooldown = owner.Knck_baseCooldown;
            } 
        }
	}
}
