using UnityEngine;
using System.Collections;

public class PROJ_Explosive : PROJ_Base 
{
    public PROJ_Explosion mySplosion;       // the aftershock

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        CreateExplosion();
        base.OnTriggerEnter2D(collision);                                 // start the pain
    }

    void CreateExplosion()
    {
        PROJ_Explosion clone = (PROJ_Explosion)Instantiate(mySplosion, transform.position, new Quaternion(0, 0, 0, 0));
        clone.owner = owner;
        clone.Initialize();
    }
}
