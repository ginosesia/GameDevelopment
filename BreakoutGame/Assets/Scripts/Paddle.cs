using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paddle : MonoBehaviour
{

    public float speed;
    public float rightBoundary;
    public float leftBoundary;
    public Ball ball;
    public GameManager gameManager;
    [SerializeField] SoundManager soundManager;
    [SerializeField] private OptionsStates optionsStates;
    private Image paddle;
    public Rigidbody2D rigidBody;

    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = rigidBody.GetComponent<Rigidbody2D>();
        SetPaddleColor();
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
            gameManager.AdjustLives(1, true);
            Destroy(collision.gameObject);
            soundManager.PlaySound(2);
 
        }

        if (collision.CompareTag("DoubleSpeed"))
        {
            ball.SetBallSpeed((float)1.75);
            Destroy(collision.gameObject);
            gameManager.doubleSpeed.gameObject.SetActive(true);
            Invoke(nameof(SetNormalSpeed), 10);
            soundManager.PlaySound(2);
        }

        if (collision.CompareTag("doublePointsBall"))
        {
            Destroy(collision.gameObject);
            gameManager.AdjustScore(0, true);
            soundManager.PlaySound(2);
        }
    }

    public void SetNormalSpeed()
    {
        ball.SetBallSpeed(1);
        gameManager.doubleSpeed.gameObject.SetActive(false);
    }

    public void SetPaddleColor()
    {
        paddle = rigidBody.GetComponent<Image>();
        switch (optionsStates.ballColorValue)
        {
            case 0:
                paddle.color = Color.white;
                break;
            case 1:
                paddle.color = Color.green;
                break;
            case 2:
                paddle.color = Color.blue;
                break;
            case 3:
                paddle.color = Color.magenta;
                break;
            case 4:
                paddle.color = Color.yellow;
                break;
            case 5:
                paddle.color = Color.white;
                break;
        }
    }

}
