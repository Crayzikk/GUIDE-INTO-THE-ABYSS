using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject shortGun;
    [SerializeField] private GameObject revolver;
    
    void Start()
    {
        ChangeWeapon(false, true);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && shortGun.activeSelf)
        {
            ChangeWeapon(false, true);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && revolver.activeSelf)
        {
            ChangeWeapon(true, false);
        }
    }
    
    private void ChangeWeapon(bool stateShortGun, bool stateRevolver)
    {
        shortGun.SetActive(stateShortGun);
        revolver.SetActive(stateRevolver);
    }

    public Weapon GetActiveWeapon()
    {
        if(shortGun.activeSelf)
            return shortGun.GetComponent<Weapon>();
        else
            return revolver.GetComponent<Weapon>();
    }
}
