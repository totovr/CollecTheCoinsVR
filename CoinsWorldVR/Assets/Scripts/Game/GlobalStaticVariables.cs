using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStaticVariables : MonoBehaviour
{
    [SerializeField]
    public static bool theUserResetTheGame = false;
    [SerializeField]
    public static int totalEnemyCounter = 0;
    [SerializeField]
    public static int totalTimer = 0;
}
