using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class MENU_Defense : MonoBehaviour {

	// Use this for initialization
    public int Def_Cost;
    public int Def_UpgradesNum;
    public Text Def_ButtonText;
    public Text Def_CoinNum;

    void Start()
    {
        if (MNGR_Game.arrowPos == 2)
            Def_UpgradesNum = MNGR_Game.HordeDelayVllOne - 1;
        else if (MNGR_Game.arrowPos == 8)
            Def_UpgradesNum = MNGR_Game.HordeDelayVllTwo - 1;
        else if (MNGR_Game.arrowPos == 14)
            Def_UpgradesNum = MNGR_Game.HordeDelayVllThree - 1;
    }

    void Update()
    {
        Def_CoinNum.text = "X" + MNGR_Game.wallet.ToString();
        Def_Cost = (5 * MNGR_Game.hordePosition) * (1 + Def_UpgradesNum);
        if (Def_UpgradesNum < 3)
            Def_ButtonText.text = "Days Delay: " + Def_UpgradesNum.ToString() + "\nCost: " + Def_Cost.ToString();
        else
            Def_ButtonText.text = "Days Delay: " + Def_UpgradesNum.ToString() + "\nMax Days Reach";
    }

    public void DealayHoard()
    {
        if (MNGR_Game.wallet >= Def_Cost && Def_UpgradesNum < 3)
        {
            MNGR_Game.wallet -= Def_Cost;
            Def_UpgradesNum++;
            if (MNGR_Game.arrowPos == 2) 
                MNGR_Game.HordeDelayVllOne++;
            else if (MNGR_Game.arrowPos == 8)
                MNGR_Game.HordeDelayVllTwo++;
            else if (MNGR_Game.arrowPos == 14)
                MNGR_Game.HordeDelayVllThree++;
        }
    }
}
