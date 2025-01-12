using UnityEngine;
using Zenject;

public class CheckGroundInstaller : MonoInstaller
{
    [SerializeField] private CheckGround checkGround;
    
    public override void InstallBindings()
    {
        Container.Bind<CheckGround>().FromInstance(checkGround).AsSingle();
    }
}