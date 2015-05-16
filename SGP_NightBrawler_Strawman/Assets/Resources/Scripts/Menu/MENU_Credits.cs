using UnityEngine;
using System.Collections;

public class MENU_Credits : MonoBehaviour {

    public AudioClip Menu_SelectedSound;

    public void Return()
    {
        AudioSource.PlayClipAtPoint(Menu_SelectedSound, new Vector3(0, 0, 0), 1.0f);
        StartCoroutine(WaitForSound());
    }



    IEnumerator WaitForSound()
    {
        yield return new WaitForSeconds(0.35f);
        Application.LoadLevel("MainMenu");
    }
}
