using UnityEngine;
using Zenject;

public class PlayerPanelInstaller : MonoInstaller
{
    [SerializeField]
    private PlayerPanel _panel;

    public override void InstallBindings()
    {
        Container.Bind<PlayerPanel>().FromInstance(_panel);
    }
}