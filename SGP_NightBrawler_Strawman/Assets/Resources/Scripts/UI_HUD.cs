using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI_HUD : MonoBehaviour {

	public Canvas theCanvas;
	public GameObject party;

	public Image fighterHealth;
	public Image fighterCooldown;

	public Image rangerHealth;
	public Image rangerCooldown;

	public Image mageHealth;
	public Image mageCooldown;

	ACT_CHAR_Base fighter, ranger, mage;

	// Use this for initialization
	public void Initialize () 
	{
		Cursor.visible = false;

		fighter = party.GetComponent<PlayerController>().party[0];
		ranger = party.GetComponent<PlayerController>().party[1];
		mage = party.GetComponent<PlayerController>().party[2];

		fighter.Start();
		ranger.Start();
		mage.Start();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		fighterHealth.fillAmount = (float)(fighter.Act_currHP / fighter.Act_baseHP);
		fighterCooldown.fillAmount = (float)((fighter.cooldownTmrBase - fighter.cooldownTmr) / fighter.cooldownTmrBase);

		rangerHealth.fillAmount = (float)(ranger.Act_currHP / ranger.Act_baseHP);
		rangerCooldown.fillAmount = (float)((ranger.cooldownTmrBase - ranger.cooldownTmr) / ranger.cooldownTmrBase);

		mageHealth.fillAmount = (float)(mage.Act_currHP / mage.Act_baseHP);
		mageCooldown.fillAmount = (float)((mage.cooldownTmrBase - mage.cooldownTmr) / mage.cooldownTmrBase);


		fighter.Update();
		ranger.Update();
		mage.Update();



		if (Input.GetKeyDown(KeyCode.Escape))
		{
			party.SetActive(false);
			theCanvas.transform.GetChild(3).gameObject.SetActive(true);
			Cursor.visible = true;
		}
	}

	public void ResumeGame()
	{
		party.SetActive(true);
		theCanvas.transform.GetChild(3).gameObject.SetActive(false);
		Cursor.visible = false;
	}
}
