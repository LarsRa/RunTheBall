/*
 * This script manages the current state of the players health.
 * The life points of the player are stored here and the damage
 * from enemies reaches directly the player by calling the apply
 * damage function and updating the ui.
 * The game manager is only called, if the player dies.
 */
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //reference to the life bar in the top left corner
    public Image healthStats;
    private float health = 100f;

    //decrease life points, update ui and check if player is dead.
    public void applyDamage(float damage)
    {
        health -= damage;
        DisplayHealthStats(health);

        if (health <= 0f)
        {
            PlayerDied();
        }
    }

    //call game over method in game manager if the player died
    void PlayerDied()
    {
        FindObjectOfType<GameManager>().GameOver();
    }

    //display the life bar in the ui with current health values
    private void DisplayHealthStats(float healthValue)
    {
        healthValue /= 100;
        healthStats.fillAmount = healthValue;
    }

    public void SetFullLife()
    {
        health = 100f;
        DisplayHealthStats(health);
    }
}
