using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundEffect : MonoBehaviour
{
    public List<AudioClip> ShootAudio = new List<AudioClip>();
    public List<AudioClip> KilledAudio = new List<AudioClip>();

    private AudioSource effectsAudioSource;

    int randomNumberKill;
    int randomNumberShoot;

    // Start is called before the first frame update
    void Start()
    {
        effectsAudioSource = gameObject.AddComponent<AudioSource>();
        randomNumberShoot = Random.Range(0, 3);
        randomNumberKill = Random.Range(0, 4);
    }

    public void EnemyShooted()
    {
        effectsAudioSource.clip = ShootAudio[randomNumberShoot];
        effectsAudioSource.Play();
    }

    public void EnemyKilled()
    {
        effectsAudioSource.clip = KilledAudio[randomNumberKill];
        effectsAudioSource.Play();
    }
}
