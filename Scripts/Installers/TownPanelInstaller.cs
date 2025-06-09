using UnityEngine;
using Zenject;

public class TownPanelInstaller : MonoInstaller
{
    [SerializeField] private TownPanel _panel;

    public override void InstallBindings()
    {
        Container.Bind<TownPanel>().FromInstance(_panel);
    }
}