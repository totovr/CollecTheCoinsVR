using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNotStaticAnimations : MonoBehaviour
{
    private Animator enemyAnimationsPositions;

    void Start()
    {
        enemyAnimationsPositions = GetComponent<Animator>();
    }

    public void EnemyIsPossing()
    {
        enemyAnimationsPositions.SetBool("Wait_flag", true);
        enemyAnimationsPositions.SetBool("Run_flag", false);
    }

    public void EnemyIsMoving()
    {
        enemyAnimationsPositions.SetBool("Wait_flag", false);
        enemyAnimationsPositions.SetBool("Run_flag", true);
    }

    public void EnemyIsAttacking()
    {
        enemyAnimationsPositions.SetBool("Run_flag", false);
        enemyAnimationsPositions.SetBool("Attack_flag", true);
    }

    public void EnemyIsDeath()
    {
        enemyAnimationsPositions.SetBool("Run_flag", false);
        enemyAnimationsPositions.SetBool("Dead_flag", true);
    }
}
