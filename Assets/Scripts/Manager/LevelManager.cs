/*
 * The level manager manages the references to the ui elements in the current level. 
 * It call the game manager to get the current state of the game and update the ui.
 * It updates the timer every frame and shows the game over or level complete ui.
 */
using UnityEngine;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    //referenced ui elements of the level
    public GameObject completeLevelUI;
    public Text lvlWonScore;
    public GameObject gameOverUI;
    public Text lvllostScore;
    public GameObject HUD;
    public Text timeUI;
    public Text scoreText;
    public Text countdownText;
    public Text lifesUI;

    private GameManager gameManager;

    //get the game manager
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        //if game not started, update countdown
        if (!gameManager.Timeout && !gameManager.GameStarted)
        {
            countdownText.text = gameManager.CountdownTimer.ToString("D");
        }

        //check if game is running and update time in the ui
        if (gameManager.GameStarted && !gameManager.GameEnded)
        {
            timeUI.text = gameManager.Timer.ToString("F");
            scoreText.text = gameManager.Score.ToString();
        }

    }

    //Show level completed panel. The function is called in the game manager.
    public void ShowCompleteLevelUI(int score)
    {
        completeLevelUI.SetActive(true);
        HUD.SetActive(false);
        lvlWonScore.text = score.ToString();
    }

    //Show game over panel. The function is called in the game manager.
    public void ShowGameOverUI(int score)
    {
        gameOverUI.SetActive(true);
        HUD.SetActive(false);
        lvllostScore.text = score.ToString("D");
    }

    public void HideCountdown()
    {
        countdownText.text = " ";
    }

    public void UpdateLifes(int lifes)
    {
        lifesUI.text = "Lifes: " + lifes.ToString();
    }
}
