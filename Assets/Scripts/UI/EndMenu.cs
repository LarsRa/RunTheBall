/*
 * This menu is used in the end scene after all three levels are completed.
 * It shows the total score and has functions to exit the game, get back to lobby or
 * restart the last level.
 */
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public Text score;
    private GameManager gameManager;

    //load score from game manager and display it in the end scene
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (SceneManager.GetActiveScene().name == "End") { 
            score.text = gameManager.TotalScore.ToString();
        }
    }

    //exit the game
    public void QuitGame()
    {
        Application.Quit();
    }

    //get back to the lobby by loading start scene and reset the score
    public void LoadLobby()
    {
        gameManager.TotalScore = 0;
        SceneManager.LoadScene("Start");
    }

    //restart level by reloading the scene and call the game manager to reset current values
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameManager.InitNewLevel();
    }
}
