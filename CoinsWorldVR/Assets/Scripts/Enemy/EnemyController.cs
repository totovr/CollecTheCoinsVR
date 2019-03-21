using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyAnimations enemyAnimator;

    //　Bullet object
    public GameObject bulletPrefab;
    public Transform muzzle;
    //　Speed of bullet
    [HideInInspector]
    public float bulletSpeed = 1200;

    // public Transform player;
    Transform player;
    [HideInInspector]
    public float rotate_speed = 0.5f;
    private float timeBtwShots;
    public float startTimeBtwShots;

    //  Shooting sound
    // private AudioSource audioSource;
    // public AudioClip shootSE;
    // public AudioClip deadSE;
    private EnemySoundEffect enemySound;

    // If the enemy is shoot the enemy will not be able to shoot
    private bool notShooted = true;
    private bool disableTheShootedSound = true;

    EnemyTypes enemyTypeDamage;

    private int monsterKilledUI = 1;

    void Shot()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * bulletSpeed);
        Destroy(bulletInstance, 3f);
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("PlayerBullet") || GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {
            // Debug.Log("Enemy shooted stone");
            enemyAnimator.EnemyIsDeath();

            if (disableTheShootedSound)
            {
                // audioSource.PlayOneShot(deadSE);
                enemySound.EnemyKilled();
            }

            disableTheShootedSound = false;
            notShooted = false;

            // Bonus time
            if (GameManager.sharedInstance.currentGameState != GameState.gameOver)
            {
                UICountDown.TimerBonus = enemyTypeDamage.EnemyValueCalculator(EnemyTypesEnum.STONE);
            }

            // Destroy the enemy after n time
            Invoke("destroyEnemy", 1.5f);
        }
    }

    void destroyEnemy()
    {
        GlobalStaticVariables.totalEnemyCounter -= monsterKilledUI;
        UIEnemies.sharedInstance.UpdateEnemyKilledUI();
        Destroy(gameObject);
    }

    void Start()
    {
        // audioSource = gameObject.AddComponent<AudioSource>();
        enemyAnimator = GetComponent<EnemyAnimations>();
        enemyTypeDamage = GetComponent<EnemyTypes>();
        enemySound = GetComponent<EnemySoundEffect>();
        player = GameObject.FindWithTag("PlayerFPS").transform;
        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        enemyAnimator.EnemyIsPossing();
        var relativePos = player.position - transform.position;
        var rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotate_speed);

        if (notShooted)
        {
            if (timeBtwShots <= 0)
            { 
                enemyAnimator.EnemyIsAttacking();
                // audioSource.PlayOneShot(shootSE);
                enemySound.EnemyShooted();
                Shot();
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }
}
