using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
      Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().AsSingle();
      
      Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
      Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
    }
  }
}