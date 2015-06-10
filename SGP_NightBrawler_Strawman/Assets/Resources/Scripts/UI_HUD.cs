﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI_HUD : MonoBehaviour {

	public Canvas theCanvas;
	public GameObject party;
    public GameObject touchPanel;

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

	public Image heldItem;

	public Text potionCount; 


	string[] filePaths;

	ACT_CHAR_Base fighter, ranger, mage;

	// Use this for initialization
	public void Initialize () 
	{
		Cursor.visible = true;

		fighter = party.GetComponent<PlayerController>().party[0];
		ranger = party.GetComponent<PlayerController>().party[1];
		mage = party.GetComponent<PlayerController>().party[2];

		gold = theCanvas.transform.GetChild(4).transform.GetChild(1).GetComponent<Text>();

        if (MNGR_Game.AmIMobile())
            touchPanel.SetActive(true);

		filePaths = new string[11];

		filePaths[0] = "Sprites/GUI/Warrior";
		filePaths[1] = "Sprites/GUI/Archer";
		filePaths[2] = "Sprites/GUI/Mage";
		filePaths[3] = "Sprites/GUI/Dead";
		filePaths[4] = "Sprites/GUI/Nothing";

		filePaths[5] = "Sprites/Item/Health_Potion";
		filePaths[6] = "Sprites/Item/Regen_Potion";
		filePaths[7] = "Sprites/Item/Stamina_Potion";
		filePaths[8] = "Sprites/Item/Acceleration_Potion";
		filePaths[9] = "Sprites/Item/Protection_Potion";
		filePaths[10] = "Sprites/Item/Strength_Potion";

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

		gold.text = "C o i n s : " + MNGR_Game.wallet;

		potionCount.text = "x " + MNGR_Game.theInventory.containers[0].count;

		switch (MNGR_Game.equippedItem)
		{
			case -1:
				heldItem.sprite = Resources.Load<Sprite>(filePaths[4]);
				break;
			case 0:
				heldItem.sprite = Resources.Load<Sprite>(filePaths[5]);
				break;
			case 1:
				heldItem.sprite = Resources.Load<Sprite>(filePaths[6]);
				break;
			case 2:
				heldItem.sprite = Resources.Load<Sprite>(filePaths[7]);
				break;
			case 3:
				heldItem.sprite = Resources.Load<Sprite>(filePaths[8]);
				break;
			case 4:
				heldItem.sprite = Resources.Load<Sprite>(filePaths[9]);
				break;
			case 5:
				heldItem.sprite = Resources.Load<Sprite>(filePaths[10]);
				break;
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
            PauseGame();
		}
	}

	public void ResumeGame()
	{
		MNGR_Game.paused = false;
		party.SetActive(true);

		theCanvas.transform.GetChild(3).gameObject.SetActive(false);
		Cursor.visible = false;
        Input.simulateMouseWithTouches = false;
	}

    public void PauseGame()
    {
        MNGR_Game.paused = true;
        party.SetActive(false);

        theCanvas.transform.GetChild(3).gameObject.SetActive(true);
        Cursor.visible = true;
        Input.simulateMouseWithTouches = true;
    }
}
