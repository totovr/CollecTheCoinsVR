using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNotStaticController : MonoBehaviour
{
    //　Bullet object
    public GameObject bulletPrefab;
    public Transform muzzle;
    //　Speed of bullet
    [HideInInspector]
    public float bulletSpeed = 700f;
    //  Animation
    private EnemyNotStaticAnimations enemyAnimatorPositions;

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    Transform player;
    [HideInInspector]
    public float rotate_speed = 0.5f;
    private float timeBtwShots;
    public float startTimeBtwShots;

    //  Shooting sound
    //private AudioSource audioSource;
    //public AudioClip shootSE;
    //public AudioClip deadSE;
    private EnemySoundEffect enemySound;

    // If the enemy is shoot the enemy will not be able to shoot
    private bool notShooted = true;
    private bool disableTheShootedSound = true;

    EnemyTypes enemyTypeDamage;

    private int monsterKilledUI = 1;

    void Shot()
    {
        var bulletInstance = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation) as GameObject;
        bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * bulletSpeed);
        Destroy(bulletInstance, 3f);
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("PlayerBullet") || GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {
            // Debug.Log("Enemy shooted ghost");
            enemyAnimatorPositions.EnemyIsDeath(); // The enemy animation change to killed

            if (disableTheShootedSound)
            {
                enemySound.EnemyKilled();
            }

            disableTheShootedSound = false;
            notShooted = false;

            // Bonus time
            if (GameManager.sharedInstance.currentGameState != GameState.gameOver)
            {
                UICountDown.TimerBonus = enemyTypeDamage.EnemyValueCalculator(EnemyTypesEnum.GHOST);
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
        enemyAnimatorPositions = GetComponent<EnemyNotStaticAnimations>();
        enemyTypeDamage = GetComponent<EnemyTypes>();
        enemySound = GetComponent<EnemySoundEffect>();
        // player = GameObject.FindWithTag("PlayerFPS").transform;
        player = GameObject.FindWithTag("PlayerScripts").transform;
        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        enemyAnimatorPositions.EnemyIsMoving();
        var relativePos = player.position - transform.position;
        var rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotate_speed);

        if (notShooted)
        {
            if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else if (Vector3.Distance(transform.position, player.position) < stoppingDistance && Vector3.Distance(transform.position, player.position) > retreatDistance)
            {
                transform.position = transform.position;
            }
            else if (Vector3.Distance(transform.position, player.position) < retreatDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }

            if (timeBtwShots <= 0)
            {
                enemyAnimatorPositions.EnemyIsAttacking();
                //audioSource.PlayOneShot(shootSE);
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
