using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip bgmClip;
    public AudioClip bowClip;
    public AudioClip magicClip;
    public AudioClip hitClip;
    public AudioClip dieClip;
    public AudioClip laughClip;
    public AudioClip payClip;
    public AudioClip clickClip;
    public AudioClip sellClip;
    public AudioClip buildClip;
    public AudioClip magicTowerClip;
    public AudioClip upgradeClip;
    public AudioClip beepClip;
    public AudioClip retryClip;

    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgmClip;
    }
    public void PlayMusic()
    {
        // audioSource.Play(); // 노래 재생
    }


    public void BowSound()
    {
        audioSource.PlayOneShot(bowClip);
    }

    public void MagicSound()
    {
        audioSource.PlayOneShot(magicClip);
    } 
    public void HitSound()
    {
        audioSource.PlayOneShot(hitClip);
    }
    public void DieSound()
    {
        audioSource.PlayOneShot(dieClip);
    }
    public void LaughSound()
    {
        audioSource.PlayOneShot(laughClip);
    }
    public void PaySound()
    {
        audioSource.PlayOneShot(payClip);
    }
    public void ClickSound()
    {
        audioSource.PlayOneShot(clickClip);
    }
    public void SellSound()
    {
        audioSource.PlayOneShot(clickClip);
    }
    public void BuildSound()
    {
        audioSource.PlayOneShot(buildClip);
    }
    public void UpgradeSound()
    {
        audioSource.PlayOneShot(upgradeClip);
    }
    public void MagicTowerSound()
    {
        audioSource.PlayOneShot(magicTowerClip);
    }
    public void BeepSound()
    {
        audioSource.PlayOneShot(beepClip);
    }
    public void RetrySound()
    {
        audioSource.clip = retryClip;
        audioSource.Play();
    }
    

}
