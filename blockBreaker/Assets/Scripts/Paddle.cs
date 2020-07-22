using UnityEngine;

public class Paddle : MonoBehaviour
{

    //config param

    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float collisionXVelocityMultipier = 1f;


    float minX;
    float maxX;

    // refs
    GameSession gameSession;
    Ball ball;

    
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
        MovementLimitation();
    }


    void Update()
    {
        Movement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 ballXVelocityIncrease = new Vector2(GetXPos() * collisionXVelocityMultipier, 0);
        collision.rigidbody.velocity += ballXVelocityIncrease;
    }

    private void Movement()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = paddlePos;
    }

    public void MovementLimitation()
    {
        minX = 0f;
        maxX = screenWidthInUnits;
        minX += transform.localScale.x;
        maxX -= transform.localScale.x;
    }

    private float GetXPos()
    {
        if (gameSession.IsAutoplayEnabled())
        {
            return ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
