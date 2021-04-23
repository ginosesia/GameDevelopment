using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    public float speed;

    [SerializeField] private OptionsStates optionsStates;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private Transform doublePointsBall;
    [SerializeField] private Transform extraLife;
    [SerializeField] private Transform doubleSpeed;

    private bool doubleSpeedDropped = false;
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
        if (optionsStates.PowerUpsState)
        {
            int random = Random.Range(1, 100);
            if (random <= 15 && gameManager.currentLevelNumber > 2) DropDoubleSpeed(collision);
            if (random >= 20 && random <= 35 && gameManager.score <= 150) DropDoublePoints(collision);
            if (random >= 35 && random <= 65 && gameManager.lives < 4) DropExtraLife(collision);
        }
    }

    private void DropDoublePoints(Collision2D collision)
    {
        if (!doublePoints) Instantiate(doublePointsBall, collision.transform.position, collision.transform.rotation);
        doublePoints = true;
    }


    private void DropExtraLife(Collision2D collision)
    {
        if (gameManager.lives < 5)
        {
            int random = Random.Range(1, 101);
            if (random < 15) Instantiate(extraLife, collision.transform.position, collision.transform.rotation);
        }
    }

    private void DropDoubleSpeed(Collision2D collision)
    {
        int random = Random.Range(1, 101);
        if (random < 15) if (!doubleSpeedDropped)
        {
            Instantiate(doubleSpeed, collision.transform.position, collision.transform.rotation);
            doubleSpeedDropped = true;
        }
    }
}