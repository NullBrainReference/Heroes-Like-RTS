using UnityEngine;
using Zenject;

public class PlayerControllerInstaller: MonoInstaller
{
    [SerializeField] private MapPlayerController _playerController;

    public override void InstallBindings()
    {
        Container.Bind<MapPlayerController>().FromInstance(_playerController);
    }
}
