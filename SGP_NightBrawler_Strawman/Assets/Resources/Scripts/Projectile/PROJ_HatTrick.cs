using UnityEngine;
using System.Collections;

public class PROJ_HatTrick : PROJ_PiercingArrow
{
    bool canSpawn = true;

    public override void Initialize(bool _r = true, float _damMult = 1.0f)
    {
        hits = 6;
        base.Initialize(_r, _damMult);

        if (canSpawn)
        {
            GameObject clone = Instantiate<GameObject>(gameObject);
            clone.GetComponent<PROJ_HatTrick>().canSpawn = false;
            clone.transform.position = new Vector2(transform.position.x, transform.position.y + 1.0f);
            clone.GetComponent<PROJ_HatTrick>().Initialize(_r, _damMult);

            clone = Instantiate<GameObject>(gameObject);
            clone.GetComponent<PROJ_HatTrick>().canSpawn = false;
            clone.transform.position = new Vector2(transform.position.x, transform.position.y - 1.0f);
            clone.GetComponent<PROJ_HatTrick>().Initialize(_r, _damMult);
        }
    }

}
