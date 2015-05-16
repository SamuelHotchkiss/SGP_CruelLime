using UnityEngine;
using System.Collections;

public class MENU_Options : MonoBehaviour {

    public AudioClip Menu_SelectedSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void Return()
    {
        AudioSource.PlayClipAtPoint(Menu_SelectedSound, new Vector3(0, 0, 0), 1.0f);
        StartCoroutine(WaitForSound(0));
    }

    IEnumerator WaitForSound(int _Selection)
    {
        //This can be use to stop the action from been activated
        //Each time a button is PRESS this sould be call to allow 
        //the sound is play ONLY if the scene is been change. 
        yield return new WaitForSeconds(0.35f);
        switch (_Selection)
        {
            case 0:
                Application.LoadLevel("MainMenu");
                break;
            default:
                break;
        }
    }
}
