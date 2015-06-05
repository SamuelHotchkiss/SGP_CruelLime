using UnityEngine;
using System.Collections;

public class PROJ_Decoy : PROJ_Base 
{
	public float decoyTimer;

	// Use this for initialization
	public override void Initialize(bool _r = true, float _damMult = 1.0f)
	{
		base.Initialize(_r, _damMult);

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		for (int i = 0; i < enemies.Length; i++)
		{
			enemies[i].GetComponent<ACT_Enemy>().target = gameObject;
		}

		owner.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);

		decoyTimer = 10.0f;
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		decoyTimer -= Time.deltaTime;

		if (decoyTimer <= 0.0f)
		{
			ProjectileExpired();
		}
	}

	protected override void ProjectileExpired()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		for (int i = 0; i < enemies.Length; i++)
		{
			enemies[i].GetComponent<ACT_Enemy>().target = GameObject.FindGameObjectWithTag("Player");
		}

		owner.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);


		Destroy(gameObject);
	}

}
