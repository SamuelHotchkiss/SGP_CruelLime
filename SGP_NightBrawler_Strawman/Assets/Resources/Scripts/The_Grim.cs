using UnityEngine;
using System.Collections;

public class The_Grim : MonoBehaviour 
{
	public GameObject boss;
	private ACT_Enemy script;
	private AudioClip victory;
	private bool playMusic = true;
	// Use this for initialization
	void Start () 
	{
		victory = Resources.Load("Audio/Boss_Win") as AudioClip;

		if (boss.GetComponent<ACT_BOS_Ent>())
		{
			script = boss.GetComponent<ACT_BOS_Ent>();
		}
		else if (boss.GetComponent<ACT_BOS_Trollgre>())
		{
			script = boss.GetComponent<ACT_BOS_Trollgre>();
		}
		else if (boss.GetComponent<ACT_BOS_Bipolar>())
		{
			script = boss.GetComponent<ACT_BOS_Bipolar>();
		}
		else if (boss.GetComponent<ACT_BOS_Miner>())
		{
			script = boss.GetComponent<ACT_BOS_Miner>();
		}
		else if (boss.GetComponent<ACT_BOS_Dwagon>())
		{
			script = boss.GetComponent<ACT_BOS_Dwagon>();
		}
		else if (boss.GetComponent<ACT_BOS_Sovalpa>())
		{
			script = boss.GetComponent<ACT_BOS_Sovalpa>();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

		if ((script.Act_currHP <= 0.0f || script.state == ACT_Enemy.STATES.DEAD) && playMusic)
		{
			StartCoroutine("LoadLevel");
			playMusic = false;
		}
	}

	IEnumerator LoadLevel()
	{
		AudioSource.PlayClipAtPoint(victory, new Vector3(0, 0, 0), MNGR_Options.musicVol);
		yield return new WaitForSeconds(5);
		MNGR_Game.UpdateWorld();
		Application.LoadLevel("TransitionScene");
	}
}
