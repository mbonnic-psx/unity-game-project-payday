using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // Maximum health of the player
    public int currentHealth; // Current health of the player
    public float invincibilityDuration = 5.0f; // Duration of invincibility after taking damage
    public MonsterAI monsterAI; // Reference to the MonsterAI script
    public GameObject LoseState;
    public GameObject player;

    private bool isInvincible = false; // Flag to track invincibility state

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to maximum health
        LoseState.SetActive(false);
    }

    void Update()
    {
        if (monsterAI.playerInAttackRange && !isInvincible) // Check if the player is in attack range and not invincible
        {
            TakeDamage(1); // Take damage if the player is in attack range
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Player is dead!"); // Log when the player is dead
            // Add any additional logic for player death here (e.g., respawn, game over screen, etc.)
            Die(); // Call the Die method to handle player death
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible && currentHealth > 0)
        {
            currentHealth -= damage; // Reduce current health by damage amount
            Debug.Log("Player took damage! Current health: " + currentHealth);
            StartCoroutine(InvincibilityCoroutine()); // Start invincibility period
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true; // Set invincibility flag
        Debug.Log("Player is now invincible.");

        // Optional: Add visual feedback here (e.g., flashing effect)

        yield return new WaitForSeconds(invincibilityDuration); // Wait for the duration of invincibility

        isInvincible = false; // Reset invincibility flag
        Debug.Log("Player is no longer invincible.");
    }

    public void Die()
    {
        LoseState.SetActive(true);
        player.SetActive(false);
    }
}
