using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICoins : MonoBehaviour
{
    private Text _coinsText;
    private int _targetCoins;
    private int _remaningCoins;
    private int _collectedCoins;

    static public UICoins sharedInstance;

    void Awake()
    {
        sharedInstance = this;
    }

    void Start()
    {
        _coinsText = GetComponent<Text>();
        _targetCoins = Coin.CoinsCounter;
        UpdateCoins();
    }

    public void UpdateCoins()
    {
        _remaningCoins = Coin.CoinsCounter;
        _collectedCoins = _targetCoins - _remaningCoins;

        if (_remaningCoins > 0)
        {
            _coinsText.text = "Collected coins: \n <color='yellow'>" + _collectedCoins + "/ " + _targetCoins + " </color>";
        }
        else
        {
            _coinsText.text = "<color='green'>Congratulations you have won the game</color>";
        }
    }
}
