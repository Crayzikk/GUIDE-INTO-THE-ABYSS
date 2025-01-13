using UnityEngine;
using Zenject;

public class WeaponControllerInstaller : MonoInstaller
{
    [SerializeField] private WeaponController weaponController;

    public override void InstallBindings()
    {
        Container.Bind<WeaponController>().FromInstance(weaponController).AsSingle();
    }
}