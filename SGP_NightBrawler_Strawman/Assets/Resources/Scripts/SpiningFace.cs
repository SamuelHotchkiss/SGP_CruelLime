using UnityEngine;
using System.Collections;

public class SpiningFace : MonoBehaviour {

    public float Spn_Timer;
    public Sprite[] Spn_Faces;
    bool Snp_Backwards;
    float Spin;


    
	// Use this for initialization
    void Start()
    {
        if (MNGR_Options.colorblind)
        {
            Spn_Faces[0] = Resources.Load<Sprite>("Sprites/GUI/Port_Sword_blind");
            Spn_Faces[1] = Resources.Load<Sprite>("Sprites/GUI/Port_Lancer_blind");
            Spn_Faces[2] = Resources.Load<Sprite>("Sprites/GUI/Port_Defender_blind");
            Spn_Faces[3] = Resources.Load<Sprite>("Sprites/GUI/Port_Archer_blind");
            Spn_Faces[4] = Resources.Load<Sprite>("Sprites/GUI/Port_Ninja_blind");
            Spn_Faces[5] = Resources.Load<Sprite>("Sprites/GUI/Port_Poisoner_blind");
            Spn_Faces[6] = Resources.Load<Sprite>("Sprites/GUI/Port_Wizard_blind");
            Spn_Faces[7] = Resources.Load<Sprite>("Sprites/GUI/Port_ForecMage_blind");
            Spn_Faces[8] = Resources.Load<Sprite>("Sprites/GUI/Port_Spellslinger_blind");
        }
        Snp_Backwards = false;
        int NewFace = Random.Range(0, Spn_Faces.Length);
        GetComponent<SpriteRenderer>().sprite = Spn_Faces[NewFace];
	}
	
	// Update is called once per frame
	void Update () {

        Spn_Timer -= Time.deltaTime;
        if (Spin > 25)
            Snp_Backwards = true;

        if (!Snp_Backwards)
            Spin += Time.deltaTime * 25;
        else
            Spin -= Time.deltaTime * 15;
        
        transform.localScale = new Vector3(Spin, Spin, transform.localScale.z);

	}
}
