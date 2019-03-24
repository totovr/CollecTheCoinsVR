using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundsManager : MonoBehaviour
{
    public List<AudioClip> ShootAudio = new List<AudioClip>();
    public List<AudioClip> DamageAudio = new List<AudioClip>();
    public List<AudioClip> KilledAudio = new List<AudioClip>();

    private AudioSource effectsAudioSource;

    int randomNumber;

    // Start is called before the first frame update
    void Start()
    {
        effectsAudioSource = gameObject.AddComponent<AudioSource>();
        effectsAudioSource.priority = 200;
    }

    public void ShootTheGunSound()
    {
        effectsAudioSource.clip = ShootAudio[0];
        effectsAudioSource.Play();
    }

    public void PlayerReceivedDamaged()
    {
        int rand = Random.Range(0, 3);
        effectsAudioSource.clip = DamageAudio[rand];
        effectsAudioSource.Play();
    }

    public void PlayerKilled()
    {
        int rand = Random.Range(0, 3);
        effectsAudioSource.clip = KilledAudio[rand];
        effectsAudioSource.Play();
    }
}
