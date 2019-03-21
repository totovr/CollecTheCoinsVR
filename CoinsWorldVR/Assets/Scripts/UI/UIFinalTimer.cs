using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFinalTimer : MonoBehaviour
{

    private Text timer;
    private string timerMessage;
    private int userTime;

    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.thePlayerWon == true)
        {
            userTime = GlobalStaticVariables.totalTimer;
            timerMessage = "Your time was: " + LeadingZero(userTime);
            timer.text = timerMessage;
        }
    }

    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(3, '0');
    }
}
