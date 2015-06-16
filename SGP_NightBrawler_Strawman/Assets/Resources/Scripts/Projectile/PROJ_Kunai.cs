using UnityEngine;
using System.Collections;

public class PROJ_Kunai : PROJ_Base {

	// Use this for initialization
	public override void Initialize(bool _r = true, float _damMult = 1.0f)
	{
		base.Initialize(_r, _damMult);
	}

	public override void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerController player = owner.GetComponent<PlayerController>();
		Debug.Log("HIT!");
		if (collision.gameObject.tag == "Enemy"
			|| collision.gameObject.tag == "Obstacle")
		{
			if (collision.gameObject.GetComponent<ACT_Enemy>().Act_facingRight == right || (GameObject.FindGameObjectWithTag("Decoy") && player.party[player.currChar].hasSpecial))
			{
				collision.gameObject.GetComponent<ACT_Enemy>().ChangeHP(-power * 4);
			}
			else
			{
				collision.gameObject.GetComponent<ACT_Enemy>().ChangeHP(-power);
			}
			if (gameObject != null)
				ProjectileExpired();
		}
	}
}
