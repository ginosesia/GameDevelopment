using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{

    [SerializeField] private Text soundLevel;
    [SerializeField] private Text musicLevel;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Toggle PowerUpsToggle;
    [SerializeField] private Toggle MusicToggle;
    [SerializeField] private Toggle SoundToggle;

    private float soundSliderValue;
    private float musicSliderValue;
    private string state;
    private bool toggleState;
    private static readonly string Sound = "Sound";
    private static readonly string Music = "Music";
    private static readonly string PowerUps = "PowerUps";
    private readonly string[] switches = { Sound, Music, PowerUps };
    private string SettingName;
    private readonly string menu = "MainMenu";

    //All Public Methods below:
    public void Back()
    {
        SceneManager.LoadScene(menu);
    }

    public void Start()
    {

        foreach (string togleSwitch in switches)
        {
            state = PlayerPrefs.GetString(togleSwitch);
            SetToggleSwitches(state, togleSwitch);
        }
    }

    public void HandleSoundToggleButtonPressed()
    {
        SoundToggle = GameObject.Find(Sound).GetComponent<Toggle>();
        ToggleSwitch(SoundToggle);

    }

    public void HandleMusicToggleButtonPressed()
    {
        MusicToggle = GameObject.Find(Music).GetComponent<Toggle>();
        ToggleSwitch(MusicToggle);
    }

    public void HandlePowerUpToggleButtonPressed()
    {
        PowerUpsToggle = GameObject.Find(PowerUps).GetComponent<Toggle>();
        ToggleSwitch(PowerUpsToggle);
    }

    public void HandleSoundSliderChanged()
    {
        soundSliderValue = soundSlider.value;
        soundLevel.text = soundSliderValue.ToString();
    }

    public void HandleMusicSliderChanged()
    {
        musicSliderValue = musicSlider.value;
        musicLevel.text = musicSliderValue.ToString();
    }

    //All Private Methods below:
    private void SetToggleSwitches(string state, string toggleSwitch)
    {

        if (state == "True")
        {
            toggleState = true;
        } else
        {
            toggleState = false;
        }

        switch (toggleSwitch)
        {
            case "Music":
                MusicToggle.isOn = toggleState;
                break;
            case "Sound":
                SoundToggle.isOn = toggleState;
                break;
            case "PowerUps":
                PowerUpsToggle.isOn = toggleState;
                break;
            default:
                break;
        }
    }

    private void ToggleSwitch(Toggle toggle)
    {
        SettingName = toggle.name;
        if (toggle.isOn)
        {
            PlayerPrefs.SetString(SettingName, "True");
        } else
        { 
            PlayerPrefs.SetString(SettingName, "False");
        }
    }
}
