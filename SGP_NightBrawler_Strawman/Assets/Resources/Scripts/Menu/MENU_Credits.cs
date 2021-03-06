﻿using UnityEngine;
using System.Collections;

public class MENU_Credits : MonoBehaviour {

    public AudioClip Menu_SelectedSound;
    public GameObject Menu_Credits, Ref_Point;
    public float Menu_CreditTimer;
    public float Menu_CreditSpeed;

    void Start()
    {
        Destroy(GameObject.Find("DJ"));
    }

    void Update()
    {
        //Menu_CreditTimer -= Time.deltaTime;
        if (Menu_Credits.transform.position.y > Ref_Point.transform.position.y)
            StartCoroutine(WaitForSound());

        float GoUp = Menu_Credits.transform.position.y + (Menu_CreditSpeed * Time.deltaTime);
        Menu_Credits.transform.position = new Vector3(Menu_Credits.transform.position.x, GoUp);

    }

    public void Return()
    {
        AudioSource.PlayClipAtPoint(Menu_SelectedSound, new Vector3(0, 0, 0), MNGR_Options.sfxVol);
        StartCoroutine(WaitForSound());
    }

    IEnumerator WaitForSound()
    {
        yield return new WaitForSeconds(0.35f);
        Application.LoadLevel("MainMenu");
    }
}
