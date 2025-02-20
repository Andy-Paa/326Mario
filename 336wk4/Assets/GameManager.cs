using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI score;

    private int currentScore = 0;

    void Update()
    {
        int timeLeft = 300 - (int)(Time.time);
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
    }
}
