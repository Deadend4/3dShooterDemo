using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.Events;
using UnityEditor.Animations;
using TMPro;

public class WeaponShoot : MonoBehaviour
{
    [Header("Animators")]
    //public Animator pistolAnimator;
    // public Animator rifleAnimator;
    [SerializeField] Animator handAnimator;
    [SerializeField] AnimatorController pistolController;
    [SerializeField] AnimatorController rifleController;

    [Header("Weapons")]
    [SerializeField] GameObject pistol;
    [SerializeField] GameObject rifle;
    private bool isRifle = false;

    [Header("Scripts")]
    [SerializeField] Weapon weapon;
    [SerializeField] AmmoCalculation ammoCalculation;
    [SerializeField] UniversalGunScript universalGunScript;
    private WeaponData weaponData;

    [Header("UI")]
    [SerializeField] TMP_Text textCurrentWeapon;

    FirstPersonController fpsController;
    StarterAssetsInputs starterAssetsInputs;

    private Coroutine c_Reload;

    private bool isRunning = false;
    private bool canShoot = true;
    private bool canReload = true;

    public UnityEvent EventShoot = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        fpsController = GetComponent<FirstPersonController>();
        weapon.EventBulletOut.AddListener(RateOfFire);
        weapon.EventReload.AddListener(Reload);
        // Weapon.EventBulletOut.AddListener(RateOfFire);
        weaponData = weapon.WeaponData;
        textCurrentWeapon.text = weaponData.WeaponName;
        DisableComponents(rifle);
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponData.CurrentAmmo > 0 && (weaponData.IsAuto && Input.GetMouseButton(0)
        || !weaponData.IsAuto && Input.GetMouseButtonDown(0)))
        {
            Debug.Log("SH11T");
            if (canShoot && canReload)
                EventShoot?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.R) && canReload && (weaponData.CurrentAmmo < weaponData.MaxAmmo) && weaponData.InventoryAmmo > 0)
        {
            Reload();
        }

        SprintController();

        if (Input.GetKeyDown(KeyCode.Y))
        {
            handAnimator.SetTrigger("looking");
        }

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            handAnimator.SetBool("isWalking", true);
        }
        else
        {
            handAnimator.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            pistol.SetActive(false);
            rifle.SetActive(true);
            SwitchWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            pistol.SetActive(true);
            rifle.SetActive(false);
            SwitchWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Z) || Input.mouseScrollDelta.y != 0)
        {
            pistol.SetActive(!pistol.activeSelf);
            rifle.SetActive(!rifle.activeSelf);
            SwitchWeapon();
        }

    }

    private void DisableComponents(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out Animator newAnimator))
        {
            newAnimator.enabled = false;
        }
        gameObject.GetComponent<AudioSource>().enabled = false;
        gameObject.GetComponent<AmmoCalculation>().enabled = false;
        gameObject.GetComponent<Weapon>().enabled = false;
        gameObject.GetComponent<UniversalGunScript>().enabled = false;
    }
    private void EnableComponents(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out Animator newAnimator))
        {
            newAnimator.enabled = true;
        }
        gameObject.GetComponent<AudioSource>().enabled = true;
        gameObject.GetComponent<AmmoCalculation>().enabled = true;
        gameObject.GetComponent<Weapon>().enabled = true;
        gameObject.GetComponent<UniversalGunScript>().enabled = true;
    }

    public void SprintController()
    {
        if (isRunning && !fpsController.Grounded)
        {
            ForcedStopSprint();
        }

        if (!isRunning && starterAssetsInputs.sprint)
        {
            isRunning = true;
            handAnimator.SetBool("isRunning", isRunning);
        }
        else if (isRunning && !starterAssetsInputs.sprint)
        {
            isRunning = false;
            handAnimator.SetBool("isRunning", isRunning);
        }
    }

    public void RateOfFire()
    {
        if (canShoot)
            StartCoroutine(WaitShoot());
    }

    IEnumerator WaitShoot()
    {
        if (isRifle)
        {
            //rifleAnimator.SetTrigger("shooting");
        }
        else
        {
            //pistolAnimator.SetTrigger("shooting");
        }

        handAnimator.SetTrigger("shooting");
        universalGunScript.WeaponVFX();
        if (isRunning)
        {
            ForcedStopSprint();
        }
        canShoot = false;
        yield return new WaitForSeconds(60 / weaponData.FireRate);
        canShoot = true;
    }

    public void Reload()
    {
        c_Reload = StartCoroutine("DoReload");
    }

    IEnumerator DoReload()
    {
        handAnimator.SetTrigger("reload");
        if (isRifle)
        {
            // rifleAnimator.SetTrigger("reload");
            if (weaponData.CurrentAmmo > 0)
            {
                handAnimator.SetBool("isEmpty", false);
            }
            else
            {
                handAnimator.SetBool("isEmpty", true);
            }
        }
        else
        {
            // pistolAnimator.SetTrigger("reload");
        }

        canReload = false;
        if (isRifle && weaponData.CurrentAmmo == 0)
        {
            yield return new WaitForSeconds(weaponData.Reload + 2f);
        }
        else
        {
            yield return new WaitForSeconds(weaponData.Reload);
        }

        ammoCalculation.AmmoReload();
        canReload = true;
    }

    private void ForcedStopSprint()
    {
        starterAssetsInputs.sprint = false;
        isRunning = false;
        handAnimator.SetBool("isRunning", isRunning);
    }
    private void SwitchWeapon()
    {

        isRifle = rifle.activeSelf;
        //StopAllCoroutines();
        Debug.Log(c_Reload);
        if (c_Reload != null)
        {
            StopCoroutine(c_Reload);
            c_Reload = null;
            canReload = true;
        }

        weapon.StopAllCoroutines();
        // weapon.CancelInvoke();
        weapon.ResetListeners();
        handAnimator.SetBool("isRifle", isRifle);
        //handAnimator.SetTrigger("initialization");
        if (isRifle)
        {
            EnableComponents(rifle);
            DisableComponents(pistol);
            handAnimator.runtimeAnimatorController = rifleController;
            weapon = rifle.GetComponent<Weapon>();
            weaponData = weapon.WeaponData;
            universalGunScript = rifle.GetComponent<UniversalGunScript>();
            ammoCalculation = rifle.GetComponent<AmmoCalculation>();
            EventShoot.RemoveAllListeners();
        }
        else
        {
            EnableComponents(pistol);
            DisableComponents(rifle);
            handAnimator.runtimeAnimatorController = pistolController;
            weapon = pistol.GetComponent<Weapon>();
            weaponData = weapon.WeaponData;
            universalGunScript = pistol.GetComponent<UniversalGunScript>();
            ammoCalculation = pistol.GetComponent<AmmoCalculation>();
        }
        weapon.SetListeners(this);
        weapon.EventBulletOut.RemoveAllListeners();
        weapon.EventReload.RemoveAllListeners();
        weapon.EventBulletOut.AddListener(RateOfFire);
        weapon.EventReload.AddListener(Reload);
        ammoCalculation.SetNewWeaponData(weaponData);
        ammoCalculation.RefreshCurrentAmmoUI();
        ammoCalculation.RefreshInventoryUI();
        textCurrentWeapon.text = weaponData.WeaponName;

    }
    public WeaponData WeaponData
    {
        get
        {
            return weaponData;
        }
    }
}
