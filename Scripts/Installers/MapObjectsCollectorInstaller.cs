using UnityEngine;
using Zenject;

public class MapObjectsCollectorInstaller : MonoInstaller
{
    [SerializeField]
    private MapObjectsCollector _mapObjectsCollector;

    public override void InstallBindings()
    {
        Container.Bind<MapObjectsCollector>().FromInstance(_mapObjectsCollector);
    }
}