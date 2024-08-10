using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public float speed = 7f; // Base speed of the ball
    public Vector2 direction = Vector2.right; // Initial direction of the ball

    private bool isSpeedBoosted = false;

    void Start()
    {
        // Normalize the direction to ensure consistent speed
        direction = direction.normalized;
    }

    void Update()
    {
        MoveBall();
    }

    private void MoveBall()
    {
        // Move the ball in the specified direction
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reflect the ball's direction when it collides with something
        direction = Vector2.Reflect(direction, collision.contacts[0].normal);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the ball collides with a speed boost power-up
        if (other.CompareTag("SpeedBoost") && !isSpeedBoosted)
        {
            StartCoroutine(SpeedBoost());
        }
    }

    private IEnumerator SpeedBoost()
    {
        isSpeedBoosted = true;
        float originalSpeed = speed;
        speed *= 2; // Double the speed

        yield return new WaitForSeconds(5f); // Wait for 5 seconds

        speed = originalSpeed; // Reset speed to original value
        isSpeedBoosted = false;
    }
}
