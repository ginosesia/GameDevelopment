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
    [SerializeField] private AudioClip powerUp;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);

        //Check if paddle reached right or left boundary
        if (transform.position.x < leftBoundary)
        {
            //PlayClip(powerUp);
            transform.position = new Vector2(leftBoundary, transform.position.y);
        }
        if (transform.position.x > rightBoundary)
        {
            //PlayClip(powerUp);
            transform.position = new Vector2(rightBoundary, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("ExtraLifePowerUp"))
        {
            gameManager.AdjustLives(1, true);
            Destroy(collision.gameObject);
            //PlayClip(powerUp);
        }

        //if (collision.CompareTag("SpawnBalls"))
        //{
        //    Destroy(collision.gameObject);
        //    ball.DropMultipleBalls();
        //}

        if (collision.CompareTag("DoubleSpeed"))
        {
            ball.SetBallSpeed((float)1.75);
            Destroy(collision.gameObject);
            gameManager.doubleSpeed.gameObject.SetActive(true);
            Invoke(nameof(SetNormalSpeed), 10);
            //PlayClip(powerUp);
        }

        if (collision.CompareTag("doublePointsBall"))
        {
            Destroy(collision.gameObject);
            gameManager.AdjustScore(0, true);
            //PlayClip(powerUp);
        }
    }

    public void SetNormalSpeed()
    {
        ball.SetBallSpeed(1);
        gameManager.doubleSpeed.gameObject.SetActive(false);
    }

    private void PlayClip(AudioClip audio)
    {
        GetComponent<AudioSource>().PlayOneShot(audio);
    }

    public void SetPaddleColor(Color color)
    {
        //rigidBody.GetComponent<SpriteRenderer>().color = color;
    }

}
