using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private GameObject healthObjectBar;
    private RectTransform healthBar;
    private Image imageColor;
    private Text healthText;

    private Color colorGreen;
    private Color colorYellow;
    private Color colorRed;

    private float maxLife = 1;
    private float lifeReduce;

    private string originalTextLife = "100%";

    void Start()
    {
        healthObjectBar = GameObject.FindGameObjectWithTag("HealthBarAnimation");
        healthBar = healthObjectBar.GetComponent<RectTransform>();
        imageColor = healthObjectBar.GetComponent<Image>();
        healthText = GameObject.FindGameObjectWithTag("LifeText").GetComponent<Text>();
        lifeReduce = maxLife;

        colorGreen = new Color32(105, 200, 86, 255);
        colorYellow = new Color32(255, 255, 0, 255);
        colorRed = new Color(255, 0, 0, 255);
    }

    // This will be called if the user was hit
    void UpdateHealthBar(float _hitDamage)
    {
        if (lifeReduce > 0.5)
        {
            healthText.text = IntToString(_hitDamage);
            imageColor.color = colorGreen;
        }
        else if (lifeReduce > 0.2)
        {
            healthText.text = IntToString(_hitDamage);
            imageColor.color = colorYellow;
        }
        else if (lifeReduce > 0)
        {
            healthText.text = IntToString(_hitDamage);
            imageColor.color = colorRed;
        }
    }

    string IntToString(float _hit)
    {
        lifeReduce -= _hit;
        healthBar.localScale = new Vector3(lifeReduce, 1, 1);
        return (Math.Round(lifeReduce, 2) * 100).ToString() + "%";
    }

    // call this from GameManager
    void ResetTheHealthBar()
    {
        lifeReduce = 1;
        healthBar.localScale = new Vector3(maxLife, 1, 1);
        healthText.text = originalTextLife;
        imageColor.color = colorGreen;
    }

}
