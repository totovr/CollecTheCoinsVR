using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoinType { BRONZE, SILVER, GOLD };

public class Coin : MonoBehaviour
{
    public List<AudioClip> CoinsAudio = new List<AudioClip>();
    private AudioSource effectsAudioSource;
    int randomNumber;

    public CoinType Type;

    private CoinType _type;

    public Material[] CoinMaterials;

    public static int CoinsCounter = 0;

    private int coinValue = 1;

    CoinType coinMaterial
    {
        get
        {
            return _type;
        }

        set
        {
            _type = value;

            int _typeValue = (int)_type;

            Material _mat = CoinMaterials[_typeValue];

            Renderer Rend = GetComponent<Renderer>();

            if (Rend != null)
            {
                Rend.material = _mat;
                coinValue = 1 + (int)Mathf.Pow(_typeValue, 2); // here the value of the coin is modified internally
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        coinMaterial = Type; // setup all the features of the coin 
        CoinsCounter += coinValue;
    }

    void Start()
    {
        effectsAudioSource = gameObject.AddComponent<AudioSource>();
        // effectsAudioSource.priority = 1;
    }

    private void OnTriggerEnter(Collider collider)
    {
        // collider.CompareTag("Player") || collider.CompareTag("PlayerFPS")
        if (collider.CompareTag("PlayerScripts") || collider.CompareTag("PlayerFPS"))
        {
            CoinCollectedSound();

            CoinsCounter -= coinValue;
            UICoins.sharedInstance.UpdateCoins();

            if (GameManager.sharedInstance.currentGameState != GameState.gameOver)
            {
                UICountDown.TimerBonus = coinValue * 3;
            }

            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (CoinsCounter <= 0)
        {
            UICountDown.sharedInstance.theGameIsCounting = false;
            // Invoke("GameManager.sharedInstance.GameWon", 2);
            GameManager.sharedInstance.GameWon();
        }
    }

    void CoinCollectedSound()
    {
        Debug.Log("Sound");
        randomNumber = 0;
        effectsAudioSource.clip = CoinsAudio[randomNumber];
        //effectsAudioSource.priority = 1;
        //effectsAudioSource.volume = 1;
        effectsAudioSource.Play();
    }

}
