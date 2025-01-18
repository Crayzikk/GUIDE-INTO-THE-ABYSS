public class Revoler : Weapon
{
    protected override int damageWeapon { get; set; } = 20;
    protected override float shootingRange { get; set; } = 40f;
    public override int totalAmmo { get; set; } = 36;
    public override int currentAmmoInMagazine { get; set; } = 6;
    protected override int ammoInMagazine { get; set; } = 6;
    protected override float fireRate { get; set; } = 0.8f;
}
