using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class MENU_Options : MonoBehaviour
{
    MENU_Controller myController;
    int currSelected;

    public AudioClip Menu_SelectedSound;

    public Slider sfxSlider;
    public Slider musicSlider;
    public Toggle fullscreenToggle, blindToggle;

    public Button sfxButton, musicButton, screenButton;

    // Use this for initialization
    void Start()
    {
        myController = this.gameObject.GetComponent<MENU_Controller>();

        MNGR_Save.LoadOptions();

        sfxSlider.value = MNGR_Options.sfxVol;
        musicSlider.value = MNGR_Options.musicVol;
        fullscreenToggle.isOn = MNGR_Options.fullscreen;
        blindToggle.isOn = MNGR_Options.colorblind;

        sfxSlider.onValueChanged.AddListener(delegate { ChangeSFXVolume(sfxSlider.value); });
        musicSlider.onValueChanged.AddListener(delegate { ChangeMusicVolume(musicSlider.value); });
        fullscreenToggle.onValueChanged.AddListener(delegate { ChangeFullscreen(fullscreenToggle.isOn); });
        blindToggle.onValueChanged.AddListener(delegate { ChangeBlind(blindToggle.isOn); });

        if (MNGR_Game.AmIMobile())
        {
            fullscreenToggle.gameObject.SetActive(false);
            screenButton.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currSelected = myController.Menu_CurrButton;

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (currSelected == 0)
                ChangeSFXSlider(Input.GetAxis("Horizontal"));
            else if (currSelected == 2)
                ChangeMusicSlider(Input.GetAxis("Horizontal"));
        }
        else if (Input.GetAxis("Pad_Horizontal") != 0)
        {
            if (currSelected == 0)
                ChangeSFXSlider(Input.GetAxis("Pad_Horizontal"));
            else if (currSelected == 2)
                ChangeMusicSlider(Input.GetAxis("Pad_Horizontal"));
        }
    }

    public void ChangeSFXVolume(float value)
    {
        MNGR_Options.sfxVol = value;
    }

    public void ChangeSFXSlider(float value)
    {
        sfxSlider.value += value;
    }

    public void PreviewSFXVolume()
    {
        float oldTimey = Time.timeScale;

        if(oldTimey == 0)
            Time.timeScale = 1.0f;

        AudioSource.PlayClipAtPoint(Menu_SelectedSound, Camera.main.transform.position, MNGR_Options.sfxVol);

        if (oldTimey == 0)
            Time.timeScale = 0;

    }

    public void ChangeMusicVolume(float value)
    {
        MNGR_Options.musicVol = value;
    }

    public void ChangeMusicSlider(float value)
    {
        musicSlider.value += value;
    }

    public void ChangeFullscreen(bool screen)
    {
        MNGR_Options.fullscreen = screen;

        if (screen)
            Screen.SetResolution(1920, 1200, screen);
        else
            Screen.SetResolution(1280, 720, screen);
    }

    public void ChangeBlind(bool blind)
    {
        MNGR_Options.colorblind = blind;
		if (GameObject.FindGameObjectWithTag("Player"))
			GameObject.FindGameObjectWithTag("Player").GetComponent<MNGR_Animation_Player>().Initialize();
    }

    public void ChangeScreenToggle()
    {
        fullscreenToggle.isOn = !fullscreenToggle.isOn;
    }

    public void ChangeBlindToggle()
    {
        blindToggle.isOn = !blindToggle.isOn;
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
