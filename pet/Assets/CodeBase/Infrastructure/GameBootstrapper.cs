using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.States;
using CodeBase.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    [SerializeField] private LoadingCurtain _curtain;
    private Game _game;
    private IGameFactory _gameFactory;
    private IPersistentProgressService _progressService;
    private ISaveLoadService _saveLoadService;

    [Inject]
    public void Construct(IGameFactory gameFactory, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
    {
      _gameFactory = gameFactory;
      _progressService = progressService;
      _saveLoadService = saveLoadService;
    }
    
    private void Awake()
    {
      _game = new Game(this, Instantiate(_curtain), _gameFactory, _progressService, _saveLoadService);
      _game.StateMachine.Enter<BootstrapState>();

      DontDestroyOnLoad(this);
    }
  }
}