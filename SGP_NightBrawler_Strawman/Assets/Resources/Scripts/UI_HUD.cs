using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI_HUD : MonoBehaviour {

	public Canvas theCanvas;
	public GameObject party;

	public Image fighterPort;
	public Image rangerPort;
	public Image magePort;

	public Image fighterHealth;
	public Image fighterCooldown;

	public Image rangerHealth;
	public Image rangerCooldown;

	public Image mageHealth;
	public Image mageCooldown;

	public Text gold;

	string[] filePaths;

	ACT_CHAR_Base fighter, ranger, mage;

	// Use this for initialization
	public void Initialize () 
	{
		Cursor.visible = false;

		fighter = party.GetComponent<PlayerController>().party[0];
		ranger = party.GetComponent<PlayerController>().party[1];
		mage = party.GetComponent<PlayerController>().party[2];

		gold = theCanvas.transform.GetChild(4).transform.GetChild(1).GetComponent<Text>();

		filePaths = new string[4];

		filePaths[0] = "Sprites/GUI/Warrior";
		filePaths[1] = "Sprites/GUI/Archer";
		filePaths[2] = "Sprites/GUI/Mage";
		filePaths[3] = "Sprites/GUI/Dead";

		fighter.Start();
		ranger.Start();
		mage.Start();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		fighterHealth.fillAmount = ((float)fighter.Act_currHP / (float)fighter.Act_baseHP);
		fighterCooldown.fillAmount = (float)((fighter.cooldownTmrBase - fighter.cooldownTmr) / fighter.cooldownTmrBase);

		rangerHealth.fillAmount = ((float)ranger.Act_currHP / (float)ranger.Act_baseHP);
		rangerCooldown.fillAmount = (float)((ranger.cooldownTmrBase - ranger.cooldownTmr) / ranger.cooldownTmrBase);

		mageHealth.fillAmount = ((float)mage.Act_currHP / (float)mage.Act_baseHP);
		mageCooldown.fillAmount = (float)((mage.cooldownTmrBase - mage.cooldownTmr) / mage.cooldownTmrBase);

		if (fighter.Act_currHP > 0)
			fighterPort.sprite = Resources.Load<Sprite>(filePaths[0]);
		else
			fighterPort.sprite = Resources.Load<Sprite>(filePaths[3]);

		if (ranger.Act_currHP > 0)
			rangerPort.sprite = Resources.Load<Sprite>(filePaths[1]);
		else
			rangerPort.sprite = Resources.Load<Sprite>(filePaths[3]);

		if (mage.Act_currHP > 0)
			magePort.sprite = Resources.Load<Sprite>(filePaths[2]);
		else
			magePort.sprite = Resources.Load<Sprite>(filePaths[3]);

		fighter.Update();
		ranger.Update();
		mage.Update();

		gold.text = "Coins: " + MNGR_Game.wallet;

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
