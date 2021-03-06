﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Encounters : MonoBehaviour {

    public int Enc_DeadEnemies;
    public int Enc_EnemiesNum;
    public int Enc_SpawnNum;

    public float Enc_SpawnTimer;
    public float Enc_AreaLenght;


    public GameObject[] Enc_Bounds;
    public GameObject[] Enc_SpawnEnemies;

    public GameObject Enc_EncounterEvent;

    public GameObject Enc_TopSpawn;
    public GameObject Enc_LeftSpawn;
    public GameObject Enc_RightSpawn;
    public GameObject Enc_BottomSpawn;

    public List<GameObject> Enc_EnemiesInTheArea;

    private bool Enc_IsActive;
    private float Enc_BaseSpawnTimer;
    private GameObject Enc_Victime;

	public Camera cam;

	// Use this for initialization
	void Start () {

		cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        Enc_BaseSpawnTimer = Enc_SpawnTimer;
        Enc_IsActive = false;
        if (Enc_AreaLenght == 0.0f)
            Enc_AreaLenght = 1.0f;

        for (int i = 0; i < Enc_Bounds.Length; i++)
        {
            BoxCollider2D BoxLoc = Enc_Bounds[i].GetComponent<BoxCollider2D>();
            if (i == 0)
                BoxLoc.offset = new Vector2(-(Enc_AreaLenght / 2) - BoxLoc.size.x, 0);
            if (i == 1)
                BoxLoc.offset = new Vector2((Enc_AreaLenght / 2) + BoxLoc.size.x, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Enc_Victime != null && cam != null)
            cam.GetComponent<CameraFollower>().Cam_CurrTarget = gameObject;

        CheckEnemiesInArea();
        Enc_SpawnTimer -= Time.deltaTime;
        if (Enc_SpawnTimer <= 0.0f && Enc_SpawnNum > 0 && Enc_IsActive)
        {
            int RandLoc = Random.Range(1, 5);
            int RandEnemy = Random.Range(0, Enc_SpawnEnemies.Length);
            switch (RandLoc)
            {
                case 1:
                    //Spawn At the Top;
                    Instantiate(Enc_SpawnEnemies[RandEnemy], Enc_TopSpawn.transform.position, Quaternion.identity);
                    Enc_SpawnNum--;
                    break;
                case 2:
                    //Spawn At the Bottom
                    Instantiate(Enc_SpawnEnemies[RandEnemy], Enc_BottomSpawn.transform.position, Quaternion.identity);
                    Enc_SpawnNum--;
                    break;
                case 3:
                    //Spawn At the Left
                    Instantiate(Enc_SpawnEnemies[RandEnemy], Enc_LeftSpawn.transform.position, Quaternion.identity);
                    Enc_SpawnNum--;
                    break;
                case 4:
                    //Spawn At the right
                    Instantiate(Enc_SpawnEnemies[RandEnemy], Enc_RightSpawn.transform.position, Quaternion.identity);
                    Enc_SpawnNum--;
                    break;
            }
            Enc_SpawnTimer = Enc_BaseSpawnTimer;
        }

        if ((Enc_DeadEnemies == Enc_EnemiesNum) && Enc_EnemiesNum > 0)
        {
            //Spawn SPRITE OF MOVING FOWARD.
			if (cam != null)
			{
				cam.GetComponent<CameraFollower>().Cam_CurrTarget = Enc_Victime;
				Destroy(gameObject);
			}
        }
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player" && !Enc_IsActive)
        {
            Enc_Victime = col.gameObject;
            Enc_EncounterEvent.SetActive(true);
            GetComponent<BoxCollider2D>().size = new Vector2(Enc_AreaLenght, GetComponent<BoxCollider2D>().size.y);
            Enc_IsActive = true;
        }

        if (col.tag == "Enemy" && Enc_IsActive)
        {
            for (int i = 0; i < Enc_EnemiesInTheArea.Count; i++)
            {
                if (Enc_EnemiesInTheArea[i] == col.gameObject)
                    return;
            }
            Enc_EnemiesInTheArea.Add(col.gameObject);
            Enc_EnemiesNum++;
        }  

    }

    void CheckEnemiesInArea()
    {
        int NullEnemies = 0;
        for (int i = 0; i < Enc_EnemiesInTheArea.Count; i++)
        {
            if (Enc_EnemiesInTheArea[i] == null)
                NullEnemies++;
        }
        Enc_DeadEnemies = NullEnemies;
    }
}
