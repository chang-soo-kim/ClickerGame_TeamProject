using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectController : MonoBehaviour
{
    public AudioClip Buy_Sound;
    public AudioClip Click_Sound;
    public AudioClip GetGold_Sound;
    public AudioClip MonsterDie_Sound;
    public AudioClip Attack_Sound;
    public AudioClip UpgradeFail_Sound;
    public AudioClip UpgradeSuccess_Sound;
    AudioSource audioSource;

    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void PlaySound(string a)
    {
        switch(a)
        {
            case "Buy":
                audioSource.clip = Buy_Sound;
                break;
            case "Click":
                audioSource.clip = Click_Sound;
                break;
            case "GetGold":
                audioSource.clip = GetGold_Sound;
                break;
            case "MonsterDie":
                audioSource.clip = MonsterDie_Sound;
                break;
            case "Attack":
                audioSource.clip = Attack_Sound;
                break;
            case "UpgradeFail":
                audioSource.clip = UpgradeFail_Sound;
                break;
            case "UpgradeSuccess":
                audioSource.clip = UpgradeSuccess_Sound;
                break;
            
        }
        audioSource.Play();
    }
}
