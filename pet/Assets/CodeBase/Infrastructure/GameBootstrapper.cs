using CodeBase.Infrastructure.Factories;
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

    [Inject]
    public void Construct(IGameFactory gameFactory)
    {
      _gameFactory = gameFactory;
    }
    
    private void Awake()
    {
      _game = new Game(this, _curtain, _gameFactory);
      _game.StateMachine.Enter<BootstrapState>();

      DontDestroyOnLoad(this);
    }
  }
}