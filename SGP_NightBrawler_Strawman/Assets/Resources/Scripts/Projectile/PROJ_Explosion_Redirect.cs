using UnityEngine;
using System.Collections;

public class PROJ_Explosion_Redirect : PROJ_Explosion
{
    public override void Initialize(bool _r = true)
    {
        base.Initialize(_r);
        gameObject.layer = 14;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Projectile")
        {
            collision.gameObject.GetComponent<PROJ_Base>().velocity =
                -collision.gameObject.GetComponent<PROJ_Base>().velocity;
            collision.gameObject.layer = 10;
        }
    }

}
