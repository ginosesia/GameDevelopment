using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpCollected : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private OptionsStates optionsStates;
    [SerializeField] private Ball ball;
    [SerializeField] private Text powerUp;
    private readonly string extraLife = "Exra Life";
    private readonly string doubleSpeed = "Double Speed";
    private readonly string doublePoints = "Points Doubled";


    public void PowerUpCollection(Collider2D collision)
    {
        if (collision.CompareTag("ExtraLifePowerUp"))
        {
            gameManager.AdjustLives(1, true);
            Destroy(collision.gameObject);
            soundManager.PlaySound(2);
            ShowMessage(extraLife);
        }

        if (collision.CompareTag("DoubleSpeed"))
        {
            ball.SetBallSpeed((float)1.75);
            Destroy(collision.gameObject);
            gameManager.doubleSpeed.gameObject.SetActive(true);
            Invoke(nameof(SetNormalSpeed), 10);
            soundManager.PlaySound(2);
            ShowMessage(doubleSpeed);
        }

        if (collision.CompareTag("doublePointsBall"))
        {
            Destroy(collision.gameObject);
            gameManager.AdjustScore(0, true);
            soundManager.PlaySound(2);
            ShowMessage(doublePoints);
        }
    }


    private void SetNormalSpeed()
    {
        ball.SetBallSpeed(1);
        gameManager.doubleSpeed.gameObject.SetActive(false);
    }


    private void ShowMessage(string message)
    {
        powerUp.text = message;
        powerUp.gameObject.SetActive(true);
        Invoke(nameof(RemoeMessage), (float)1.5);
    }

    private void RemoeMessage()
    {
        powerUp.gameObject.SetActive(false);

    }


}
