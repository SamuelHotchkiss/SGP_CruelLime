using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {

	public string levelName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player")
		{
			Application.LoadLevel(levelName);
		}
	}
}
