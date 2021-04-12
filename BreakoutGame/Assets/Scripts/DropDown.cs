using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDown : MonoBehaviour
{
    public TextMeshProUGUI BallColor;
    public TextMeshProUGUI PaddleColor;

    private Ball ball;
    private Paddle paddle;

    public void HandleBallChanges(int val)
    {
        switch (val)
        {
            case 0:
                ball.SetBallColor(Color.red);
                break;
            case 1:
                ball.SetBallColor(Color.green);
                break;
            case 2:
                ball.SetBallColor(Color.blue);
                break;
            case 3:
                ball.SetBallColor(Color.magenta);
                break;
            case 4:
                ball.SetBallColor(Color.yellow);
                break;
            case 5:
                ball.SetBallColor(Color.white);
                break;
        }
    }

    public void HandlePaddleChanges(int val)
    {
        switch (val)
        {
            case 0:
                paddle.SetPaddleColor(Color.red);
                break;
            case 1:
                paddle.SetPaddleColor(Color.green);
                break;
            case 2:
                paddle.SetPaddleColor(Color.blue);
                break;
            case 3:
                paddle.SetPaddleColor(Color.magenta);
                break;
            case 4:
                paddle.SetPaddleColor(Color.yellow);
                break;
            case 5:
                paddle.SetPaddleColor(Color.white);
                break;
        }
    }

}
