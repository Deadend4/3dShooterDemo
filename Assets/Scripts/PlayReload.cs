using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayReload : MonoBehaviour
{
    WeaponShoot weaponShoot;
    WeaponData weaponData;
    AudioSource audioSource;
    private float volume = 0.2f;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        weaponShoot = FindObjectOfType<WeaponShoot>();
    }
    public void ReloadImpact1()
    {
        weaponData = weaponShoot.WeaponData;
        if (weaponData.name.Equals("1_PM"))
        {
            volume = 0.2f;
        }
        else
        {
            volume = 0.5f;
        }
        audioSource.PlayOneShot(weaponData.ReloadSound1, volume);
    }
    public void ReloadImpact2()
    {
        audioSource.PlayOneShot(weaponData.ReloadSound2, volume);
    }
    public void ReloadImpact3()
    {
        audioSource.PlayOneShot(weaponData.ReloadSound3, volume);
    }
}
