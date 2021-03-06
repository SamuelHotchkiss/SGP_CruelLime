﻿using UnityEngine;
using System.Collections;

public class TUT_Traning : MonoBehaviour {

    public GameObject Tut_Flames;
    public Sprite Tut_HitTarget;
    public GameObject Tut_SmackDummy;

    public bool IsTorch;

    //private Vector3 Tut_PermaLoc;
    private float Tut_SmackTimer;

	// Use this for initialization
	void Start () 
    {
        //Tut_PermaLoc = transform.position;
        if (Tut_Flames != null)
            Tut_Flames.SetActive(false);

        if (Tut_SmackDummy != null)
            Tut_SmackDummy.SetActive(false);
	}

    void Update()
    {
        Tut_SmackTimer -= Time.deltaTime;
        if (Tut_SmackTimer <= 0 && Tut_SmackDummy != null)
            Tut_SmackDummy.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D Col)
    {
        ActivatePlayerFeedback(Col);
    }

    void OnTriggerStay2D(Collider2D Col)
    {
        ActivatePlayerFeedback(Col);
    }

    void ActivatePlayerFeedback(Collider2D Col)
    {
        if ((Col.name.Contains("Fire") || Col.name.Contains("Sling") || Col.name.Contains("Force") || Col.name.Contains("Explosion") || Col.name.Contains("Flame")) && Tut_Flames != null)
        {
            Tut_Flames.SetActive(true);
            Tut_SmackDummy.SetActive(true);
            Tut_SmackTimer = 0.5f;
        }

        if ((Col.name.Contains("Arrow") || Col.name.Contains("Dart") || Col.name.Contains("Shuriken") || Col.name.Contains("Kunai")) && Tut_HitTarget != null && !IsTorch)
        {
            if (!IsTorch)
                GetComponent<SpriteRenderer>().sprite = Tut_HitTarget; 
            Tut_SmackDummy.SetActive(true);
            Tut_SmackTimer = 0.5f;
        }

        if (Col.name.Contains("Melee"))
        {
            Tut_SmackDummy.SetActive(true);
            Tut_SmackTimer = 0.5f;
        }
    }
}
