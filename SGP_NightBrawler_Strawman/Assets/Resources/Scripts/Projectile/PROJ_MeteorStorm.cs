using UnityEngine;
using UnityEngine.Sprites;
using System.Collections;

public class PROJ_MeteorStorm : PROJ_Base
{

    public PROJ_Base Projectile;
    public float Duration;
    float OrgFreq;
    public float SpawnFreq;

    void Start()
    {
        OrgFreq = SpawnFreq;

        Vector3 newpos = transform.position;
        newpos.x -= 3.0f;
        newpos.y += 7.0f;
        transform.position = newpos;

    }

    public override void Update()
    {
        if (Duration > 0)
        {
            Duration -= Time.deltaTime;
            if (Duration < 0)
            {
                ProjectileExpired();
            }

        }

        if (SpawnFreq > 0)
        {
            SpawnFreq -= Time.deltaTime;
            if (SpawnFreq < 0)
            {
                SpawnFreq = OrgFreq + Random.Range(0.0f, 0.5f);
                SpawnProj();
            }
        }

    }

    void SpawnProj()
    {
        PROJ_Base clone = (PROJ_Base)Instantiate(Projectile, transform.position, new Quaternion(0, 0, 0, 0));
        clone.owner = owner;

        float range = 4.0f;
        Vector3 pos = clone.transform.position;
        pos.x += Random.Range(-range, range);
        pos.y += Random.Range(-range * 0.5f, range * 0.5f);
        clone.transform.position = pos;

        clone.Initialize();

        Vector3 rot = clone.transform.localEulerAngles;
        rot.z = 290.0f;
        clone.transform.localEulerAngles = rot;
        clone.velocity = new Vector2(0.4f, -1.0f);

        clone.range *= 1.5f;
        

    }



}
