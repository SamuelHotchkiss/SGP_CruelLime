using UnityEngine;
using System.Collections;

public class PROJ_PoisonSplash : PROJ_Explosion
{

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        AttachDebuff(collision, 8); // Attach poison and stun maybe.
        AttachDebuff(collision, 10);

    }

    void AttachDebuff(Collider2D collision, int debuff_ID)
    {

        if (collision.gameObject.tag == "Enemy"
            || collision.gameObject.tag == "Obstacle")
        {
            ACT_Enemy enemy = collision.gameObject.GetComponent<ACT_Enemy>();
            //enemy.ChangeHP(-power);

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
                //player.party[target].ChangeHP(-power);

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
    }


}
