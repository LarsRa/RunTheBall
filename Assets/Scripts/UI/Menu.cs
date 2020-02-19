/*
 * This menu is used at the start scene and after a level is completed.
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //load next scene after a level is completed or after game started
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        FindObjectOfType<GameManager>().InitNewLevel();
    }

    //exit the game by clicking quit button
    public void QuitGame()
    {
        Application.Quit();
    }

    //get back to the lobby (start scene)
    public void LoadLobby()
    {
        SceneManager.LoadScene("Start");
    }
}
