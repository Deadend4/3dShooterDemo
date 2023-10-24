using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponData", menuName = "Weapon Data", order = 51)]
public class WeaponData : ScriptableObject
{
    [Header("Description")]
    [SerializeField] private string weaponName;
    [SerializeField] private string description;
    [SerializeField] public Sprite icon;

    [Header("Parameters")]
    [SerializeField] private bool isAuto;
    [Range(30, 1000)]
    [SerializeField] private float fireRate;
    [Range(0, 5)]
    [SerializeField] private float spread;
    [Range(0, 10)]
    [SerializeField] private float recoil;
    [Range(0.5f, 3)]
    [SerializeField] private float reload;
    [Range(30, 500)]
    [SerializeField] private float damage;
    [Range(50, 500)]
    [SerializeField] private float maxDistance;
    [SerializeField] private int inventoryAmmo;
    [SerializeField] private int maxAmmo;
    [SerializeField] private int currentAmmo;
    [SerializeField] private string caliber;

    [Header("Audio")]
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip reloadSound1;
    [SerializeField] AudioClip reloadSound2;
    [SerializeField] AudioClip reloadSound3;
    [SerializeField] AudioClip triggerSound;


    public string WeaponName
    {
        get
        {
            return weaponName;
        }
    }
    public string Description
    {
        get
        {
            return description;
        }
    }
    public Sprite Icon
    {
        get
        {
            return icon;
        }
    }
    public bool IsAuto
    {
        get
        {
            return isAuto;
        }
    }
    public float FireRate
    {
        get
        {
            return fireRate;
        }
    }
    public float Spread
    {
        get
        {
            return spread;
        }
    }
    public float Recoil
    {
        get
        {
            return recoil;
        }
    }
    public float Reload
    {
        get
        {
            return reload;
        }
    }
    public float Damage
    {
        get
        {
            return damage;
        }
    }
    public float MaxDistance
    {
        get
        {
            return maxDistance;
        }
    }
    public int InventoryAmmo
    {
        get
        {
            return inventoryAmmo;
        }
        set
        {
            inventoryAmmo = value;
        }
    }
    public int MaxAmmo
    {
        get
        {
            return maxAmmo;
        }
    }
    public int CurrentAmmo
    {
        get
        {
            return currentAmmo;
        }
        set
        {
            currentAmmo = value;
        }
    }
    public string Caliber
    {
        get
        {
            return caliber;
        }
    }
    public AudioClip ShootSound
    {
        get
        {
            return shootSound;
        }
    }
    public AudioClip ReloadSound1
    {
        get
        {
            return reloadSound1;
        }
    }
    public AudioClip ReloadSound2
    {
        get
        {
            return reloadSound2;
        }
    }
    public AudioClip ReloadSound3
    {
        get
        {
            return reloadSound3;
        }
    }
    public AudioClip TriggerSound
    {
        get
        {
            return triggerSound;
        }
    }
}
