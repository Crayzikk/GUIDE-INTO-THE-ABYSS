using UnityEngine;
using Zenject;

public class DialogTriggerInstaller : MonoInstaller
{
    [SerializeField] DialogManager dialogManager;

    public override void InstallBindings()
    {
        Container.Bind<DialogManager>().FromInstance(dialogManager).AsSingle();
    }
}