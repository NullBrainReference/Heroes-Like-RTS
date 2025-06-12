using UnityEngine;
using Zenject;

public class MapControllerInstaller : MonoInstaller
{
    [SerializeField]
    private MapController _mapController;

    public override void InstallBindings()
    {
        Container.BindInstance(_mapController).AsSingle();
    }
}