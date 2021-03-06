﻿using UnityEngine;
using System.Collections;

public class Horde : MonoBehaviour 
{
	public GameObject player;
	public PROJ_Base projectile;
	public float travelSpeed;
	public float arrowSpeed;
	public float fireRate;
	public float timer;
	public float offset;
	public float distanceToPlayer;
    public Vector3 OGLoc;

	// Use this for initialization
	void Start () 
	{
		timer = fireRate;
		player = GameObject.FindGameObjectWithTag("Player");
        OGLoc = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		distanceToPlayer = Mathf.Abs(player.transform.position.x - transform.position.x);
		fireRate = distanceToPlayer * 0.01f;

		if (fireRate < 0.0f)
			fireRate = 0.2f;

		if (timer > 0)
		{
			timer -= Time.deltaTime;
		}
		else
		{
			Vector3 spawn = new Vector3(transform.position.x, transform.position.y + (Random.Range(-1.0f, 1.1f) * offset), transform.position.z);
			Instantiate(projectile, spawn, Quaternion.identity);
			projectile.owner = gameObject;
			projectile.Initialize();
			projectile.velocity = new Vector2(arrowSpeed, 0.0f);
			timer = fireRate;
		}

		transform.position = new Vector3(transform.position.x + (travelSpeed * Time.deltaTime), transform.position.y, transform.position.z);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.GetComponent<PlayerController>())
		{
			player.GetComponent<PlayerController>().MurderEveryone();
		}
	}
}
