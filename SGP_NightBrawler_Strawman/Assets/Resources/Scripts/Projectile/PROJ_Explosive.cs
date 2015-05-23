﻿using UnityEngine;
using System.Collections;

public class PROJ_Explosive : PROJ_Base
{
    public int NumSpawnOnDeath;
    public PROJ_Explosion SpawnOnDeath;       // the aftershock
    public Sprite[] sprites;
    int curSprite = 0;
    float updateSprite = 0.0f;

    public override void Initialize()
    {
        base.Initialize();
        sprites = Resources.LoadAll<Sprite>("Sprites/Projectile/Fireball");
    }

    public override void Update()
    {
        if (updateSprite > 0)
            updateSprite -= Time.deltaTime;
        else
        {
            updateSprite = 0.075f;
            curSprite++;
            if (curSprite > 2)
                curSprite = 0;
            GetComponent<SpriteRenderer>().sprite = sprites[curSprite];
        }

        base.Update();
    }

    protected override void ProjectileExpired()
    {
        CreateExplosion();
        base.ProjectileExpired();
    }

    void CreateExplosion()
    {
        for (int i = 0; i < NumSpawnOnDeath; i++)
        {
            PROJ_Explosion clone = (PROJ_Explosion)Instantiate(SpawnOnDeath, transform.position, new Quaternion(0, 0, 0, 0));
            clone.owner = owner;
            clone.Initialize();
        }
    }

}
