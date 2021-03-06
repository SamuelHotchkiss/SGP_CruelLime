﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UI_HUD : MonoBehaviour
{

    public Canvas theCanvas;
    public GameObject party;
    public GameObject touchPanel;

    public GameObject pausePanel;
    public GameObject optionsPanel;

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


    bool options = false;

    string[] filePaths;
    string[] orgFilePaths;

    ACT_CHAR_Base fighter, ranger, mage;

    // Use this for initialization
    public void Initialize()
    {
        MNGR_Game.Initialize();
        MNGR_Options.Initialize();

        Cursor.visible = true;

		//if (MNGR_Options.colorblind)
		//{
		//	fighterCooldown.sprite = mageCooldown.sprite = rangerCooldown.sprite = Resources.Load<Sprite>("Sprites/GUI/Cooldown_GUI_blind");
		//}

        fighter = party.GetComponent<PlayerController>().party[0];
        ranger = party.GetComponent<PlayerController>().party[1];
        mage = party.GetComponent<PlayerController>().party[2];

        gold = theCanvas.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>();

        if (MNGR_Game.AmIMobile())
            touchPanel.SetActive(true);

        filePaths = new string[17];

        filePaths[0] = "Sprites/GUI/Port_Sword";
        filePaths[1] = "Sprites/GUI/Port_Lancer";
        filePaths[2] = "Sprites/GUI/Port_Defender";

        filePaths[3] = "Sprites/GUI/Port_Archer";
        filePaths[4] = "Sprites/GUI/Port_Ninja";
        filePaths[5] = "Sprites/GUI/Port_Poisoner";

        filePaths[6] = "Sprites/GUI/Port_Wizard";
        filePaths[7] = "Sprites/GUI/Port_ForceMage";
        filePaths[8] = "Sprites/GUI/Port_Spellslinger";

        filePaths[9] = "Sprites/GUI/Dead";
        filePaths[10] = "Sprites/GUI/Nothing";

        filePaths[11] = "Sprites/Item/Health_Potion";
        filePaths[12] = "Sprites/Item/Regen_Potion";
        filePaths[13] = "Sprites/Item/Stamina_Potion";
        filePaths[14] = "Sprites/Item/Acceleration_Potion";
        filePaths[15] = "Sprites/Item/Protection_Potion";
        filePaths[16] = "Sprites/Item/Strength_Potion";

        orgFilePaths = new string[9];

        for (int i = 0; i < 9; i++)
        {
            orgFilePaths[i] = filePaths[i];
        }

        if (MNGR_Options.colorblind)
        {
            for (int i = 0; i < 9; i++)
            {
                filePaths[i] += "_blind";
            }
        }

        fighter.Start();
        ranger.Start();
        mage.Start();
    }

    // Update is called once per frame
    void Update()
    {

        if (MNGR_Game.paused)
            if (GameObject.Find("LevelDJ"))
                GameObject.Find("LevelDJ").GetComponent<AudioSource>().volume = MNGR_Options.musicVol;

        fighterHealth.fillAmount = ((float)fighter.Act_currHP / (float)fighter.Act_baseHP);
        fighterCooldown.fillAmount = (float)((fighter.cooldownTmrBase - fighter.cooldownTmr) / fighter.cooldownTmrBase);

        rangerHealth.fillAmount = ((float)ranger.Act_currHP / (float)ranger.Act_baseHP);
        rangerCooldown.fillAmount = (float)((ranger.cooldownTmrBase - ranger.cooldownTmr) / ranger.cooldownTmrBase);

        mageHealth.fillAmount = ((float)mage.Act_currHP / (float)mage.Act_baseHP);
        mageCooldown.fillAmount = (float)((mage.cooldownTmrBase - mage.cooldownTmr) / mage.cooldownTmrBase);

        if (fighter.Act_currHP > 0)
        {
            fighterPort.sprite = Resources.Load<Sprite>(filePaths[fighter.characterIndex]);
        }
        else
            fighterPort.sprite = Resources.Load<Sprite>(filePaths[9]);

        if (ranger.Act_currHP > 0)
            rangerPort.sprite = Resources.Load<Sprite>(filePaths[ranger.characterIndex]);
        else
            rangerPort.sprite = Resources.Load<Sprite>(filePaths[9]);

        if (mage.Act_currHP > 0)
            magePort.sprite = Resources.Load<Sprite>(filePaths[mage.characterIndex]);
        else
            magePort.sprite = Resources.Load<Sprite>(filePaths[9]);

        fighter.Update();
        ranger.Update();
        mage.Update();

        gold.text = "C o i n s : " + MNGR_Game.wallet;

        potionCount.text = "x " + MNGR_Game.theInventory.containers[0].count;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        switch (MNGR_Game.equippedItem)
        {
            case -1:
                heldItem.sprite = Resources.Load<Sprite>(filePaths[10]);
                break;
            case 0:
                heldItem.sprite = Resources.Load<Sprite>(filePaths[11]);
                break;
            case 1:
                heldItem.sprite = Resources.Load<Sprite>(filePaths[12]);
                break;
            case 2:
                heldItem.sprite = Resources.Load<Sprite>(filePaths[13]);
                break;
            case 3:
                heldItem.sprite = Resources.Load<Sprite>(filePaths[14]);
                break;
            case 4:
                heldItem.sprite = Resources.Load<Sprite>(filePaths[15]);
                break;
            case 5:
                heldItem.sprite = Resources.Load<Sprite>(filePaths[16]);
                break;
            default:
                break;
        }
    }

    public void TogglePause()
    {
		if (options)
		{
			ToggleOptions();
			return;
		}

        MNGR_Game.paused = !MNGR_Game.paused;
        //party.SetActive(!MNGR_Game.paused);
        if (!MNGR_Game.paused)
        {
            Time.timeScale = 1.0f;
            Input.simulateMouseWithTouches = false;
        }
        else
        {
            Time.timeScale = 0;
            Input.simulateMouseWithTouches = true;
        }

        pausePanel.gameObject.SetActive(MNGR_Game.paused);
        //Cursor.visible = !Cursor.visible;
        //Input.simulateMouseWithTouches = !Input.simulateMouseWithTouches;

        if (MNGR_Options.colorblind)
        {
            for (int i = 0; i < 9; i++)
            {
                filePaths[i] = orgFilePaths[i] + "_blind";
            }
        }
        else
        {
            for (int i = 0; i < 9; i++)
            {
                filePaths[i] = orgFilePaths[i];
            }
        }
    }

    public void ToggleOptions()
    {
        options = !options;
        optionsPanel.gameObject.SetActive(options);
        //MNGR_Save.optionsFile.CopyOptions(); // S: redundant SaveOptions() already does this
        MNGR_Save.SaveOptions();
    }

    public void ExitGame()
    {
        MNGR_Game.paused = false;
        pausePanel.gameObject.SetActive(false);
        //Input.simulateMouseWithTouches = false;
        Time.timeScale = 1.0f;

        MNGR_Save.saveFiles[MNGR_Save.currSave].AssignGameManager();

        Application.LoadLevel("WorldMap");
    }
}
