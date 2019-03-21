using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    // GameObject worldLight;
    private Transform sunTransform;

    // string hour;
    string minutes;
    string seconds;

    // Start is called before the first frame update
    void Start()
    {
        // worldLight = GameObject.FindGameObjectWithTag("WorldLight");
        sunTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        DateTime Now = DateTime.Now;

        // hour = LeadingZero(Now.Hour);
        minutes = LeadingZero(Now.Minute);
        seconds = LeadingZero(Now.Second);

        sunTransform.transform.eulerAngles = new Vector3(6 * Now.Minute + Now.Second / 10.0f, 0, 0);
    }

    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0'); // Number of int and when is 0 to the left with wich character we will fill
    }
}
