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
    private AudioSource audioSourceWeapon;

    // Clips
    [SerializeField] private AudioClip reloadClip;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip noAmmoClip;

    void Start()
    {
        animatorWeapon = GetComponent<Animator>();
        audioSourceWeapon = GetComponent<AudioSource>();

        cameraPlayer = Camera.main;
        screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !Player.isRunning)
        {
            if(audioSourceWeapon.clip != shootClip)
                audioSourceWeapon.clip = shootClip;
            
            Shoot();    
        }

        if(Input.GetKeyDown(KeyCode.R) && !Player.isRunning)
        {
            if(audioSourceWeapon.clip != reloadClip)
                audioSourceWeapon.clip = reloadClip;
            
            Reload();
        }

        animatorWeapon.SetBool("IsRunning", Player.isRunning);
    }

    protected virtual void Shoot()
    {
        if(currentAmmoInMagazine > 0 && Time.time >= nextTimeToFire)
        {
            audioSourceWeapon.Play();
            nextTimeToFire = Time.time + fireRate;

            animatorWeapon.Play("Attack");
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
            if(audioSourceWeapon.clip != noAmmoClip)
                audioSourceWeapon.clip = noAmmoClip;
            

            audioSourceWeapon.Play();
        }
    }

    protected virtual void Reload()
    {
        if (currentAmmoInMagazine == ammoInMagazine || totalAmmo == 0) return;
        
        audioSourceWeapon.Play();
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
