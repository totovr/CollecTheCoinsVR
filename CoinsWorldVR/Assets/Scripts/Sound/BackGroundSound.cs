using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSound : MonoBehaviour
{

    public List<AudioClip> backGroundClips = new List<AudioClip>();

    private AudioSource backGroundAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        backGroundAudioSource = gameObject.AddComponent<AudioSource>();
        backGroundAudioSource.clip = backGroundClips[1];
        backGroundAudioSource.Play();
    }
 
}
