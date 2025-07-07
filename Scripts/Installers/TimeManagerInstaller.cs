using UnityEngine;
using Zenject;

public class TimeManagerInstaller : MonoInstaller
{
    [SerializeField]
    private TimeManager _manager;

    public override void InstallBindings()
    {
        Container.Bind<TimeManager>().FromInstance(_manager);
    }
}