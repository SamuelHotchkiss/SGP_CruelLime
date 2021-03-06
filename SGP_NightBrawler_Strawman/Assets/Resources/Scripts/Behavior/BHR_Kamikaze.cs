﻿using UnityEngine;
using System.Collections;

public class BHR_Kamikaze : BHR_Base
{
	// Use this for initialization
	void Start () 
	{
		ID = 5;
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		base.Update();
	}

	public override void PerformBehavior()
	{
        if (owner.target.transform.position.x > owner.transform.position.x)
        {
            Vector2 vel = owner.GetComponent<Rigidbody2D>().velocity;
            vel = new Vector2(owner.Act_currSpeed, vel.y);
            owner.GetComponent<Rigidbody2D>().velocity = vel;
            owner.Act_facingRight = true;
        }
        else
        {
            Vector2 vel = owner.GetComponent<Rigidbody2D>().velocity;
            vel = new Vector2(-owner.Act_currSpeed, vel.y);
            owner.GetComponent<Rigidbody2D>().velocity = vel;
            owner.Act_facingRight = false;
        }
        if (owner.target.transform.position.y > owner.transform.position.y)
        {
            Vector2 vel = owner.GetComponent<Rigidbody2D>().velocity;
            vel = new Vector2(vel.x, owner.Act_currSpeed);
            owner.GetComponent<Rigidbody2D>().velocity = vel;
        }
        else
        {
            Vector2 vel = owner.GetComponent<Rigidbody2D>().velocity;
            vel = new Vector2(vel.x, -owner.Act_currSpeed);
            owner.GetComponent<Rigidbody2D>().velocity = vel;
        }

        

		// Debug.Log("Kamikaze Activated!");
	}
}
