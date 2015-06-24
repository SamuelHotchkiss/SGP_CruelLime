using UnityEngine;
using System.Collections;

public class DestroyWithPillar : MonoBehaviour {

    public GameObject Pillar;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	


	}

    void OnTriggerEnter2D(Collider2D Col)
    {
       if (Col.name.Contains("PROJ_Pillar_3"))
       {
           Destroy(gameObject);
       }
        
    }
}
