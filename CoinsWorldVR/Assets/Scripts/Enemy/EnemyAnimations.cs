using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private Animator enemyAnimationsPositions;

    void Start()
    {
        enemyAnimationsPositions = GetComponent<Animator>();
        // enemyAnimationsPositions.SetBool("Idle_flag", true);
    }

    public void EnemyIsPossing()
    {
        enemyAnimationsPositions.SetBool("Idle_flag", true);
        enemyAnimationsPositions.SetBool("Attack_flag", false);
    }

    public void EnemyIsAttacking()
    {
        enemyAnimationsPositions.SetBool("Idle_flag", false);
        enemyAnimationsPositions.SetBool("Attack_flag", true);
    }

    public void EnemyIsDeath()
    {
        enemyAnimationsPositions.SetBool("Attack_flag", false);
        enemyAnimationsPositions.SetBool("Idle_flag", false);
        enemyAnimationsPositions.SetBool("Dead_flag", true);
    }
}
