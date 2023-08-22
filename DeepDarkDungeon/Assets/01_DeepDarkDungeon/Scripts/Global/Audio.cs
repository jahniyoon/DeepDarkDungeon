using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip bgmClip;

    [Header("Player Clip")]
    public AudioClip playerStepClip;
    public AudioClip playerDamageClip;

    [Header("Item Clip")]
    public AudioClip coinClip;
    public AudioClip itemEquipClip;

    public AudioClip retryClip;

    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgmClip;
    }


    public void PlayerStepSound()
    {
        audioSource.PlayOneShot(playerStepClip);
    }
    public void PlayerDamageSound()
    {
        audioSource.PlayOneShot(playerDamageClip);
    }
    public void ItemEquipSound()
    {
        audioSource.PlayOneShot(itemEquipClip);
    }
    public void CoinSound()
    {
        audioSource.PlayOneShot(coinClip);
    }

  
    public void RetrySound()
    {
        audioSource.clip = retryClip;
        audioSource.Play();
    }
    

}
