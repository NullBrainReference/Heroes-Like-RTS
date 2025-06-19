using UnityEngine;
using Zenject;

public class TooltipsManagerInstaller : MonoInstaller
{
    [SerializeField] private TooltipsManager _manager; 

    public override void InstallBindings()
    {
        Container.Bind<TooltipsManager>().FromInstance(_manager);
    }
}