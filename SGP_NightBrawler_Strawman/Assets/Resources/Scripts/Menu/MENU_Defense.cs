using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class MENU_Defense : MonoBehaviour {

	// Use this for initialization
    public int Def_Cost;
    public int Def_UpgradesNum;

    public Text Def_ButtonText;
    public Text Def_CoinNum;

    void Update()
    {
        Def_CoinNum.text = "X" + MNGR_Game.wallet.ToString();
        Def_Cost = (50 * MNGR_Game.hordePosition) * (1 + (Def_UpgradesNum * MNGR_Game.hordePosition));
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
            MNGR_Game.HordeDelay++;
        }
    }
}
