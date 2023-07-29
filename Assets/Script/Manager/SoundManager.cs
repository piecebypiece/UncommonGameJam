using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bgm1;
    public AudioClip bgm2;

    private void Start()
    {
        audioSource.clip = bgm1;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void ChangeSound()
    {
        if (audioSource.clip != bgm1)
            return;
        audioSource.clip = bgm2;
        audioSource.Play();
    }
}
