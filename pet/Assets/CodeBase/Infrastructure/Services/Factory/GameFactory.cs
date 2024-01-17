using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Infrastructure.Services.Factory
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssetsProvider _assetsProvider;
    private readonly IStaticDataService _staticData;
    private readonly IPersistentProgressService _progressService;
    private readonly IRandomService _randomService;
    private readonly IWindowService _windowService;

    private GameObject _santaGameObject;

    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

    public GameFactory(IAssetsProvider assetsProvider, IStaticDataService staticData, IRandomService randomService,
      IPersistentProgressService progressService, IWindowService windowService)
    {
      _assetsProvider = assetsProvider;
      _staticData = staticData;
      _randomService = randomService;
      _progressService = progressService;
      _windowService = windowService;
    }

    public GameObject CreateSanta(GameObject at) =>
      _santaGameObject = InstantiateRegistered(AssetPath.Santa, at.transform.position);

    public GameObject CreateHud()
    {
      GameObject hud = InstantiateRegistered(AssetPath.Hud);

      hud.GetComponentInChildren<LootCounter>()
        .Construct(_progressService.Progress.WorldData);

      foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
        openWindowButton.Construct(_windowService);

      return hud;
    }

    public GameObject CreateMonster(MonsterTypeId typeId, Transform parent)
    {
      MonsterStaticData monsterData = _staticData.ForMonster(typeId);
      GameObject monster = Object.Instantiate(monsterData.Prefab, parent.position, Quaternion.identity, parent);

      IHealth health = monster.GetComponent<IHealth>();
      health.CurrentHealth = monsterData.Hp;
      health.MaxHealth = monsterData.Hp;

      monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;
      monster.GetComponent<AgentMoveToSanta>()?.Construct(_santaGameObject.transform);
      monster.GetComponent<RotateToSanta>()?.Construct(_santaGameObject.transform);

      LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
      lootSpawner.SetLootValue(monsterData.MinLoot, monsterData.MaxLoot);
      lootSpawner.Construct(this, _randomService);

      Attack attack = monster.GetComponent<Attack>();
      attack.Construct(_santaGameObject.transform);
      attack.Damage = monsterData.Damage;
      attack.Cleavage = monsterData.Cleavage;
      attack.EffectiveDistance = monsterData.EffectiveDistance;


      return monster;
    }

    public LootPiece CreateLoot()
    {
      LootPiece lootPiece = InstantiateRegistered(AssetPath.Loot)
        .GetComponent<LootPiece>();

      lootPiece.Construct(_progressService.Progress.WorldData);

      return lootPiece;
    }

    public void CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId)
    {
      SpawnPoint spawner = InstantiateRegistered(AssetPath.Spawner, at)
        .GetComponent<SpawnPoint>();

      spawner.Construct(this);
      spawner.Id = spawnerId;
      spawner.MonsterTypeId = monsterTypeId;
    }

    public void Cleanup()
    {
      ProgressReaders.Clear();
      ProgressWriters.Clear();
    }

    public void Register(ISavedProgressReader progressReader)
    {
      if (progressReader is ISavedProgress progressWriter)
        ProgressWriters.Add(progressWriter);

      ProgressReaders.Add(progressReader);
    }

    private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
    {
      GameObject gameObject = _assetsProvider.Instantiate(path: prefabPath, at: at);
      RegisterProgressWatchers(gameObject);

      return gameObject;
    }

    private GameObject InstantiateRegistered(string prefabPath)
    {
      GameObject gameObject = _assetsProvider.Instantiate(path: prefabPath);
      RegisterProgressWatchers(gameObject);

      return gameObject;
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
      foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
        Register(progressReader);
    }
  }
}