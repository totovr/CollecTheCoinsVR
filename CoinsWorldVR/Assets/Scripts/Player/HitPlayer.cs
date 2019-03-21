using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    private int playerLivesCounter = 0;
    private float damageTaken = 0.01f; // Damage taken by the the ememy;
    private int playerLifes = 99; // This are the lifes of the player

    EffectSoundsManager effectSoundsManager;

    GamePGMController pgmControl;

    void Start()
    {
        effectSoundsManager = GetComponent<EffectSoundsManager>();
        pgmControl = GetComponent<GamePGMController>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("EnemyBullet") && GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {

            if (playerLivesCounter <= playerLifes)
            {
                // pgmControl.EnemyOneActuation();
                GameObject.FindGameObjectWithTag("HealthBar").SendMessage("UpdateHealthBar", damageTaken);
                effectSoundsManager.PlayerReceivedDamaged();
            }

            playerLivesCounter += 1;

            if (playerLivesCounter > playerLifes) // the player is death
            {
                playerLivesCounter = 0;
                effectSoundsManager.PlayerKilled();
                GameManager.sharedInstance.GameOver();
            }
        }
        else if (collider.gameObject.CompareTag("EnemyTwoBullet") && GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {

            if (playerLivesCounter <= playerLifes)
            {
                // pgmControl.EnemyTwoActuation();
                GameObject.FindGameObjectWithTag("HealthBar").SendMessage("UpdateHealthBar", damageTaken * 4);
                effectSoundsManager.PlayerReceivedDamaged();
            }

            playerLivesCounter += 4;

            if (playerLivesCounter > playerLifes) // the player is death
            {
                playerLivesCounter = 0;
                effectSoundsManager.PlayerKilled();
                GameManager.sharedInstance.GameOver();
            }
        }
    }
}
