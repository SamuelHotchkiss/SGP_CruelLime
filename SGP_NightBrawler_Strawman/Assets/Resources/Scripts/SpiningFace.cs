using UnityEngine;
using System.Collections;

public class SpiningFace : MonoBehaviour {

    public float Spn_Timer;
    public Sprite[] Spn_Faces;
    bool Snp_Backwards;
    float Spin;


    
	// Use this for initialization
	void Start () {
        Snp_Backwards = false;
        int NewFace = Random.Range(0, Spn_Faces.Length);
        GetComponent<SpriteRenderer>().sprite = Spn_Faces[NewFace];
	}
	
	// Update is called once per frame
	void Update () {

        Spn_Timer -= Time.deltaTime;
        if (Spin > 15)
            Snp_Backwards = true;

        if (!Snp_Backwards)
            Spin += Time.deltaTime * 25;
        else
            Spin -= Time.deltaTime * 20;
        
        transform.localScale = new Vector3(Spin, Spin, transform.localScale.z);

	}
}
