using UnityEngine;
using System.Collections;

public class The_Grim : MonoBehaviour 
{
	private AudioClip victory;
	private bool playMusic = true;
	// Use this for initialization
	void Start () 
	{
		victory = Resources.Load("Audio/Music/Boss_Win") as AudioClip;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ((!GameObject.FindGameObjectWithTag("Enemy")) && playMusic)
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
		MNGR_Game.NextLevel = "WorldMap";
		Application.LoadLevel("TransitionScene");
	}
}
