using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.States;
using CodeBase.StaticData;
using CodeBase.UI;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Factory;

namespace CodeBase.Infrastructure
{
  public class Game
  {
    public GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, IGameFactory gameFactory,
      IPersistentProgressService progressService, ISaveLoadService saveLoadService,
      IStaticDataService staticDataService, IUIFactory uiFactory, IAssetProvider assetProvider)
    {
      StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, gameFactory, progressService,
        saveLoadService, staticDataService, uiFactory, assetProvider);
    }
  }
}