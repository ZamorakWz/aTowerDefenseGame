using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Non-Mono
        Container.Bind<TowerDataPanelManager>().AsSingle().NonLazy();
    }
}