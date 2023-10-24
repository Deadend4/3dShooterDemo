using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AmmoCalculation))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Impacts impacts;
    [SerializeField] private WeaponShoot weaponShoot;
    private UniversalGunScript universalGunScript;

    private AudioSource audioSource;
    private AmmoCalculation ammoCalculation;

    public UnityEvent EventBulletOut = new UnityEvent(); //static
    public UnityEvent EventReload = new UnityEvent();

    public WeaponData WeaponData
    {
        get
        {
            return weaponData;
        }
    }

    private void Start()
    {
        universalGunScript = GetComponent<UniversalGunScript>();
        ammoCalculation = GetComponent<AmmoCalculation>();
        audioSource = GetComponent<AudioSource>();
        //When a shot event occurs, the added methods will be called
        if (!weaponData.name.Equals("2_AK47"))
            SetListeners(weaponShoot);
    }
    //The OneShot() method reproduces all the effects accompanying the shot
    public void OneShot()
    {
        if (weaponData.CurrentAmmo > 0)
        {
            Debug.Log("SHOT");
            ammoCalculation.RemoveBullet();
            ShootImpact();
            DecalSpawner();
            EventBulletOut.Invoke();
        }
        if (weaponData.CurrentAmmo == 0)
        {
            TriggerImpact();
            //EventReload.Invoke();
        }

    }
    //Spawn decals on the target
    public void DecalSpawner()
    {
        Vector3 Ray_start_pos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray crosshair = universalGunScript.GetCamera.ScreenPointToRay(Ray_start_pos);
        RaycastHit hitInfo;

        if (Physics.Raycast(crosshair, out hitInfo, universalGunScript.MaxDistance, universalGunScript.DecalLayer))
        {

            Quaternion impactRotation = Quaternion.LookRotation(transform.position - hitInfo.point, Vector3.up);
            TempSpawn(hitInfo, impactRotation);
        }
    }

    void TempSpawn(RaycastHit raycastHit, Quaternion rotation)
    {
        StartCoroutine(Spawning(raycastHit, rotation));
    }

    IEnumerator Spawning(RaycastHit raycastHit, Quaternion rotation)
    {
        GameObject spawnImpact;
        GameObject spawnDecal;
        if (raycastHit.transform.tag == "wood")
        {
            spawnImpact = Instantiate(impacts.impactWood, raycastHit.point, rotation);
            AudioSource.PlayClipAtPoint(impacts.soundWood, raycastHit.point, 0.65f);
            spawnDecal = Instantiate(impacts.decalWood, raycastHit.point, rotation);
            spawnDecal.transform.SetParent(raycastHit.transform);
        }
        else if (raycastHit.transform.tag == "metal")
        {
            spawnImpact = Instantiate(impacts.impactMetal, raycastHit.point, rotation);
            AudioSource.PlayClipAtPoint(impacts.soundMetal, raycastHit.point, 0.65f);
            spawnDecal = Instantiate(impacts.decalMetal, raycastHit.point, rotation);
            spawnDecal.transform.SetParent(raycastHit.transform);
        }
        else
        {
            spawnImpact = Instantiate(impacts.impactConcrete, raycastHit.point, rotation);
            AudioSource.PlayClipAtPoint(impacts.soundConcrete, raycastHit.point, 0.65f);
            spawnDecal = Instantiate(impacts.decalConcrete, raycastHit.point, rotation);
            spawnDecal.transform.SetParent(raycastHit.transform);
        }
        yield return new WaitForSeconds(1.2f);
        Destroy(spawnImpact);
    }

    public void ShootImpact()
    {
        audioSource.PlayOneShot(weaponData.ShootSound, 0.3f);
        universalGunScript.Spread(weaponData.Spread);
        universalGunScript.Recoil(weaponData.Recoil);
    }

    private void TriggerImpact()
    {
        audioSource.PlayOneShot(weaponData.TriggerSound, 0.2f);
    }

    public void ReloadImpact1()
    {
        audioSource.PlayOneShot(weaponData.ReloadSound1, 0.2f);
    }
    public void ReloadImpact2()
    {
        audioSource.PlayOneShot(weaponData.ReloadSound2, 0.2f);
    }
    public void ReloadImpact3()
    {
        audioSource.PlayOneShot(weaponData.ReloadSound3, 0.2f);
    }
    public void SetListeners(WeaponShoot newWeaponShoot)
    {
        newWeaponShoot.EventShoot.AddListener(OneShot);
        //newWeaponShoot.EventShoot.AddListener(DecalSpawner);
    }
    public void ResetListeners()
    {
        weaponShoot.EventShoot.RemoveAllListeners();
    }
}
