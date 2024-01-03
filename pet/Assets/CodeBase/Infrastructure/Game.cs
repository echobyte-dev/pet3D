using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.States;
using CodeBase.UI;

namespace CodeBase.Infrastructure
{
  public class Game
  {
    public GameStateMachine StateMachine;
    
    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, IGameFactory gameFactory, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
    {
      StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, gameFactory, progressService, saveLoadService);    }
  }
}