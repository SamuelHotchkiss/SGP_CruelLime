using UnityEngine;
using System.Collections;

public class PROJ_Flamethrower : PROJ_Base
{
    float timer = 1.5f;

    // Use this for initialization
    public override void Initialize(bool _r = true, float _damMult = 1.0f)
    {
        if(owner.tag == "Player")
        {
            gameObject.layer = 10;

            PlayerController player = owner.GetComponent<PlayerController>();
            int target = player.currChar;

            power += player.party[target].Act_currPower;

            if(!_r)
            {
                GetComponentInChildren<ParticleSystem>().startSpeed = -5.0f;
            }

            transform.SetParent(owner.transform);
        }
        else if (owner.tag == "Enemy")
        {
            gameObject.layer = 11;

            bool right = owner.GetComponent<ACT_Enemy>().Act_facingRight;

            power += owner.GetComponent<ACT_Enemy>().Act_currPower;

            transform.SetParent(owner.transform);
        }
    }

    public override void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
            ProjectileExpired();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        //base.OnTriggerEnter2D(collision);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("BURN!");

            other.GetComponent<ACT_Enemy>().ChangeHP(-power);
        }
    }
}
