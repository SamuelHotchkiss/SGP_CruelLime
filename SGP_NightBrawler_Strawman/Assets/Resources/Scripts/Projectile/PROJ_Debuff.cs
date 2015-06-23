using UnityEngine;
using System.Collections;

public class PROJ_Debuff : PROJ_Base
{

    public int debuff_ID;
    public PROJ_Base[] SpawnOnDeath; // last second add for poisoner.

    // Use this for initialization
    public override void Initialize(bool _r = true, float _damMult = 1.0f)
    {
        base.Initialize(_r, _damMult);
    }

    // Update is called once per frame
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("HIT!");
        if (collision.gameObject.tag == "Enemy") //|| collision.gameObject.tag == "Obstacle"
        {
            ACT_Enemy enemy = collision.gameObject.GetComponent<ACT_Enemy>();

            if (enemy.Act_currHP <= 0)
                return;

            enemy.ChangeHP(-power);

            bool existing = false;
            for (int i = 0; i < enemy.myBuffs.Count; i++)
            {
                if (enemy.myBuffs[i].Mod_ModIndexNum == debuff_ID)
                {
                    enemy.myBuffs[i].EndModifyEnemy();
                    MNGR_Item.AttachModifier(debuff_ID, collision.gameObject);
                    existing = true;
                }
            }
            if (!existing)
            {
                MNGR_Item.AttachModifier(debuff_ID, collision.gameObject);
            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            // Find the active character
            if (collision.gameObject.GetComponent<PlayerController>() != null)
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                int target = player.currChar;

                // Mess with the active character
                player.party[target].ChangeHP(-power);

                // Attach the Debuff
                bool existing = false;
                for (int i = 0; i < player.myBuffs.Count; i++)
                {
                    if (player.myBuffs[i].Mod_ModIndexNum == debuff_ID)
                    {
                        player.myBuffs[i].EndModifyActor();
                        MNGR_Item.AttachModifier(debuff_ID, collision.gameObject);
                        existing = true;
                    }
                }
                if (!existing)
                {
                    MNGR_Item.AttachModifier(debuff_ID, collision.gameObject);
                }
            }

        }

        if (SpawnOnDeath.Length > 0)
        {
            for (int i = 0; i < SpawnOnDeath.Length; i++)
            {
                PROJ_Explosion clone = (PROJ_Explosion)Instantiate(SpawnOnDeath[i], transform.position, new Quaternion(0, 0, 0, 0));
                clone.owner = owner;
                clone.Initialize();
            }
        }
        ProjectileExpired();

    }
}
