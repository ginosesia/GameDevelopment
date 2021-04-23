using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsStates : MonoBehaviour
{
    [HideInInspector] public bool SoundState;
    [HideInInspector] public bool PowerUpsState;

    private static readonly string Sound = "Sound";
    private static readonly string PowerUps = "PowerUps";
    private readonly string ballColor = "BallColor";
    private readonly string paddleColor = "paddleColor";

    private string state;
    public int ballColorValue;
    public int paddleColorValue;
    private readonly string[] switches = { Sound, PowerUps };

    // Start is called before the first frame update
    void Start()
    {
        CheckGameOptionsStates();
    }


    private void CheckGameOptionsStates()
    {
        foreach (string togleSwitch in switches)
        {
            state = PlayerPrefs.GetString(togleSwitch);
            switch (state)
            {
                case "True":
                    if (togleSwitch == Sound) SoundState = true;
                    if (togleSwitch == PowerUps) PowerUpsState = true;
                    break;
                case "False":
                    if (togleSwitch == Sound) SoundState = false;
                    if (togleSwitch == PowerUps) PowerUpsState = false;
                    break;
            }
        }
    }

    private void GetBallColor()
    {
        ballColorValue = PlayerPrefs.GetInt(ballColor);
    }


    private void GetPaddleColor()
    {
        paddleColorValue = PlayerPrefs.GetInt(paddleColor);
    }



}
