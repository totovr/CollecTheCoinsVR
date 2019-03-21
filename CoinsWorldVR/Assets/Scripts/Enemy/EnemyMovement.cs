using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyController enemyControllerScript;
    PlayerDetectionArea playerDetectionArea;


    void Start()
    {
        enemyControllerScript = gameObject.GetComponentInParent<EnemyController>();
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
