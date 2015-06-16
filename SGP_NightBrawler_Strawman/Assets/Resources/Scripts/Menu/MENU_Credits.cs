using UnityEngine;
using System.Collections;

public class MENU_Credits : MonoBehaviour {

    public AudioClip Menu_SelectedSound;
    public GameObject Menu_Credits;
    public float Menu_CreditTimer;

    void Update()
    {
        Menu_CreditTimer -= Time.deltaTime;
        if (Menu_CreditTimer <= 0.0f)
            StartCoroutine(WaitForSound());

        float GoUp = Menu_Credits.transform.position.y + (2.5f * Time.deltaTime);
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
