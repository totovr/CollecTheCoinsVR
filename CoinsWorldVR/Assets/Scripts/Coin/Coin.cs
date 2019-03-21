using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoinType { BRONZE, SILVER, GOLD };

public class Coin : MonoBehaviour
{
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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") || collider.CompareTag("PlayerFPS"))
        {
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

}
