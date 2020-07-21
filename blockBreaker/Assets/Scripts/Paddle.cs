using UnityEngine;

public class Paddle : MonoBehaviour
{

    //config param

    [SerializeField] float screenWidthInUnits = 16f;


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

    private void Movement()
    {
        float mousePosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
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
