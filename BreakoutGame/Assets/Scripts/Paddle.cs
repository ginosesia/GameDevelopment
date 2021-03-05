using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed;
    public float rightBoundary;
    public float leftBoundary;

    public GameManager gameManager;
    public Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);

        //Check if paddle reached right or left boundary
        if (transform.position.x < leftBoundary)
        {
            transform.position = new Vector2(leftBoundary, transform.position.y);
        }
        if (transform.position.x > rightBoundary)
        {
            transform.position = new Vector2(rightBoundary, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("ExtraLifePowerUp"))
        {
            gameManager.AdjustLives(1);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("SpawnBalls"))
        {
            Destroy(collision.gameObject);
            ball.DropMultipleBalls();
        }

        if (collision.CompareTag("DoubleSpeed"))
        {
            ball.SetBallSpeed(2f);
            Destroy(collision.gameObject);
            gameManager.doubleSpeed.gameObject.SetActive(true);
            Invoke(nameof(SetNormalSpeed), 5);
        }
    }

    public void SetNormalSpeed()
    {
        ball.SetBallSpeed(1);
        gameManager.doubleSpeed.gameObject.SetActive(false);
    }
}
