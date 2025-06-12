using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "MapObjectsLibInstaller", menuName = "Installers/MapObjectsLibInstaller")]
public class MapObjectsLibInstaller : ScriptableObjectInstaller<MapObjectsLibInstaller>
{
    [SerializeField]
    private MapObjectsLib _mapObjectsLib;

    public override void InstallBindings()
    {
        Container.BindInstance(_mapObjectsLib).AsSingle();
    }
}