using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDown : MonoBehaviour
{
    public TextMeshProUGUI BallColor;
    public TextMeshProUGUI PaddleColor;
    private readonly string ballColor = "BallColor";
    private readonly string paddleColor = "paddleColor";

    public void HandleBallChanges(int val)
    {
        switch (val)
        {
            case 0:
                PlayerPrefs.SetInt(ballColor, 0);
                break;
            case 1:
                PlayerPrefs.SetInt(ballColor, 1);
                break;
            case 2:
                PlayerPrefs.SetInt(ballColor, 2);
                break;
            case 3:
                PlayerPrefs.SetInt(ballColor, 3);
                break;
            case 4:
                PlayerPrefs.SetInt(ballColor, 4);
                break;
            case 5:
                PlayerPrefs.SetInt(ballColor, 5);
                break;
        }
    }

    public void HandlePaddleChanges(int val)
    {
        switch (val)
        {
            case 0:
                PlayerPrefs.SetInt(paddleColor, 0);
                break;
            case 1:
                PlayerPrefs.SetInt(paddleColor, 1);
                break;
            case 2:
                PlayerPrefs.SetInt(paddleColor, 2);
                break;
            case 3:
                PlayerPrefs.SetInt(paddleColor, 3);
                break;
            case 4:
                PlayerPrefs.SetInt(paddleColor, 4);
                break;
            case 5:
                PlayerPrefs.SetInt(paddleColor, 5);
                break;
        }
    }

}
