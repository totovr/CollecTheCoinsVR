using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNotStaticMovement : MonoBehaviour
{
    EnemyNotStaticController enemyControllerScript;
    PlayerDetectionArea playerDetectionArea;
    

    void Start()
    {
        enemyControllerScript = gameObject.GetComponentInParent<EnemyNotStaticController>();
        playerDetectionArea = GetComponent<PlayerDetectionArea>();
    }

    void Update()
    {
        EnableTheController();
    }

    void EnableTheController()
    {

        if (playerDetectionArea.enableTheController == false || GameManager.sharedInstance.currentGameState != GameState.inTheGame)
        {
            enemyControllerScript.enabled = false;
        }
        else if (playerDetectionArea.enableTheController == true && GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {
            enemyControllerScript.enabled = true;
        }

    }
}
