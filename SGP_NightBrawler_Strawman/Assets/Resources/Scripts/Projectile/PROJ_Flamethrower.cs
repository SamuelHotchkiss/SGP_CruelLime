using UnityEngine;
using System.Collections;

public class PROJ_Flamethrower : PROJ_Base
{
    float timer = 1.5f;

    // Use this for initialization
    public override void Initialize(bool _r = true, float _damMult = 1.0f)
    {
        if (owner.tag == "Player")
        {
            gameObject.layer = 10;

            PlayerController player = owner.GetComponent<PlayerController>();
            int target = player.currChar;

            power += player.party[target].Act_currPower;
            power = (int)(_damMult * (float)power);

            if (!_r)
            {
                GetComponentInChildren<ParticleSystem>().startSpeed = -5.0f;
            }

            transform.SetParent(owner.transform);
        }
        else if (owner.tag == "Enemy")
        {
            gameObject.layer = 11;

            //bool right = owner.GetComponent<ACT_Enemy>().Act_facingRight;

            power += owner.GetComponent<ACT_Enemy>().Act_currPower;
			power = (int)(_damMult * (float)power);

            transform.SetParent(owner.transform);

            if (!_r)
            {
                GetComponentInChildren<ParticleSystem>().startSpeed = -5.0f;
            }
        }
    }

    public override void Update()
    {
		if (owner.tag == "Player")
		{
			if (owner.GetComponent<PlayerController>().party[owner.GetComponent<PlayerController>().currChar].Act_facingRight == true)
				GetComponentInChildren<ParticleSystem>().startSpeed = 5.0f;
			else
				GetComponentInChildren<ParticleSystem>().startSpeed = -5.0f;
		}
		else if (owner.tag == "Enemy")
		{
			if (owner.GetComponent<ACT_Enemy>().Act_facingRight == true)
				GetComponentInChildren<ParticleSystem>().startSpeed = 10.0f;
			else
				GetComponentInChildren<ParticleSystem>().startSpeed = -10.0f;
		}

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
        if (other.tag == "Enemy"
            || other.tag == "Obstacle")
        {
            Debug.Log("BURN!");

            other.GetComponent<ACT_Enemy>().ChangeHP(-power);
        }
		else if (other.tag == "Player")
		{
			Debug.Log("BURN!");

			other.GetComponent<PlayerController>().party[other.GetComponent<PlayerController>().currChar].ChangeHP(-power);
		}
    }
}
