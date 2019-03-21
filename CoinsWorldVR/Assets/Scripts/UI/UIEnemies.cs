using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemies : MonoBehaviour
{
    private Text enemyText;
    private int targetEnemies;
    private int remaningEnemies;
    private int enemiesKilled;

    static public UIEnemies sharedInstance;

    void Awake()
    {
        sharedInstance = this;
    }

    void Start()
    {
        enemyText = GetComponent<Text>();
        targetEnemies = GlobalStaticVariables.totalEnemyCounter;
        UpdateEnemyKilledUI();
    }

    public void UpdateEnemyKilledUI()
    {
        remaningEnemies = GlobalStaticVariables.totalEnemyCounter;
        enemiesKilled = targetEnemies - remaningEnemies;

        if (remaningEnemies > 0)
        {
            enemyText.text = "Enemies defeated: \n <color='yellow'>" + enemiesKilled + "/ " + targetEnemies + " </color>";
        }
        else
        {
            enemyText.text = "<color='green'>All enemies killed</color>";
        }
    }
}
