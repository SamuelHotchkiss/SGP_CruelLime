using UnityEngine;
using System.Collections;

public class PROJ_DefenderWallMaster : PROJ_Base
{
    public float TmrSpawn;
    float TmrSpawnOrg;
    //public float TmrTot;
    //float TmrTotOrg;
    public float SpawnHorzOff;
    public float SpawnVertOff;
    int TierSpawned;            // Since we spawn in several stages, we need to know which stage we're in.
    public PROJ_Base Wall;      // The defender's original walls.
    public PROJ_Base Projectile;    // The master version's pillars

	// Use THIS for initialization
    public override void Initialize(bool _r = true, float _damMult = 1.0f)
    {

        base.Initialize(_r, _damMult);

        if (owner == null)
            return;

        gameObject.layer = owner.layer;
        gameObject.tag = owner.tag;

        TmrSpawnOrg = TmrSpawn;
        //TmrTotOrg = TmrTot;


        // Spawn two walls here.
        PROJ_Base clone = (PROJ_Base)Instantiate(Wall, transform.position, new Quaternion(0, 0, 0, 0));
        clone.owner = owner;
        Vector3 newpos = transform.position;
        newpos.x += SpawnHorzOff * 0.01f;
        clone.transform.position = newpos;
        clone.Initialize();

        clone = (PROJ_Base)Instantiate(Wall, transform.position, new Quaternion(0, 0, 0, 0));
        clone.owner = owner;
        newpos = transform.position;
        newpos.x -= SpawnHorzOff * 0.01f;
        clone.transform.position = newpos;
        clone.Initialize(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //TmrTot -= Time.deltaTime;
        TmrSpawn -= Time.deltaTime;



        if (TmrSpawn < 0)
        {
            TmrSpawn = TmrSpawnOrg;
            switch (TierSpawned)
            {
                case 0:
                    SpawnProj(SpawnHorzOff * 3, 0);
                    SpawnProj(-SpawnHorzOff * 3, 0);
                    break;
                case 1:
                    SpawnProj(SpawnHorzOff * 2.5f, SpawnVertOff);
                    SpawnProj(SpawnHorzOff * 2.5f, -SpawnVertOff);
                    SpawnProj(-SpawnHorzOff * 2.5f, SpawnVertOff);
                    SpawnProj(-SpawnHorzOff * 2.5f, -SpawnVertOff);
                    ProjectileExpired();
                    break;
            }
            TierSpawned++;
        }

        //if (TmrTot < 0)
            //ProjectileExpired();
	}

    void SpawnProj(float _xOff, float _yOff) // spawn pillars here.
    {
        PROJ_Base clone = (PROJ_Base)Instantiate(Projectile, transform.position, new Quaternion(0, 0, 0, 0));
        clone.owner = owner;

        Vector3 newpos = clone.transform.position;
        newpos.x += _xOff;
        newpos.y += _yOff;
        clone.transform.position = newpos;

        if (_xOff > 0)
            clone.Initialize();
        else
            clone.Initialize(false);

    }


}
