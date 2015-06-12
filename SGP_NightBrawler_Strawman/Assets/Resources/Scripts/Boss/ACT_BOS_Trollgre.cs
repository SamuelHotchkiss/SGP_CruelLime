using UnityEngine;
using System.Collections;

public class ACT_BOS_Trollgre : ACT_Enemy {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (target.transform.position.x > transform.position.x)
        {
            Vector2 vel = GetComponent<Rigidbody2D>().velocity;
            vel = new Vector2(Act_currSpeed, vel.y);
            GetComponent<Rigidbody2D>().velocity = vel;
            Act_facingRight = true;
        }
        else
        {
            Vector2 vel = GetComponent<Rigidbody2D>().velocity;
            vel = new Vector2(-Act_currSpeed, vel.y);
            GetComponent<Rigidbody2D>().velocity = vel;
            Act_facingRight = false;
        }
        if (target.transform.position.y > transform.position.y)
        {
            Vector2 vel = GetComponent<Rigidbody2D>().velocity;
            vel = new Vector2(vel.x, Act_currSpeed);
            GetComponent<Rigidbody2D>().velocity = vel;
        }
        else
        {
            Vector2 vel = GetComponent<Rigidbody2D>().velocity;
            vel = new Vector2(vel.x, -Act_currSpeed);
            GetComponent<Rigidbody2D>().velocity = vel;
        }

        switch (state)
        {
            case STATES.IDLE:
                {
                    if (Act_IsIntelligent)
                        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    break;
                }
            case STATES.WALKING:
                {
                    if (target != null)
                    {
                        if (isMelee)
                        {
                            if (target.transform.position.x > transform.position.x)
                            {
                                Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                vel = new Vector2(Act_currSpeed, vel.y);
                                GetComponent<Rigidbody2D>().velocity = vel;
                                Act_facingRight = true;
                            }
                            else
                            {
                                Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                vel = new Vector2(-Act_currSpeed, vel.y);
                                GetComponent<Rigidbody2D>().velocity = vel;
                                Act_facingRight = false;
                            }
                            if (target.transform.position.y > transform.position.y)
                            {
                                Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                vel = new Vector2(vel.x, Act_currSpeed);
                                GetComponent<Rigidbody2D>().velocity = vel;
                            }
                            else
                            {
                                Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                vel = new Vector2(vel.x, -Act_currSpeed);
                                GetComponent<Rigidbody2D>().velocity = vel;
                            }
                            break;
                        }
                        else
                        {
                            if (target.transform.position.x > transform.position.x)
                            {
                                if (distanceToTarget < maxDistance)
                                {
                                    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                    vel = new Vector2(-Act_currSpeed, vel.y);
                                    GetComponent<Rigidbody2D>().velocity = vel;
                                    Act_facingRight = true;
                                }
                                else
                                {
                                    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                    vel = new Vector2(Act_currSpeed, vel.y);
                                    GetComponent<Rigidbody2D>().velocity = vel;
                                    Act_facingRight = true;
                                }
                            }
                            else if (target.transform.position.x < transform.position.x)
                            {
                                if (distanceToTarget < maxDistance)
                                {
                                    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                    vel = new Vector2(Act_currSpeed, vel.y);
                                    GetComponent<Rigidbody2D>().velocity = vel;
                                    Act_facingRight = false;
                                }
                                else
                                {
                                    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                    vel = new Vector2(-Act_currSpeed, vel.y);
                                    GetComponent<Rigidbody2D>().velocity = vel;
                                    Act_facingRight = false;
                                }
                            }
                            if (target.transform.position.y > transform.position.y)
                            {
                                Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                vel = new Vector2(vel.x, Act_currSpeed);
                                GetComponent<Rigidbody2D>().velocity = vel;
                            }
                            else
                            {
                                Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                vel = new Vector2(vel.x, -Act_currSpeed);
                                GetComponent<Rigidbody2D>().velocity = vel;
                            }
                            break;
                        }
                    }
                    break;
                }
            case STATES.RUNNING:
                {
                    if (target != null)
                    {
                        if (isMelee)
                        {
                            SetCurrSpeed(Act_baseSpeed + 1);
                            if (target.transform.position.x > transform.position.x)
                            {
                                Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                vel = new Vector2(Act_currSpeed, vel.y);
                                GetComponent<Rigidbody2D>().velocity = vel;
                                Act_facingRight = true;
                            }
                            else
                            {
                                Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                vel = new Vector2(-Act_currSpeed, vel.y);
                                GetComponent<Rigidbody2D>().velocity = vel;
                                Act_facingRight = false;
                            }
                            if (target.transform.position.y > transform.position.y)
                            {
                                Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                vel = new Vector2(vel.x, Act_currSpeed);
                                GetComponent<Rigidbody2D>().velocity = vel;
                            }
                            else
                            {
                                Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                vel = new Vector2(vel.x, -Act_currSpeed);
                                GetComponent<Rigidbody2D>().velocity = vel;
                            }
                            break;
                        }
                        else
                        {
                            SetCurrSpeed(Act_baseSpeed + 1);
                            if (target.transform.position.x > transform.position.x)
                            {
                                if (distanceToTarget < maxDistance)
                                {
                                    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                    vel = new Vector2(-Act_currSpeed, vel.y);
                                    GetComponent<Rigidbody2D>().velocity = vel;
                                    Act_facingRight = true;
                                }
                                else
                                {
                                    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                    vel = new Vector2(Act_currSpeed, vel.y);
                                    GetComponent<Rigidbody2D>().velocity = vel;
                                    Act_facingRight = true;
                                }
                            }
                            else if (target.transform.position.x < transform.position.x)
                            {
                                if (distanceToTarget < maxDistance)
                                {
                                    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                    vel = new Vector2(Act_currSpeed, vel.y);
                                    GetComponent<Rigidbody2D>().velocity = vel;
                                    Act_facingRight = false;
                                }
                                else
                                {
                                    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                    vel = new Vector2(-Act_currSpeed, vel.y);
                                    GetComponent<Rigidbody2D>().velocity = vel;
                                    Act_facingRight = false;
                                }
                            }
                            if (target.transform.position.y > transform.position.y)
                            {
                                Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                vel = new Vector2(vel.x, Act_currSpeed);
                                GetComponent<Rigidbody2D>().velocity = vel;
                            }
                            else
                            {
                                Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                                vel = new Vector2(vel.x, -Act_currSpeed);
                                GetComponent<Rigidbody2D>().velocity = vel;
                            }
                            break;
                        }
                    }
                    break;
                }
            case STATES.ATTACKING:
                {
                    break;
                }
            case STATES.SPECIAL:
                {
                    break;
                }
            case STATES.HURT:
                {
                    break;
                }
            case STATES.DEAD:
                {
                    break;
                }
        } 
	}
}
