using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    public float speed;
    private int score;
    [HideInInspector] public bool coloredBallDroped = false;
    [HideInInspector] public bool ballIsMultiColoured = false;


    [SerializeField] private OptionsStates optionsStates;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private Transform doublePointsBall;
    [SerializeField] private Transform extraLife;
    [SerializeField] private Transform multiColoredBall;
    [SerializeField] private Transform doubleSpeedBall;

    private bool doublePoints = false;
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0f, -1f) * Time.deltaTime * speed);

        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }

    public void SelectPowerUp(Collision2D collision)
    {
        score = uIManager.score;

        if (optionsStates.PowerUpsState)
        {
            int random = Random.Range(1, 100);
            if (!ballIsMultiColoured && random < 20)
            {
                if (!coloredBallDroped)
                {
                    DropMultiColoredBall(collision);
                    coloredBallDroped = true;
                }
            }
            if (random >= 20 && random <= 35 && score <= 200)
            {
                DropDoublePoints(collision);

            }

            if (random >= 50 && random <= 55)
            {
                DropDoubleSpeedBall(collision);
            }

            if (random >= 35 && random <= 65 && uIManager.lives < 4) DropExtraLife(collision);

        }
    }

    private void DropDoublePoints(Collision2D collision)
    {
        if (!doublePoints) Instantiate(doublePointsBall, collision.transform.position, collision.transform.rotation);
        doublePoints = true;
    }


    private void DropExtraLife(Collision2D collision)
    {
        if (uIManager.lives < 5)
        {
            int random = Random.Range(1, 101);
            if (random < 15) Instantiate(extraLife, collision.transform.position, collision.transform.rotation);
        }
    }

    private void DropMultiColoredBall(Collision2D collision)
    {
        Instantiate(multiColoredBall, collision.transform.position, collision.transform.rotation);
    }


    private void DropDoubleSpeedBall(Collision2D collision)
    {
        Instantiate(doubleSpeedBall, collision.transform.position, collision.transform.rotation);
    }
}