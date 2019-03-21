using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTypesEnum
{
    STONE,
    GHOST
}
public class EnemyTypes : MonoBehaviour
{
    // Type of enemy
    private int monsterValue = 1;

    public int EnemyValueCalculator(EnemyTypesEnum _enemyType)
    {

        int _typeValue = (int)_enemyType;
        if (_typeValue == 0)
        {
            monsterValue = 3; // add 3 seconds for the first enemy
        }
        else if (_typeValue == 1)
        {
            monsterValue = _typeValue * 5;
        }

        return monsterValue;
    }
}




