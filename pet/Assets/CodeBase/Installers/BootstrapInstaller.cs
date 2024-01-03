using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factories;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
    }
}