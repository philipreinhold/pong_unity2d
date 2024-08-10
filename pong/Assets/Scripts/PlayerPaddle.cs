using UnityEngine;

public class PlayerPaddle : MonoBehaviour
{
    public float speed = 5f; // Speed at which the paddle moves
    public float rotationSpeed = 50f; // Speed of rotation
    public float returnSpeed = 10f; // Speed at which the paddle returns to the default position
    public float minY = -3f; // Minimum Y position
    public float maxY = 3f;  // Maximum Y position
    private float currentRotation = 0f; // Current rotation angle

    void Update()
    {
        MovePaddle();
        RotatePaddle();
    }

    private void MovePaddle()
    {
        // Player control: Move the paddle up and down with arrow keys
        float moveDirection = 0f;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection = 1f; // Move up
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection = -1f; // Move down
        }

        // Calculate new position and clamp it within minY and maxY
        Vector2 newPosition = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y + speed * moveDirection * Time.deltaTime, minY, maxY));

        // Apply the new position
        transform.position = newPosition;
    }

    private void RotatePaddle()
    {
        // Player control: Rotate the paddle with left and right arrow keys
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentRotation += rotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            currentRotation -= rotationSpeed * Time.deltaTime;
        }

        // Gradually return the rotation to the default position
        currentRotation = Mathf.Lerp(currentRotation, 0f, returnSpeed * Time.deltaTime);

        // Apply the rotation to the paddle
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);
    }
}
