using UnityEngine;
using System.Collections;

public class PROJ_DefenderWall : PROJ_Base
{
    
	// Use THIS for initialization
    public override void Initialize(bool _r = true, float _damMult = 1.0f)
    {
        float dir = 1.0f;
        if (!_r)
            dir = -1.0f;

        Vector3 newpos = transform.position;
        newpos.x += 1.5f * dir;
        transform.position = newpos;

        base.Initialize(_r, _damMult);

        if (owner == null)
            return;

        gameObject.layer = owner.layer;
        gameObject.tag = owner.tag;

    }
	
	// Update is called once per frame
	/*void Update ()
    {
	
	}*/
}
