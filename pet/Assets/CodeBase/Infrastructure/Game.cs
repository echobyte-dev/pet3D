using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.States;
using CodeBase.UI;

namespace CodeBase.Infrastructure
{
  public class Game
  {
    public GameStateMachine StateMachine;
    
    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, IGameFactory gameFactory)
    {
      StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, gameFactory);
    }
  }
}