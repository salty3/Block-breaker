using UnityEngine;

public class Ball : MonoBehaviour
{

    // config params
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 1f;
    [SerializeField] float yPush = 14f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    [SerializeField] float velocityIncrease = 1.2f;
    [SerializeField] float velocityDecrease = 0.8f;
    [SerializeField] float minVelocityLimit = 10f;
    [SerializeField] float maxVelocityLimit = 40f;

    // state
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    // Cached components refs
    AudioSource audioSource;
    Rigidbody2D rigidBody2D;
    

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        audioSource = GetComponent<AudioSource>();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockToPaddle();
            LaunchOnClick();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasStarted)
        {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            audioSource.PlayOneShot(clip);
            VelocityTweaks();
        }
    }

    private void LaunchOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            rigidBody2D.velocity = new Vector2(xPush, yPush);
        } 
    }

    private void LockToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }


    private void VelocityTweaks()
    {
        Vector2 velocityTweak = new Vector2
            (
            Random.Range(-randomFactor, randomFactor),
            Random.Range(-randomFactor, randomFactor)
            );
        Vector2 velocityIncreaseTweak = new Vector2
            (
            velocityIncrease,
            velocityIncrease
            );
        Vector2 velocityDecreaseTweak = new Vector2
            (
            velocityDecrease,
            velocityDecrease
            );

        rigidBody2D.velocity += velocityTweak;
        if (Mathf.Abs(rigidBody2D.velocity.x) + Mathf.Abs(rigidBody2D.velocity.y) < minVelocityLimit)
        {
            rigidBody2D.velocity *= velocityIncreaseTweak;
        }
        if (Mathf.Abs(rigidBody2D.velocity.x) + Mathf.Abs(rigidBody2D.velocity.y) > maxVelocityLimit)
        {
            rigidBody2D.velocity *= velocityDecreaseTweak;
        }
    }
}
