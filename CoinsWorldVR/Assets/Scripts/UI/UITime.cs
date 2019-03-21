using UnityEngine;
using UnityEngine.UI;
using System;

public class UITime : MonoBehaviour
{

    private Text _textClock;

    GameObject worldLight;
    private Transform _sunTransform;
    
    string hour;
    string minutes;
    string seconds;

    // Start is called before the first frame update
    void Start()
    {
        _textClock = GetComponent<Text>();
        worldLight = GameObject.FindGameObjectWithTag("WorldLight");
        _sunTransform = worldLight.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        DateTime Now = DateTime.Now;

        hour = LeadingZero(Now.Hour);
        minutes = LeadingZero(Now.Minute);
        seconds = LeadingZero(Now.Second);

        _textClock.text = hour + ":" + minutes + ":" + seconds; // Current time
        _sunTransform.transform.eulerAngles = new Vector3(6 * Now.Minute + Now.Second / 10.0f, 0, 0);
    }

    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0'); // Number of int and when is 0 to the left with wich character we will fill
    }
}
