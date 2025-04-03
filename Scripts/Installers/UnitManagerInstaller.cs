using UnityEngine;
using Zenject;

public class UnitManagerInstaller: MonoInstaller
{
    [SerializeField] private UnitManager _unitManager;

    public override void InstallBindings()
    {
        Container.Bind<UnitManager>().FromInstance(_unitManager);
    }
}
