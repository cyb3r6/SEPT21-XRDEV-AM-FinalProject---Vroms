using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource ambientSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnEnable()
    {
        PlayBackgroundSounds();
    }

    public void PlaySound(AudioClip clip, AudioSource audioSource)
    {
        if (clip && audioSource)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void StopBackgroundSound()
    {
        ambientSound.Stop();
    }

    public void PlayBackgroundSounds()
    {
        ambientSound.Play();
    }
}
