﻿using UnityEngine;
using System.Collections;

public class PROJ_Base : MonoBehaviour
{
    public GameObject owner;  // who fired us?

    Vector2 start;            // where are we from?
    public Vector2 velocity;         // which way are we going?
    public int power;         // how much damage will we deal?
    public float distance;    // how far have we gone?

    public float speed;       // how fast are we moving?
    public float range;       // how far can we go?

    public virtual void Initialize(bool _r = true, float _damMult = 1.0f)
    {
        // Are you my mommy?
        if(owner.tag == "Player")
        {
            //owner = GameObject.FindGameObjectWithTag("Player");
            gameObject.layer = 10;

            PlayerController player = owner.GetComponent<PlayerController>();
            int target = player.currChar;

            power += player.party[target].Act_currPower;
            power = (int)(_damMult * (float)power);

            //bool right = player.party[target].Act_facingRight;
            bool right = _r;

            if (right)
                velocity = new Vector2(1, 0);// * speed;
            else
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                velocity = new Vector2(-1, 0);// * speed;
            }
        }
        else if (owner.tag == "Enemy")
        {
            gameObject.layer = 11;

            bool right = owner.GetComponent<ACT_Enemy>().Act_facingRight;

            power += owner.GetComponent<ACT_Enemy>().Act_currPower;
            power = (int)(_damMult * (float)power);

            if (right)
                velocity = new Vector2(1, 0);// * speed;
            else
			{
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                velocity = new Vector2(-1, 0);// *speed;
			}
        }

        //GetComponent<Rigidbody2D>().velocity = velocity;
        start = new Vector2(transform.position.x, transform.position.y);
    }

    public virtual void Update()
    {
		if (MNGR_Game.paused)
			return;

        transform.position += (new Vector3(velocity.x * speed, velocity.y * speed, 0) * Time.deltaTime);

        distance = Mathf.Sqrt((start.x - transform.position.x) * (start.x - transform.position.x));
        if (distance >= range)
            ProjectileExpired();
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("HIT!");
        if (collision.gameObject.tag == "Enemy")
        {
            if (gameObject != null)
                ProjectileExpired();
            collision.gameObject.GetComponent<ACT_Enemy>().ChangeHP(-power);
        }
        else if (collision.gameObject.tag == "Player")
        {
            // Find the active character
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            int target = player.currChar;

            // Mess with the active character
            player.party[target].ChangeHP(-power);

            if (gameObject != null)
                ProjectileExpired();
        }
    }

    protected virtual void ProjectileExpired()
    {
        Destroy(gameObject);
    }


}
