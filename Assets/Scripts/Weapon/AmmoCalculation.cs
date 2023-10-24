using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net;

public class AmmoCalculation : MonoBehaviour
{
    //[SerializeField] private int inventoryAmmo = 200;
    [SerializeField] TMP_Text currentAmmoText;
    [SerializeField] TMP_Text invetoryAmmoText;
    //private int currentAmmo = 5;
    private Weapon weapon;
    private WeaponData weaponData;
    void Start()
    {
        weapon = GetComponent<Weapon>();
        weaponData = weapon.WeaponData;
        RefreshCurrentAmmoUI();
        RefreshInventoryUI();
    }

    public void RemoveBullet()
    {
        weaponData.CurrentAmmo--;
        //currentAmmo--;
        RefreshCurrentAmmoUI();
    }
    public void RemoveBullet(int count)
    {
        if (weaponData.CurrentAmmo > count)
        {
            weaponData.CurrentAmmo -= count;
        }
        else
        {
            weaponData.CurrentAmmo = 0;
        }
        RefreshCurrentAmmoUI();
    }

    public void AmmoReload()
    {
        if (weaponData.InventoryAmmo > weaponData.MaxAmmo - weaponData.CurrentAmmo)
        {
            weaponData.InventoryAmmo -= weaponData.MaxAmmo - weaponData.CurrentAmmo;
            weaponData.CurrentAmmo = weaponData.MaxAmmo;
        }
        else if (weaponData.InventoryAmmo > 0)
        {
            weaponData.CurrentAmmo += weaponData.InventoryAmmo;
            weaponData.InventoryAmmo = 0;
        }


        RefreshCurrentAmmoUI();
        RefreshInventoryUI();
    }

    public void RefreshCurrentAmmoUI()
    {
        currentAmmoText.text = weaponData.CurrentAmmo.ToString();
    }
    public void RefreshInventoryUI()
    {
        invetoryAmmoText.text = weaponData.InventoryAmmo.ToString();
    }
    public void SetNewWeaponData(WeaponData newWeaponData)
    {
        weaponData = newWeaponData;
    }
    // public int CurrentAmmo
    // {
    //     get
    //     {
    //         return weaponData.CurrentAmmo;
    //     }
    //     set
    //     {
    //         this.weaponData.CurrentAmmo = value;
    //     }
    // }
}
