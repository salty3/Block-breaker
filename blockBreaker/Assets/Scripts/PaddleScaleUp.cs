using UnityEngine;

public class PaddleScaleUp : MonoBehaviour
{
    [SerializeField] Paddle paddle1;
    [SerializeField] float scaleFactor;

    Vector2 paddleScale;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        paddleScale = new Vector2(paddle1.transform.localScale.x * scaleFactor, paddle1.transform.localScale.y);
        paddle1.transform.localScale = paddleScale;
        paddle1.MovementLimitation();
        Destroy(gameObject);
    }
}
