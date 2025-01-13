using UnityEngine;
using TMPro;
using Zenject;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;

    private WeaponController weaponController;

    [Inject]
    public void Initialize(WeaponController _weaponController)
    {
        weaponController = _weaponController;
    }

    void Update()
    {
        Weapon weapon = weaponController.GetActiveWeapon();

        ammoText.text = $"{weapon.currentAmmoInMagazine}/{weapon.totalAmmo}";
    }
}
