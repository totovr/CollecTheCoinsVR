using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("bullet destroyed");
        Destroy(gameObject);
    }
}
