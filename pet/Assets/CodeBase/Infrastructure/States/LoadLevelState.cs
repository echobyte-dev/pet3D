using CodeBase.Infrastructure.Factories;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class LoadLevelState : IPlayloadState<string>
  {
    private const string InitialPointTag = "InitialPoint";

    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;

    public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
      IGameFactory gameFactory)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _curtain = curtain;
      _gameFactory = gameFactory;
    }

    public void Enter(string sceneName)
    {
      _curtain.Show();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() => 
      _curtain.Hide();

    private void OnLoaded()
    {
      GameObject player = _gameFactory.CreateSanta(GameObject.FindWithTag(InitialPointTag));

      CameraFollow(player);

      _stateMachine.Enter<GameLoopState>();
    }

    private static void CameraFollow(GameObject player)
    {
      Camera.main
        .GetComponent<CameraFollow>()
        .Follow(player);
    }
  }
}