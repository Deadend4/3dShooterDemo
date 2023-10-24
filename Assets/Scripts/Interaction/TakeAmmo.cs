using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAmmo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private int ammoCount;
    [SerializeField] private AmmoCalculation ammoCalculation;
    [SerializeField] AudioSource audioSource;

    public int AmmoCount
    {
        get
        {
            return ammoCount;
        }
    }

    public WeaponData WeaponData
    {
        get
        {
            return weaponData;
        }
    }
    public void RecalculateAmmo()
    {
        weaponData.InventoryAmmo += ammoCount;
        audioSource.PlayOneShot(weaponData.TriggerSound, 0.2f);
        if (ammoCalculation.enabled)
            ammoCalculation.RefreshInventoryUI();

        Destroy(this.transform.gameObject);
    }
}
