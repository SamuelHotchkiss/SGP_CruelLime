using UnityEngine;
using System.Collections;

public class PROJ_Explosion : PROJ_Base
{
    public float timer;
    public float radius;                    // screw you and everyone you knew and loved
    public float forcestr;
    public Vector2 forcedir;

    public override void Initialize()
    {
        speed = 0;

        // flip the circle collider if the owner faces the other way
        if (!owner.GetComponent<PlayerController>().party[owner.GetComponent<PlayerController>().currChar].Act_facingRight)
        {
            Vector2 offset = GetComponent<CircleCollider2D>().offset;
            offset.x = -offset.x;
            GetComponent<CircleCollider2D>().offset = offset;
        }


        base.Initialize();

        power = (int)((float)power * 0.75f);
    }

	// Update is called once per frame
	void Update () 
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            Destroy(gameObject);
	}

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<ACT_Enemy>().ChangeHP(-power);

            if (forcestr > 0)
            {
                Vector2 ownpos;
                ownpos.x = owner.transform.position.x;
                ownpos.y = owner.transform.position.y;
                Vector2 colpos;
                colpos.x = collision.gameObject.transform.position.x;
                colpos.y = collision.gameObject.transform.position.y;

                forcedir = colpos - ownpos;

                collision.gameObject.GetComponent<ACT_Enemy>().ApplyKnockBack( forcedir * forcestr);
            }
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
