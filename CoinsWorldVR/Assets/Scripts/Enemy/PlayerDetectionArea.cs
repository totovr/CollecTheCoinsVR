using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionArea : MonoBehaviour
{
    SkinnedMeshRenderer enemyMesh;

    [HideInInspector]
    public bool enableTheController = false;

    void Start()
    {
        enemyMesh = GetComponent<SkinnedMeshRenderer>();
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("PlayerFPS") && GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {
            enemyMesh.enabled = true;
            enableTheController = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("PlayerFPS"))
        {
            enemyMesh.enabled = false;
            enableTheController = false;
        }
    }

}
