using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab; // Prefab for the ball
    public Transform spawnPosition; // The position where the ball will spawn
    public TMP_Text playerScoreText; // TMP Text for player's score
    public TMP_Text aiScoreText; // TMP Text for AI's score
    public AIPaddle aiPaddle; // Reference to the AI paddle script

    public int playerScore = 0; // Player's score
    public int aiScore = 0; // AI's score

    private GameObject currentBall; // Reference to the current ball in the game

    private float boundaryX = 15f; // X boundary for despawning the ball

    void Start()
    {
        SpawnBall();
    }

    void Update()
    {
        // Check if the ball has gone off screen
        if (currentBall != null)
        {
            if (currentBall.transform.position.x > boundaryX)
            {
                // Player scores if ball goes past the right boundary
                HandleBallOutOfBounds(false);
            }
            else if (currentBall.transform.position.x < -boundaryX)
            {
                // AI scores if ball goes past the left boundary
                HandleBallOutOfBounds(true);
            }
        }
    }

    public void SpawnBall()
    {
        // Destroy the current ball if it exists
        if (currentBall != null)
        {
            Destroy(currentBall);
        }

        // Instantiate a new ball at the spawn position
        currentBall = Instantiate(ballPrefab, spawnPosition.position, Quaternion.identity);

        // Assign the new ball to the AI paddle so it can track it
        aiPaddle.SetBall(currentBall.transform);
    }

    public void HandleBallOutOfBounds(bool isPlayerScored)
    {
        if (isPlayerScored)
        {
            // Player scores
            playerScore++;
            playerScoreText.text = playerScore.ToString();
        }
        else
        {
            // AI scores
            aiScore++;
            aiScoreText.text = aiScore.ToString();
        }

        // Respawn the ball in the center
        SpawnBall();
    }
}
