using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MENU_GameOverLose : MonoBehaviour 
{
	public float maxValue;
	//Image panel;

	public float speed;
	public float time;

	public AudioClip Menu_SelectedSound;    //Clip of sound that will play wen a button is press.
	private string Menu_Levelname;          //Name that will be use to change scenes


	/////////////////////////////////////
	// Ignore the color shit for now,
	// I was trying to do a rainbow effect
	/////////////////////////////////////


	// Use this for initialization
	void Start () 
	{
		//panel = GetComponent<Image>();
		//panel.color = new Color(30.0f, 0.0f, 0.0f, 255.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if (panel.color.r > 0 && panel.color.b < maxValue)
		//{
		//	float newColorB = Mathf.SmoothDamp(0, maxValue, ref speed, Time.deltaTime);
		//	panel.color = new Color(panel.color.r, panel.color.g, newColorB, 255.0f);
		//}
		//else if (panel.color.r > 0 && panel.color.b == maxValue)
		//{
		//	float newColorR = Mathf.SmoothDamp(panel.color.r, 0, ref speed, Time.deltaTime);
		//	panel.color = new Color(newColorR, panel.color.g, panel.color.b, 255.0f);
		//}
		/////////////////////////////////////
		//else if (panel.color.b > 0 && panel.color.g < maxValue)
		//{
		//	float newColorG = Mathf.SmoothDamp(0, maxValue, ref speed, Time.deltaTime);
		//	panel.color = new Color(panel.color.r, newColorG, panel.color.b, 255.0f);
		//}
		//else if (panel.color.b > 0 && panel.color.g == maxValue)
		//{
		//	float newColorB = Mathf.SmoothDamp(panel.color.b, 0, ref speed, Time.deltaTime);
		//	panel.color = new Color(panel.color.r, panel.color.g, newColorB, 255.0f);
		//}
		/////////////////////////////////////
		//else if (panel.color.g > 0 && panel.color.r < maxValue)
		//{
		//	float newColorR = Mathf.SmoothDamp(0, maxValue, ref speed, Time.deltaTime);
		//	panel.color = new Color(newColorR, panel.color.g, panel.color.b, 255.0f);
		//}
		//else if (panel.color.g > 0 && panel.color.r == maxValue)
		//{
		//	float newColorG = Mathf.SmoothDamp(panel.color.g, 0, ref speed, Time.deltaTime);
		//	panel.color = new Color(panel.color.r, newColorG, panel.color.b, 255.0f);
		//}
	}

	public void ChangeSceneButton(string levelname)
	{
		//Level Name most be the EXACT name of the scene.
		AudioSource.PlayClipAtPoint(Menu_SelectedSound, new Vector3(0, 0, 0), MNGR_Options.sfxVol);
		Menu_Levelname = levelname;
		StartCoroutine(WaitForSound(0));
		MNGR_Game.playerPosition++;
	}

	IEnumerator WaitForSound(int _selection)
	{
		yield return new WaitForSeconds(0.35f);     //This is how long the current sound clip takes to play.
		switch (_selection)
		{
			case 0:
				Application.LoadLevel(Menu_Levelname);
				break;
			case 1:
				Application.Quit();
				break;
			default:
				break;
		}
	}
}
