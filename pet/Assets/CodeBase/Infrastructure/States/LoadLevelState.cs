using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class LoadLevelState : IPlayloadState<string>
  {
    private const string InitialPointTag = "InitialPoint";
    private const string PlayerPath = "Santa/Santa";

    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;

    public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _curtain = curtain;
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
      GameObject initialPoint = GameObject.FindWithTag(InitialPointTag);
      GameObject player = Instantiate(PlayerPath, at: initialPoint.transform.position);

      CameraFollow(player);

      _stateMachine.Enter<GameLoopState>();
    }

    private static void CameraFollow(GameObject player)
    {
      Camera.main
        .GetComponent<CameraFollow>()
        .Follow(player);
    }

    private static GameObject Instantiate(string path)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab);
    }
    private static GameObject Instantiate(string path, Vector3 at)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab, at, Quaternion.identity);
    }
  }
}