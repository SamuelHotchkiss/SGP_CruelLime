using UnityEngine;
using System.Collections;

public class BHR_Buffer : BHR_Base
{


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		base.Update();
	}

	public override void PerformBehavior()
	{

		owner.GetComponent<ACT_Enemy>().squad.Clear();

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		for (int i = 0; i < enemies.Length; i++)
			if (Mathf.Abs(enemies[i].transform.position.x - transform.position.x) < owner.GetComponent<ACT_Enemy>().maxBuffRange)
				owner.GetComponent<ACT_Enemy>().squad.Add(enemies[i]);

		for (int i = 0; i < owner.GetComponent<ACT_Enemy>().squad.Count; i++)
		{
            // S: completely revamped modifier classes, hopefully will make this section a little easier to code, right now is broken :(

            //owner.GetComponent<ACT_Enemy>().buff.GetComponent<MOD_Base>().enemy = owner.GetComponent<ACT_Enemy>().squad[i].GetComponent<ACT_Enemy>();
            //if (!owner.GetComponent<ACT_Enemy>().buff.GetComponent<MOD_Base>().NullNewEffectsEnemy())
            //{
            //    owner.GetComponent<ACT_Enemy>().buff.GetComponent<MOD_Base>().enemy.Act_ModIsBuff = true;
            //    owner.GetComponent<ACT_Enemy>().buff.GetComponent<MOD_Base>().SetModEffectEnemy(true, owner.GetComponent<ACT_Enemy>().buffIndex);
            //}

            // S: Ideally this should be all you need
            if(owner.squad[i].GetComponent<ACT_Enemy>().buffState != MNGR_Item.BuffStates.BUFFED)
                MNGR_Item.AttachModifier(owner.buffIndex, owner.squad[i]);
		}

		// Debug.Log("Buffer Activated!");
	}
}
