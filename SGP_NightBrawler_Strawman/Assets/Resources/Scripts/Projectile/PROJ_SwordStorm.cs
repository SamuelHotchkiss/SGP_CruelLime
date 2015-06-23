using UnityEngine;
using System.Collections;

public class PROJ_SwordStorm : PROJ_Base
{
	float spawnTimerBase = 0.2f;
	float spawnTimer;
	float timer = 2.0f;
	public PROJ_Base sword;

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

			transform.SetParent(owner.transform);
		}
        else if (owner.tag == "Enemy")
        {
            gameObject.layer = 11;

            //bool right = owner.GetComponent<ACT_Enemy>().Act_facingRight;

            power += owner.GetComponent<ACT_Enemy>().Act_currPower;
            power = (int)(_damMult * (float)power);

            transform.SetParent(owner.transform);

        }

		//spawnTimer = spawnTimerBase;
	}

	public override void Update()
	{
		if (owner.tag == "Player")
		{
			if (spawnTimer > 0.0f)
			{
				spawnTimer -= Time.deltaTime;
			}
			if (spawnTimer <= 0.0f)
			{
				PROJ_Base newSword = Instantiate(sword, transform.position, Quaternion.identity) as PROJ_Base;
				newSword.owner = owner;
				newSword.Initialize();

				Vector2 randVec = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
				while (randVec.x == 0 && randVec.y == 0)
					randVec = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
				
				newSword.velocity = randVec;

				if (randVec.x == 1 && randVec.y == 0)
					newSword.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
				else if (randVec.x == 1 && randVec.y == 1)
					newSword.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 45.0f);
				else if (randVec.x == 0 && randVec.y == 1)
					newSword.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
				else if (randVec.x == -1 && randVec.y == 1)
					newSword.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 135.0f);
				else if (randVec.x == -1 && randVec.y == 0)
					newSword.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
				else if (randVec.x == -1 && randVec.y == -1)
					newSword.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 225.0f);
				else if (randVec.x == 0 && randVec.y == -1)
					newSword.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 270.0f);
				else if (randVec.x == 1 && randVec.y == -1)
					newSword.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 315.0f);

				spawnTimer = spawnTimerBase;
			}


			PlayerController player = owner.GetComponent<PlayerController>();
			if (player.currentState != ACT_CHAR_Base.STATES.SPECIAL)
			{
				ProjectileExpired();
			}
		}
        else if (owner.tag == "Enemy")
        {
            if (spawnTimer > 0.0f)
            {
                spawnTimer -= Time.deltaTime;
            }
            if (spawnTimer <= 0.0f)
            {
                PROJ_Base newSword = Instantiate(sword, transform.position, Quaternion.identity) as PROJ_Base;
                newSword.owner = owner;
                newSword.Initialize();

                Vector2 randVec = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
                while (randVec.x == 0 && randVec.y == 0)
                    randVec = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));

                newSword.velocity = randVec;

                if (randVec.x == 1 && randVec.y == 0)
                    newSword.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                else if (randVec.x == 1 && randVec.y == 1)
                    newSword.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 45.0f);
                else if (randVec.x == 0 && randVec.y == 1)
                    newSword.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                else if (randVec.x == -1 && randVec.y == 1)
                    newSword.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 135.0f);
                else if (randVec.x == -1 && randVec.y == 0)
                    newSword.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
                else if (randVec.x == -1 && randVec.y == -1)
                    newSword.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 225.0f);
                else if (randVec.x == 0 && randVec.y == -1)
                    newSword.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 270.0f);
                else if (randVec.x == 1 && randVec.y == -1)
                    newSword.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 315.0f);

                spawnTimer = spawnTimerBase;
            }
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
}
