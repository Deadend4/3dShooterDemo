using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.Events;
using TMPro;

public class GunAnimations : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] public int inventoryAmmo;

    [Header("Addition Gameobjects")]
    [SerializeField] GameObject CameraRoot;

    [Header("Animators")]
    public Animator pistolAnimator;
    [SerializeField] Animator handAnimator;

    [Header("Scripts")]
    [SerializeField] Weapon weapon;
    [SerializeField] GameObject skt1;
    [SerializeField] GunSript gunSript;

    [Header("Audio")]
    [SerializeField] AudioSource pistolAudioSource;
    [SerializeField] AudioClip ShootSound;

    FirstPersonController fpsController;
    StarterAssetsInputs starterAssetsInputs;
    [HideInInspector] SimpleCameraShakeInCinemachine cameraShake;

    private bool isRunning = false;
    private bool canShoot = true;
    private bool canReload = true;
    public static UnityEvent EventShoot = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        gunSript.textCurrentAmmo.text = gunSript.currentAmmo.ToString();
        cameraShake = GetComponent<SimpleCameraShakeInCinemachine>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        fpsController = GetComponent<FirstPersonController>();
        GunSript.EventReload.AddListener(Reload);
        GunSript.EventBulletOut.AddListener(RateOfFire);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gunSript.currentAmmo > 0)
        {
            if (canShoot && canReload)
                EventShoot?.Invoke();
        }

        if (gunSript.currentAmmo < gunSript.maxAmmo && Input.GetKeyDown(KeyCode.R) && canReload)
        {
            Reload();
        }

        if (isRunning && !fpsController.Grounded)
        {
            ForcedStopSprint();
        }

        if (starterAssetsInputs.sprint && !isRunning)
        {
            isRunning = true;
            handAnimator.SetBool("isRunning", isRunning);
        }
        else if (!starterAssetsInputs.sprint && isRunning)
        {
            isRunning = false;
            handAnimator.SetBool("isRunning", isRunning);
        }

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
    }
    public void RateOfFire()
    {
        if (canShoot)
            StartCoroutine(WaitShoot());
    }
    IEnumerator WaitShoot()
    {
        cameraShake.isFiring = true;
        pistolAnimator.SetTrigger("shooting");
        handAnimator.SetTrigger("shooting");
        gunSript.Muzzle.Play();
        gunSript.BulletOut.Play();
        gunSript.Smoke.Play();
        pistolAudioSource.PlayOneShot(ShootSound);
        if (isRunning)
        {
            ForcedStopSprint();
        }
        canShoot = false;
        yield return new WaitForSeconds(60 / gunSript.fireRate);
        canShoot = true;
    }

    public void Reload()
    {
        StartCoroutine(DoReload());
    }

    IEnumerator DoReload()
    {
        handAnimator.SetTrigger("reload");
        pistolAnimator.SetTrigger("reload");
        canReload = false;
        yield return new WaitForSeconds(1.2f);
        inventoryAmmo -= gunSript.maxAmmo - gunSript.currentAmmo;
        gunSript.textInventoryAmmo.text = inventoryAmmo.ToString();
        gunSript.currentAmmo = gunSript.maxAmmo;
        gunSript.textCurrentAmmo.text = gunSript.currentAmmo.ToString();
        canReload = true;
    }
    private void ForcedStopSprint()
    {
        starterAssetsInputs.sprint = false;
        isRunning = false;
        handAnimator.SetBool("isRunning", isRunning);
    }
}
