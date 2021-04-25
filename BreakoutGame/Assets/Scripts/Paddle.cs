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
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private OptionsStates optionsStates;
    [SerializeField] private PowerUpCollected powerUpCollected;
    private Image paddle;
    private int value;
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
        powerUpCollected.PowerUpCollection(collision);
    }


    public void SetPaddleColor()
    {
        paddle = rigidBody.GetComponent<Image>();
        value = optionsStates.GetColor();
        switch (value)
        {
            case 0:
                paddle.color = Color.red;
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
