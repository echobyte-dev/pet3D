using CodeBase.Data;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Santa;
using CodeBase.StaticData;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
  public class LoadLevelState : IPayloadState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _progressService;
    private readonly IStaticDataService _staticData;
    private readonly IUIFactory _uiFactory;

    public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
      IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticData, IUIFactory uiFactory)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _curtain = curtain;
      _gameFactory = gameFactory;
      _progressService = progressService;
      _staticData = staticData;
      _uiFactory = uiFactory;
    }

    public void Enter(string sceneName)
    {
      _curtain.Show();
      _gameFactory.Cleanup();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() =>
      _curtain.Hide();

    private void OnLoaded()
    {
      InitUIRoot();
      InitGameWorld();
      InformProgressReaders();

      _stateMachine.Enter<GameLoopState>();
    }

    private void InitGameWorld()
    {
      LevelStaticData levelData = LevelStaticData();

      InitSpawners(levelData);
      GameObject santa = InitSanta(levelData);
      InitHud(santa);
      CameraFollow(santa);
      _stateMachine.Enter<GameLoopState>();
    }

    private void InitSpawners(LevelStaticData levelData)
    {
      foreach (EnemySpawnerStaticData spawnerData in levelData.EnemySpawners) 
        _gameFactory.CreateSpawner(spawnerData.Position, spawnerData.Id, spawnerData.MonsterTypeId);
    }

    private void InitHud(GameObject santa) => 
      _gameFactory.CreateHud().GetComponentInChildren<ActorUI>().Construct(santa.GetComponent<SantaHealth>());

    private GameObject InitSanta(LevelStaticData levelData) => 
      _gameFactory.CreateSanta(levelData.InitialSantaPosition);

    private void InitUIRoot() => 
      _uiFactory.CreateUIRoot();

    private void InformProgressReaders()
    {
      foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
        progressReader.LoadProgress(_progressService.Progress);
    }

    private LevelStaticData LevelStaticData() => 
      _staticData.ForLevel(SceneManager.GetActiveScene().name);

    private void CameraFollow(GameObject hero) =>
      Camera.main.GetComponent<CameraFollow>().Follow(hero);
  }
}