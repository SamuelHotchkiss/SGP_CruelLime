using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class MENU_Options : MonoBehaviour
{

    public AudioClip Menu_SelectedSound;

    public Slider sfxSlider;
    public Slider musicSlider;
    public Toggle fullscreenToggle;

    // Use this for initialization
    void Start()
    {
        MNGR_Save.LoadOptions();

        sfxSlider.onValueChanged.AddListener(delegate { ChangeSFXVolume(sfxSlider.value); });
        musicSlider.onValueChanged.AddListener(delegate { ChangeMusicVolume(musicSlider.value); });
        fullscreenToggle.onValueChanged.AddListener(delegate { ChangeFullscreen(fullscreenToggle.isOn); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeSFXVolume(float value)
    {
        MNGR_Options.sfxVol = value;
    }

    public void ChangeMusicVolume(float value)
    {
        MNGR_Options.musicVol = value;
    }

    public void ChangeFullscreen(bool screen)
    {
        MNGR_Options.fullscreen = screen;
        Screen.fullScreen = MNGR_Options.fullscreen;
    }

    public void Return()
    {
        AudioSource.PlayClipAtPoint(Menu_SelectedSound, new Vector3(0, 0, 0), MNGR_Options.sfxVol);
        StartCoroutine(WaitForSound(0));
    }

    IEnumerator WaitForSound(int _Selection)
    {
        //This can be used to stop the action from being activated
        //each time a button is PRESSED this should be called to allow 
        //the sound to play ONLY if the scene is being changed. 
        yield return new WaitForSeconds(0.35f);
        switch (_Selection)
        {
            case 0:
                MNGR_Save.SaveOptions();
                Application.LoadLevel("MainMenu");
                break;
            default:
                break;
        }
    }
}
