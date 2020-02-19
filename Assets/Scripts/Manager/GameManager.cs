/*
 * The game manager stores values about the state of the game.
 * It has functions to end the level, complete a level or loose a round.
 * The values like score are adjusted to the current state of the game.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //customizable default values for game options
    public int nrOfLifes = 3;
    public float levelTime = 180f;
    public float countdown = 3f;

    //representing state of game
    private bool gameStarted, gameEnded, timeout;

    //persistent value to store the score of all level
    private static int totalScore = 0;

    //level variables
    private int score = 0;
    private int lifes = 3;
    private float timer = 999;
    private float countDownTimer = 999;
    private float timeoutTimer;

    /*----------------------------------------------------------------------------
     *                   Getter and setter for variables
     *      Getter mostly used for displaying values by the level manager
     *----------------------------------------------------------------------------
     */

    //Increase score after collision with item
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score += value;
        }
    }

    //To reset the total score to 0 finishing the game and restart it.
    public int TotalScore
    {
        get
        {
            return totalScore;
        }
        set
        {
            totalScore = value;
        }
    }

    public bool GameStarted
    {
        get
        {
            return gameStarted;
        }
    }

    public bool GameEnded
    {
        get
        {
            return gameEnded;
        }
    }

    public bool Timeout
    {
        get
        {
            return timeout;
        }
    }

    public int CountdownTimer
    {
        get
        {
            return (int)countDownTimer + 1;
        }
    }

    public float Timer
    {
        get
        {
            return timer;
        }
    }

    //Singelton pattern of the game manager----------------------------------------------------
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /*---------------------------------------------------------------------------
     *                    Methods for updating game state
     * --------------------------------------------------------------------------
     */

    //update timer and countdown at start. end the game, if the time is up.
    private void Update()
    {
        //update a timeout of 1 sec before countdown starts
        if (timeout)
        {
            timeoutTimer -= Time.deltaTime;
            if (timeoutTimer <= 0)
            {
                timeout = false;
            }
        }
        //update countdown timer after timeout
        else if (!gameStarted)
        {
            countDownTimer -= Time.deltaTime;
            if (countDownTimer <= 0)
            {
                gameStarted = true;
                LevelManager levelManager = FindObjectOfType<LevelManager>();
                if (levelManager != null)
                {
                    levelManager.HideCountdown();
                }
            }
        }
        //update game timer after countdown
        else if (gameStarted && !gameEnded)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                GameOver();
            }
        }
    }

    //set the score for the level to 0 and call the level manager to show the game over ui 
    public void GameOver()
    {
        gameEnded = true;
        FindObjectOfType<LevelManager>().ShowGameOverUI(score);
        score = 0;
    }

    //set level to complete, add score to total score and show related ui.
    public void CompleteLevel()
    {
        gameEnded = true;
        totalScore += score;
        FindObjectOfType<LevelManager>().ShowCompleteLevelUI(totalScore);
    }

    //decrease life if player dies
    public void LostRound()
    {
        if (!gameEnded)
        {
            lifes--;
            FindObjectOfType<LevelManager>().UpdateLifes(lifes);
            if (lifes == 0)
            {
                GameOver();
            }
        }
    }

    //set values back to default for a new level start
    public void InitNewLevel()
    {
        lifes = nrOfLifes;
        timer = levelTime;
        countDownTimer = countdown;
        timeoutTimer = 1f;
        score = totalScore;
        timeout = true;
        gameEnded = false;
        gameStarted = false;
    }
}
