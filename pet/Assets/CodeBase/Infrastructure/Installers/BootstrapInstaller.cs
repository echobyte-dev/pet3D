using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.StaticData;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
      Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().AsSingle();
      Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
      
      Container.Bind<IRandomService>().To<RandomService>().AsSingle();
      
      Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
      Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
    }
  }
}