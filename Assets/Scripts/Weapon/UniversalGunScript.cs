using StarterAssets;
using UnityEngine;

public class UniversalGunScript : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject cameraHolder;

    [Header("Decal")]
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask decalLayer;

    [Header("VFX")]
    [SerializeField] ParticleSystem MuzzleFlash;
    [SerializeField] ParticleSystem Smoke;
    [SerializeField] ParticleSystem BulletCasting;

    [Header("Scripts")]
    [SerializeField] FirstPersonController firstPersonController;


    private void Start()
    {

    }
    public void WeaponVFX()
    {
        MuzzleFlash.Play();
        Smoke.Play();
        BulletCasting.Play();
    }
    public Camera GetCamera
    {
        get
        {
            return mainCamera;
        }
    }

    public float MaxDistance
    {
        get
        {
            return maxDistance;
        }
    }
    public LayerMask DecalLayer
    {
        get
        {
            return decalLayer;
        }
    }
    public void Recoil(float force)
    {
        firstPersonController.ResetCameraRotation(new Vector3(-force, 0, 0), -force);
    }
    public void Spread(float force)
    {
        Vector3 spreadDirection = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        mainCamera.transform.Rotate(spreadDirection, force);
    }
}
