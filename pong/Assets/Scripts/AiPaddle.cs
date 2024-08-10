using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    public float baseSpeed = 3f; // Base speed for the AI paddle
    public float maxSpeed = 6f; // Maximum speed for the AI paddle
    public float minY = -3f; // Minimum Y position for the paddle
    public float maxY = 3f;  // Maximum Y position for the paddle
    public float anticipationFactor = 0.5f; // How much the AI anticipates the ball's movement
    public float smoothTime = 0.2f; // Smoothing time for the paddle's movement
    public float rotationSpeed = 50f; // Speed of the paddle's rotation
    public float rotationReturnSpeed = 10f; // Speed at which the paddle returns to its default rotation
    public float maxRotationAngle = 30f; // Maximum rotation angle for the paddle

    private Transform ball; // Reference to the ball's Transform
    private float currentSpeed;
    private float targetY;
    private float velocityY = 0f;
    private float currentRotation = 0f; // Current rotation angle of the paddle

    void Start()
    {
        SetRandomSpeed();
    }

    void Update()
    {
        if (ball != null)
        {
            MovePaddle();
            RotatePaddle();
        }
    }

    public void SetBall(Transform newBall)
    {
        ball = newBall;
    }

    private void MovePaddle()
    {
        // Anticipate where the ball will be based on its velocity and position
        float ballVelocityY = ball.GetComponent<Rigidbody2D>().velocity.y;
        targetY = ball.position.y + ballVelocityY * anticipationFactor;

        // Smoothly move the paddle towards the target Y position
        float newPositionY = Mathf.SmoothDamp(transform.position.y, targetY, ref velocityY, smoothTime);

        // Clamp the new position within the allowed Y range
        newPositionY = Mathf.Clamp(newPositionY, minY, maxY);

        // Apply the new position to the paddle
        transform.position = new Vector2(transform.position.x, newPositionY);
    }

    private void RotatePaddle()
    {
        // Rotate the paddle to create a tricky angle when hitting the ball
        if (ball.position.y > transform.position.y && Random.value > 0.5f)
        {
            currentRotation = Mathf.MoveTowards(currentRotation, maxRotationAngle, rotationSpeed * Time.deltaTime);
        }
        else if (ball.position.y < transform.position.y && Random.value > 0.5f)
        {
            currentRotation = Mathf.MoveTowards(currentRotation, -maxRotationAngle, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Gradually return to the default rotation
            currentRotation = Mathf.MoveTowards(currentRotation, 0f, rotationReturnSpeed * Time.deltaTime);
        }

        // Apply the rotation to the paddle
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);
    }

    private void SetRandomSpeed()
    {
        // Set a random speed for varied movement
        currentSpeed = Random.Range(baseSpeed, maxSpeed);
    }
}
