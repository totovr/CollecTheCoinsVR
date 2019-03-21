using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollected : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Coin"))
        {
            GameObject.FindGameObjectWithTag("PlayerScripts").SendMessage("CoinCollectedSound");
        }
    }
}
