using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScaleUp : MonoBehaviour
{
    [SerializeField] Ball ball1;
    [SerializeField] float scaleFactor;

    Vector2 ballScale;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ballScale = new Vector2(ball1.transform.localScale.x * scaleFactor, ball1.transform.localScale.y * scaleFactor);
        ball1.transform.localScale = ballScale;
        Destroy(gameObject);
    }
}
