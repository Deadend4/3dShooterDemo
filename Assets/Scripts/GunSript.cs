using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class GunSript : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] public float fireRate;
    [SerializeField] public float damage;
    [SerializeField] float maxDistance;
    [SerializeField] public int currentAmmo = 4;
    [SerializeField] public int maxAmmo = 8;

    [Header("Particles")]
    [SerializeField] public ParticleSystem Muzzle;
    [SerializeField] public ParticleSystem BulletOut;
    [SerializeField] public ParticleSystem Smoke;

    [Header("Game Objects")]
    [SerializeField] GameObject MuzzlePoint;
    [SerializeField] Camera camera;

    [Header("UI")]
    [SerializeField] public TextMeshProUGUI textCurrentAmmo;
    [SerializeField] public TextMeshProUGUI textInventoryAmmo;

    [Header("Layers")]
    [SerializeField] LayerMask enemyLayer;

    [Header("Scripts")]
    [SerializeField] Impacts impacts;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip reloadSound;

    public static UnityEvent EventReload = new UnityEvent();
    public static UnityEvent EventBulletOut = new UnityEvent();

    // Start is called before the first frame update
    private void Start()
    {
        textCurrentAmmo.text = currentAmmo.ToString();
        GunAnimations.EventShoot.AddListener(OneShot);
        GunAnimations.EventShoot.AddListener(ExplosionRB);
    }
    void Update()
    {
        // Debug.DrawLine(MuzzlePoint.transform.position, GetHitPoint(MuzzlePoint.transform.position), Color.green);

    }

    public void OneShot()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
            EventBulletOut.Invoke();
        }
        if (currentAmmo == 0)
            EventReload.Invoke();

        textCurrentAmmo.text = currentAmmo.ToString();
    }

    public void ExplosionRB()
    {
        Vector3 Ray_start_pos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray crosshair = camera.ScreenPointToRay(Ray_start_pos);
        RaycastHit hitInfo;

        if (Physics.Raycast(crosshair, out hitInfo, maxDistance, enemyLayer))
        {

            Quaternion impactRotation = Quaternion.LookRotation(transform.position - hitInfo.point, Vector3.up);
            TempSpawn(hitInfo, impactRotation);
            //(hitInfo.transform.tag, hitInfo.point, impactRotation);
            hitInfo.rigidbody?.AddForceAtPosition((hitInfo.point - transform.position).normalized, hitInfo.point, ForceMode.Impulse);
        }
    }

    void TempSpawn(RaycastHit raycastHit, Quaternion rotation)
    //(string hitTag, Vector3 point, Quaternion rotation)
    {
        StartCoroutine(Spawning(raycastHit, rotation));
        //StartCoroutine(Spawning(hitTag, point, rotation));
    }

    IEnumerator Spawning(RaycastHit raycastHit, Quaternion rotation)
    //string hitTag, Vector3 point, Quaternion rotation)
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
        else if (raycastHit.transform.tag == "metal")//(hitTag == "metal")
        {
            spawnImpact = Instantiate(impacts.impactMetal, raycastHit.point, rotation);
            AudioSource.PlayClipAtPoint(impacts.soundMetal, raycastHit.point, 0.65f);
            spawnDecal = Instantiate(impacts.decalMetal, raycastHit.point, rotation);
            spawnDecal.transform.SetParent(raycastHit.transform);
        }
        else //if (hitTag == "concrete")
        {
            spawnImpact = Instantiate(impacts.impactConcrete, raycastHit.point, rotation);
            AudioSource.PlayClipAtPoint(impacts.soundConcrete, raycastHit.point, 0.65f);
            spawnDecal = Instantiate(impacts.decalConcrete, raycastHit.point, rotation);
            spawnDecal.transform.SetParent(raycastHit.transform);
        }
        yield return new WaitForSeconds(1.2f);
        Destroy(spawnImpact);
    }

    Vector3 GetHitPoint(Vector3 muzzlePosition)
    {
        // Unless you've mucked with the projection matrix, firing through
        // the center of the screen is the same as firing from the camera itself.
        Vector3 Ray_start_pos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray crosshair = camera.ScreenPointToRay(Ray_start_pos);

        // Cast a ray forward from the camera to see what's 
        // under the crosshair from the player's point of view.
        Vector3 aimPoint;
        RaycastHit hit;
        if (Physics.Raycast(crosshair, out hit, maxDistance))
        {
            aimPoint = hit.point;
        }
        else
        {
            aimPoint = crosshair.origin + crosshair.direction * maxDistance;
        }

        // Now we know what to aim at, form a second ray from the tool.
        Ray beam = new Ray(muzzlePosition, aimPoint - muzzlePosition);

        // If we don't hit anything, just go straight to the aim point.
        if (!Physics.Raycast(beam, out hit, maxDistance))
            return aimPoint;
        // Otherwise, stop at whatever we hit on the way.
        return hit.point;
    }
    public void ReloadImpact()
    {
        audioSource.PlayOneShot(reloadSound, 0.2f);
    }
}
