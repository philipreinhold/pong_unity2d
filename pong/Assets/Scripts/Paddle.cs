using UnityEngine;
using UnityEngine.UIElements;

public class Paddle : PongObject
{
    public float moveDirection; // 1 = up, -1 = down, 0 = stationary
    public float rotationSpeed = 50f; // Speed of rotation
    public float returnSpeed = 10f; // Speed at which the paddle returns to the default position
    private float currentRotation = 0f; // Current rotation angle

    public bool isLeftPaddle; // Flag to distinguish between the left and right paddles

    private float minY = -3f; // Minimum Y position
    private float maxY = 3f;  // Maximum Y position

    void Update()
    {
        MovePaddle();
        RotatePaddle();
    }

    public void MovePaddle()
    {
        // Move the paddle based on input and clamp the Y position within the defined limits
        Position = new UnityEngine.Vector2(Position.x, Mathf.Clamp(Position.y + Speed * moveDirection * Time.deltaTime, minY, maxY));
        UpdatePosition();
    }

    public void RotatePaddle()
    {
        if (isLeftPaddle)
        {
            // Control the left paddle
            if (Input.GetKey(KeyCode.A))
            {
                currentRotation += rotationSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                currentRotation -= rotationSpeed * Time.deltaTime;
            }
        }
        else
        {
            // Control the right paddle
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                currentRotation += rotationSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                currentRotation -= rotationSpeed * Time.deltaTime;
            }
        }

        // Gradually return the rotation to the default position
        currentRotation = Mathf.Lerp(currentRotation, 0f, returnSpeed * Time.deltaTime);

        // Apply the rotation to the paddle
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);
    }

    public override void UpdatePosition()
    {
        base.UpdatePosition();
        // Additional paddle-specific logic if needed
    }
}
