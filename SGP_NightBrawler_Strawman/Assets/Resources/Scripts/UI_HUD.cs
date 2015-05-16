using UnityEngine;
using System.Collections;

public class UI_HUD : MonoBehaviour {

	public Canvas theCanvas;
	public GameObject Party;
	public GameObject character_GUI_1, 
					character_GUI_2,
					character_GUI_3;

	// Use this for initialization
	void Start () 
	{
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Party.SetActive(false);
			theCanvas.transform.GetChild(3).gameObject.SetActive(true);
			Cursor.visible = true;
		}
	}

	public void ResumeGame()
	{
		Party.SetActive(true);
		theCanvas.transform.GetChild(3).gameObject.SetActive(false);
		Cursor.visible = false;
	}
}
