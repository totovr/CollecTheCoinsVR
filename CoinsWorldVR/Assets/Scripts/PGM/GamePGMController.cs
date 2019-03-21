using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePGMController : MonoBehaviour
{
    public GamePGMHandler serialHandlerPGM;

    public void EnemyOneActuation()
    {
        serialHandlerPGM.Write("1A");
        // Debug.Log("The PGM is actuated");
    }

    public void EnemyTwoActuation()
    {
        serialHandlerPGM.Write("2A");
        // Debug.Log("The PGM is actuated");
    }

    public void RelaxThePGM()
    {
        serialHandlerPGM.Write("0A");
        // Debug.Log("The PGM is not actuated");
    }
}
