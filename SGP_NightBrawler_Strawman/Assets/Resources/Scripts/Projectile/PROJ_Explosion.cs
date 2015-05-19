using UnityEngine;
using System.Collections;

public class PROJ_Explosion : PROJ_Base
{
    public float timer;
    public float radius;                    // screw you and everyone you knew and loved

    public override void Initialize()
    {
        speed = 0;

        base.Initialize();
    }

	// Update is called once per frame
	void Update () 
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            Destroy(gameObject);
	}

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<ACT_Enemy>().ChangeHP(-power);
        }
        else if (collision.gameObject.tag == "Player")
        {
            // Find the active character
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            int target = player.currChar;

            // Mess with the active character
            player.party[target].ChangeHP(-power);
        }
    }
}
