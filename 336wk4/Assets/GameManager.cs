using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI score;

    private int currentScore = 0;

    public GameObject player;
    public GameObject lava;
    public GameObject goal;

    void Update()
    {
        int timeLeft = 20 - (int)(Time.time);
        timerText.text = $"Time: {timeLeft}";

        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Perform a raycast from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Check if the hit object is a brick
                if (hit.collider.CompareTag("Brick"))
                {
                    Destroy(hit.collider.gameObject);
                }
                // Check if the hit object is a coin
                else if (hit.collider.CompareTag("Coin"))
                {
                    currentScore += 10; // Add points for collecting a coin
                    Destroy(hit.collider.gameObject);
                }
                // Check if the hit object is a question box
                else if (hit.collider.CompareTag("QuestionBox"))
                {
                    currentScore += 50; // Add bonus points for hitting a question box
                }

                // Update the score display
                score.text = $"Score: {currentScore}";
            }
        }
        // Check if time has run out
        if (timeLeft <= 0)
        {
            timerText.text = "Game Over";
            Debug.Log("Game Over");
            // Implement game over logic here
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the lava trigger
        if (other.gameObject == lava && other.gameObject == player)
        {
            timerText.text = "Game Over";
            Debug.Log("Game Over");
            // Implement game over logic here
        }
        // Check if the player enters the goal trigger
        else if (other.gameObject == goal && other.gameObject == player)
        {
            timerText.text = "Finished";
            Debug.Log("Finished");
            // Implement game win logic here
        }
    }
}
