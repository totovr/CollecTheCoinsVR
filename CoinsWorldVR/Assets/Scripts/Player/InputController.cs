using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    public float fireRate = 0.3f;
    float nextFire = 0.0F;

    private EffectSoundsManager shootSound;

    private float bulletDamage = 0.2f;
    private bool playerReloading = false;
    private int bulletCounter = 0;
    private float bulletReloadDelay = 2.0f;
    private float countDownTimerStartTime;

    private GameObject OVRculusController;
    private OVRPlayerController playerController;

    void Start()
    {
        shootSound = GetComponent<EffectSoundsManager>();
        OVRculusController = GameObject.FindGameObjectWithTag("PlayerFPS");
        playerController = OVRculusController.GetComponent<OVRPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // change this once we use VR
        // (Input.GetKey(KeyCode.Mouse0)) && Time.time > nextFire
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) && Time.time > nextFire && GameManager.sharedInstance.currentGameState == GameState.inTheGame && playerReloading == false)
        {
            nextFire = Time.time + fireRate;
            shootSound.ShootTheGunSound();
            GameObject.FindGameObjectWithTag("ReloadBar").SendMessage("UpdateReloadBar", bulletDamage);
            BulletGenerator.sharedInstance.GenerateBullet();
            bulletCounter += 1;
            countDownTimerStartTime = Time.time;
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) && GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {
            playerController.Acceleration = 0.25f;
        }

        //if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) && GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        //{
        //    playerController.Jump();
        //}

        if (bulletCounter >= 5)
        {
            playerReloading = true;
            // this will start to calculate the slapsed time 
            float timeToReload = CountDownTimeRemaning(bulletReloadDelay, countDownTimerStartTime);
            
            if(timeToReload <= 0)
            {
                bulletCounter = 0;
                playerReloading = false;
                GameObject.FindGameObjectWithTag("ReloadBar").SendMessage("ResetTheReloadBar");
            }
        }
    }

    private float CountDownTimeRemaning(float _bulletReloadDelay, float _countDownTimerStartTime)
    {
        float _elapsedSeconds = Time.time - _countDownTimerStartTime;
        float _timeLeft = _bulletReloadDelay - _elapsedSeconds;
        return _timeLeft;
    }

}
