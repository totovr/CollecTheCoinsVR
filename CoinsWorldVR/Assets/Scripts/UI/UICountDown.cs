using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class UICountDown : MonoBehaviour
{
    public static UICountDown sharedInstance;

    public static float TimerBonus = 0;

    public int GameTime = 60;

    private Text countDownText;

    private GameObject player;
    // private FirstPersonController characterControllerScript;
    private OVRPlayerController characterVRControllerScript;
    private AudioSource playerAudio;

    private float countDownTimerDuration;
    private float countDownTimerStartTime;

    string timerMessage;
    int timeLeft;

    [HideInInspector]
    public bool theGameIsCounting = true;

    private bool displayUserTimeRecord = true;

    void Awake()
    {
        sharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        countDownText = GetComponent<Text>();

        player = GameObject.FindGameObjectWithTag("PlayerFPS");
        // characterControllerScript = player.GetComponent<FirstPersonController>();
        characterVRControllerScript = player.GetComponent<OVRPlayerController>();
        playerAudio = player.GetComponent<AudioSource>();

        //characterControllerScript.m_WalkSpeed = 0.0f;
        //characterControllerScript.m_RunSpeed = 0.0f;

        characterVRControllerScript.Acceleration = 0.0f;
    }

    // This is a probe
    public void StartTheCountDown()
    {
        SetUpCountDownTimer(GameTime); // this is the time that will be provide to the user
    }

    // Update is called once per frame
    void Update()
    {
        // Save the current timer of the player to display it if he won 
        if (GameManager.sharedInstance.thePlayerWon == true && displayUserTimeRecord == true)
        {
            GlobalStaticVariables.totalTimer = timeLeft;
            displayUserTimeRecord = false;
        }

        if (GameManager.sharedInstance.theGameStart == true && theGameIsCounting == true)
        {
            // This is to add time if the player collect or kill some enemy
            if (TimerBonus > 0)
            {
                countDownTimerStartTime += TimerBonus;
                TimerBonus = 0; // reset the variable or it will generate a bug
            }

            timeLeft = (int)CountDownTimeRemaning();

            if (timeLeft > 0)
            {
                timerMessage = "Timer: " + LeadingZero(timeLeft);
            }
            else
            {
                timerMessage = "Game Over";
                StopPlayerMovement();
                GameManager.sharedInstance.GameOver();
                Debug.Log("The game is finished"); 
            }

            countDownText.text = timerMessage;
        }
    }

    // This will setup the timing 
    void SetUpCountDownTimer(float DelayInSeconds)
    {
        countDownTimerDuration = DelayInSeconds;
        countDownTimerStartTime = Time.time;
    }

    private float CountDownTimeRemaning()
    {
        float elapsedSeconds = Time.time - countDownTimerStartTime;
        float timeLeft = countDownTimerDuration - elapsedSeconds;
        return timeLeft;
    }

    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(3, '0');
    }

    public void PlayerMovement()
    {
        //characterControllerScript.m_WalkSpeed = 5.0f;
        //characterControllerScript.m_RunSpeed = 10.0f;
        characterVRControllerScript.Acceleration = 0.3f;
    }

    public void StopPlayerMovement()
    {
        //characterControllerScript.m_WalkSpeed = 0.0f;
        //characterControllerScript.m_RunSpeed = 0.0f;
        characterVRControllerScript.Acceleration = 0.0f;
    }

}
