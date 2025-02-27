using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI score;
    public TextMeshProUGUI msg;

    private int currentScore = 0;

    public GameObject player;
    public detector lava;
    public detector goal;
    public detector voidDetector;
    public detector brickDebug;

    [Header("Coins")]

    public detector coin1;
    public detector coin2;
    public detector coin3;
    public detector coin4;
    public detector coin5;

    void Update()
    {
        int timeLeft = 100 - (int)(Time.time);
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
            msg.text = "Game Over";
            Debug.Log("Game Over");
            Time.timeScale = 0;
        }

    }

    public void OnDetect(detector detector)
    {
        if (detector == lava)
        {
            msg.text = "Game Over!";
            Debug.Log("Lose");
            Time.timeScale = 0;
        }
        else if (detector == goal)
        {
            player.SetActive(false);
            msg.text = "You Win!";
            Debug.Log("Win");
            Time.timeScale = 0;
        }else if (detector == coin1)
        {
            currentScore += 100;
            score.text = $"Score: {currentScore}";
            coin1.gameObject.SetActive(false);
        }else if (detector == coin2)
        {
            currentScore += 100;
            score.text = $"Score: {currentScore}";
            coin2.gameObject.SetActive(false);
        }else if (detector == coin3)
        {
            currentScore += 100;
            score.text = $"Score: {currentScore}";
            coin3.gameObject.SetActive(false);
        }else if (detector == coin4)    
        {
            currentScore += 100;
            score.text = $"Score: {currentScore}";
            coin4.gameObject.SetActive(false);
        }else if (detector == coin5)
        {
            currentScore += 100;
            score.text = $"Score: {currentScore}";
            coin5.gameObject.SetActive(false);
        }else if (detector == brickDebug)
        {
            currentScore += 100;
            Debug.Log("Brick Destroyed");
            score.text = $"Score: {currentScore}";
            brickDebug.gameObject.SetActive(false);
        }else if (detector == voidDetector)
        {
            msg.text = "Game Over!";
            Debug.Log("Lose");
            Time.timeScale = 0;
        }
    }
}
