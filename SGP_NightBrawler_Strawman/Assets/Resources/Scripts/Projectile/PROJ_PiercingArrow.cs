using UnityEngine;
using System.Collections;

public class PROJ_PiercingArrow : PROJ_Base 
{
    Vector2 ogVelocity;        // original velocity before collision

    public int hits = 4;       // how many enemies can we pierce?

    public override void Initialize()
    {
        base.Initialize();
        ogVelocity = velocity; // initialize original velocity
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<ACT_Enemy>().ChangeHP(-power);

            power += 5; // MOAR powah!
            hits--;     // losing hull integrity captain!

            GetComponent<Rigidbody2D>().velocity = ogVelocity; // reset velocity so that we don't slow down or veer off

            if (hits <= 0)
                Destroy(gameObject); // I'll just show myself out
        }
        else
            Destroy(gameObject);
    }
}
