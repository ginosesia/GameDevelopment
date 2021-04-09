using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{

    public Toggle PowerUpsToggle;
    public Toggle MusicToggle;
    public Toggle SoundToggle;

    private string state;
    private bool toggleState;
    private readonly string trueState = "True";
    private readonly string falseState = "False";
    private readonly string menu = "MainMenu";
    private static readonly string Sound = "Sound";
    private static readonly string Music = "Music";
    private static readonly string PowerUps = "PowerUps";
    private readonly string[] switches = { Sound, Music, PowerUps };
    private string SettingName;

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

    //All Private Methods below:
    private void SetToggleSwitches(string state, string toggleSwitch)
    {

        if (state == trueState)
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
            PlayerPrefs.SetString(SettingName, trueState);
        }
        else
        { 
            PlayerPrefs.SetString(SettingName, falseState);
        }
    }
}
