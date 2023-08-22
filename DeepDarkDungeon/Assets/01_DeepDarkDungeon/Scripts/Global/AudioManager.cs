using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayMusic("TitleBGM");
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }

    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("SFX Not Found");
        }

        else
        {
            sfxSource.PlayOneShot(sound.clip);
        }

    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    //public AudioClip bgmClip;

    //[Header("Player Clip")]
    //public AudioClip playerStepClip;
    //public AudioClip playerDamageClip;

    //[Header("Item Clip")]
    //public AudioClip coinClip;
    //public AudioClip itemEquipClip;

    //public AudioClip retryClip;

    //private AudioSource audioSource;


    //// Start is called before the first frame update
    //void Start()
    //{
    //    audioSource = GetComponent<AudioSource>();
    //    audioSource.clip = bgmClip;
    //}


    //public void PlayerStepSound()
    //{
    //    audioSource.PlayOneShot(playerStepClip);
    //}
    //public void PlayerDamageSound()
    //{
    //    audioSource.PlayOneShot(playerDamageClip);
    //}
    //public void ItemEquipSound()
    //{
    //    audioSource.PlayOneShot(itemEquipClip);
    //}
    //public void CoinSound()
    //{
    //    audioSource.PlayOneShot(coinClip);
    //}


    //public void RetrySound()
    //{
    //    audioSource.clip = retryClip;
    //    audioSource.Play();
    //}


}
