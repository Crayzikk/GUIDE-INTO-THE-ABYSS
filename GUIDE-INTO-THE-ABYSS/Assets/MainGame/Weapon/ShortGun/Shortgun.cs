
public class Shortgun : Weapon
{
    protected override int damageWeapon { get; set; } = 50;
    protected override float shootingRange { get; set; } = 10f;
    public override int totalAmmo { get; set; } = 15;
    public override int currentAmmoInMagazine { get; set; } = 5;
    protected override int ammoInMagazine { get; set; } = 5;
    protected override float fireRate { get; set; } = 0.6f;
}
