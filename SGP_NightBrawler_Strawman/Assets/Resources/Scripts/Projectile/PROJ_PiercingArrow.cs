﻿using UnityEngine;
using System.Collections;

public class PROJ_PiercingArrow : PROJ_Base 
{
    Vector2 ogVelocity;        // original velocity before collision

    protected int hits = 4;       // how many enemies can we pierce?

    public override void Initialize(bool _r = true, float _damMult = 1.0f)
    {
        base.Initialize(_r, _damMult);
        ogVelocity = velocity; // initialize original velocity
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy"
            || collision.gameObject.tag == "Obstacle")
        {
            if (collision.gameObject.GetComponent<ACT_Enemy>().Act_currHP <= 0)
                return;

            collision.gameObject.GetComponent<ACT_Enemy>().ChangeHP(-power);

            power += 5; // MOAR powah!
            hits--;     // losing hull integrity captain!

            velocity = ogVelocity; // reset velocity so that we don't slow down or veer off

            if (hits <= 0 && gameObject != null)
            {
                Destroy(gameObject); // I'll just show myself out
            }
        }
        else
        {
            if (gameObject != null)
                Destroy(gameObject);
        }
    }
}
