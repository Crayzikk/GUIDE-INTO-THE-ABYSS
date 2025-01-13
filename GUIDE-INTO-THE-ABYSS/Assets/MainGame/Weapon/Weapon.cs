using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // Weapon state
    protected abstract int damageWeapon { get; set; }
    protected abstract float shootingRange { get; set; }
    public abstract int totalAmmo { get; set; }
    public abstract int currentAmmoInMagazine { get; set; }
    protected abstract int ammoInMagazine { get; set; }

    // Time 
    protected abstract float fireRate { get; set; }
    protected float nextTimeToFire { get; set; } = 0f;

    // Camera
    private Camera cameraPlayer;
    private Vector3 screenCenter;

    // RaycastHit
    [SerializeField] private LayerMask layerMask;
    private RaycastHit hit;
    
    // Particle System
    [SerializeField] private ParticleSystem muzzleFlash;

    // Components
    private Animator animatorWeapon;

    void Start()
    {
        animatorWeapon = GetComponent<Animator>();

        cameraPlayer = Camera.main;
        screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !Player.isRunning)
        {
            Shoot();    
        }

        if(Input.GetKeyDown(KeyCode.R) && !Player.isRunning)
        {
            Reload();
        }

        animatorWeapon.SetBool("IsRunning", Player.isRunning);
    }

    protected virtual void Shoot()
    {
        if(currentAmmoInMagazine > 0 && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;

            muzzleFlash.Play();
            currentAmmoInMagazine--;

            Ray ray = cameraPlayer.ScreenPointToRay(screenCenter);

            if(Physics.Raycast(ray, out hit, shootingRange, layerMask))
            {
                Debug.Log(hit.collider.name);
                hit.collider.GetComponent<Enemy>()?.TakeDamage(damageWeapon);
            }            
        }
        else
        {
            Debug.Log("Nema patroniv");
        }
    }

    protected virtual void Reload()
    {
        if (currentAmmoInMagazine == ammoInMagazine || totalAmmo == 0) return;

        animatorWeapon.Play("Reload");

        int ammoNeeded = ammoInMagazine - currentAmmoInMagazine;

        if (totalAmmo >= ammoNeeded)
        {
            currentAmmoInMagazine += ammoNeeded; 
            totalAmmo -= ammoNeeded;           
        }
        else
        {
            currentAmmoInMagazine += totalAmmo;
            totalAmmo = 0;
        }
    }

    public void GetCurrentAmmoInfo(out int totalAmmoInfo, out int currentAmmoInfo)
    {
        
        totalAmmoInfo = totalAmmo;
        currentAmmoInfo = currentAmmoInMagazine;
    }
}
