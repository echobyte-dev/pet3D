using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.States;
using CodeBase.StaticData;
using CodeBase.UI.Services;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
using Unity.VisualScripting;
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

      Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
      Container.Bind<IWindowService>().To<WindowService>().AsSingle();
      
      Container.Bind<IRandomService>().To<RandomService>().AsSingle();
      
      Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
      Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
    }
  }
}