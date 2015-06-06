using UnityEngine;
using System.Collections;

public class PROJ_Explosive : PROJ_Base
{
    public int NumSpawnOnDeath;
    public PROJ_Explosion SpawnOnDeath;       // the aftershock
    public Sprite[] sprites;
    int curSprite = 0;
    float updateSprite = 0.0f;
	public string spriteName;

    public override void Initialize(bool _r = true, float _damMult = 1.0f)
    {
        base.Initialize(_r, _damMult);
        sprites = Resources.LoadAll<Sprite>("Sprites/Projectile/" + spriteName);
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
