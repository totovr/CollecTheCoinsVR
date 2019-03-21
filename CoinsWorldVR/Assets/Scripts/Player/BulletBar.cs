using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletBar : MonoBehaviour
{
    private GameObject reloadObjectBar;
    private RectTransform reloadBar;
    private Image imageColor;

    private GameObject reloadTextObject;
    private Text reloadText;
    private RectTransform reloadBarTextTransform;
    private RectTransform reloadBarTextModified;

    private Image backGroundColor;

    private Color colorFucsia;
    private Color colorPurple;
    private Color colorOrange;
    private Color colorRed;

    private float maxBullets = 1;
    private float bulletReduce;
    private int chargerEmpty;

    private string originalTextLife = "5";
    public int bullets = 5;

    void Start()
    {
        reloadObjectBar = GameObject.FindGameObjectWithTag("ReloadBarAnimation");
        reloadBar = reloadObjectBar.GetComponent<RectTransform>();
        imageColor = reloadObjectBar.GetComponent<Image>();
        backGroundColor = GetComponent<Image>();

        reloadTextObject = GameObject.FindGameObjectWithTag("ReloadText");
        reloadText = reloadTextObject.GetComponent<Text>();
        reloadBarTextTransform = reloadTextObject.GetComponent<RectTransform>();

        bulletReduce = maxBullets;

        colorFucsia = new Color32(200, 86, 104, 255);
        colorPurple = new Color32(132, 86, 200, 255);
        colorOrange = new Color32(200, 139, 80, 255);
        colorRed = new Color32(255, 0, 0, 255);

        reloadBarTextModified = reloadBarTextTransform;
    }

    // This will be called if the user was hit
    void UpdateReloadBar(float _hitDamage)
    {
        bullets -= 1;
        // Debug.Log(bullets);

        if (bullets > 3)
        {
            reloadText.text = IntToString(_hitDamage);
            imageColor.color = colorPurple;
        }
        else if (bullets > 2)
        {
            reloadText.text = IntToString(_hitDamage);
            imageColor.color = colorOrange;
        }
        else if (bullets > 0)
        {
            reloadText.text = IntToString(_hitDamage);
            imageColor.color = colorOrange;
        }

        if (bullets == 0)
        {
            reloadText.text = IntToString(_hitDamage);
            // reloadBarTextModified.transform.eulerAngles = Vector3(0, 0, 0);
            // reloadText.text = Reloading();
            backGroundColor.color = colorRed; 
        }
    }

    string Reloading()
    {
        return "Reloading";
    }

    string IntToString(float _hit)
    {
        bulletReduce -= _hit;
        reloadBar.localScale = new Vector3(bulletReduce, 1, 1);
        double currentBullet = Math.Round(bulletReduce, 2) * 5;
        string textToDisplay = (currentBullet).ToString();
        return textToDisplay;
    }

    // call this from GameManager
    void ResetTheReloadBar()
    {
        bullets = 5;
        bulletReduce = 1;
        reloadBar.localScale = new Vector3(maxBullets, 1, 1);
        reloadText.text = originalTextLife;
        imageColor.color = colorFucsia;
        backGroundColor.color = new Color(255, 255, 255, 255);
        // reloadBarTextTransform.rotation = Quaternion.Euler(0, 0, -90);
    }
}
