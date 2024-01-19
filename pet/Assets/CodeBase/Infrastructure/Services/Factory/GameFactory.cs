using System.Collections.Generic;
using System.Threading.Tasks;
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
    private readonly IAssetProvider _asset;
    private readonly IStaticDataService _staticData;
    private readonly IPersistentProgressService _progressService;
    private readonly IRandomService _randomService;
    private readonly IWindowService _windowService;

    private GameObject _santaGameObject;

    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

    public GameFactory(IAssetProvider asset, IStaticDataService staticData, IRandomService randomService,
      IPersistentProgressService progressService, IWindowService windowService)
    {
      _asset = asset;
      _staticData = staticData;
      _randomService = randomService;
      _progressService = progressService;
      _windowService = windowService;
    }

    public async Task WarmUp()
    {
      await _asset.Load<GameObject>(AssetAddress.Loot);
      await _asset.Load<GameObject>(AssetAddress.Spawner);
    }

    public async Task<GameObject> CreateSanta(Vector3 at) =>
      _santaGameObject = await InstantiateRegisteredAsync(AssetAddress.SantaPath, at);

    public async Task<GameObject> CreateHud()
    {
      GameObject hud = await InstantiateRegisteredAsync(AssetAddress.HudPath);
      
      hud.GetComponentInChildren<LootCounter>()
        .Construct(_progressService.Progress.WorldData);

      foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
        openWindowButton.Construct(_windowService);

      return hud;
    }

    public async Task<GameObject> CreateMonster(MonsterTypeId typeId, Transform parent)
    {
      MonsterStaticData monsterData = _staticData.ForMonster(typeId);

      GameObject prefab = await _asset.Load<GameObject>(monsterData.PrefabReference);

      GameObject monster = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);

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

    public async Task<LootPiece> CreateLoot()
    {
      GameObject prefab = await _asset.Load<GameObject>(AssetAddress.Loot);
      LootPiece lootPiece = InstantiateRegistered(prefab)
        .GetComponent<LootPiece>();

      lootPiece.Construct(_progressService.Progress.WorldData);

      return lootPiece;
    }

    public async Task CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId)
    {
      GameObject prefab = await _asset.Load<GameObject>(AssetAddress.Spawner);
      SpawnPoint spawner = InstantiateRegistered(prefab, at)
        .GetComponent<SpawnPoint>();

      spawner.Construct(this);
      spawner.Id = spawnerId;
      spawner.MonsterTypeId = monsterTypeId;
    }
    
    private void Register(ISavedProgressReader progressReader)
    {
      if (progressReader is ISavedProgress progressWriter)
        ProgressWriters.Add(progressWriter);

      ProgressReaders.Add(progressReader);
    }

    public void Cleanup()
    {
      ProgressReaders.Clear();
      ProgressWriters.Clear();
      
      _asset.Cleanup();
    }
    
    private GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
    {
      GameObject gameObject = Object.Instantiate(prefab, at, Quaternion.identity);
      RegisterProgressWatchers(gameObject);

      return gameObject;
    }
    
    private GameObject InstantiateRegistered(GameObject prefab)
    {
      GameObject gameObject = Object.Instantiate(prefab);
      RegisterProgressWatchers(gameObject);

      return gameObject;
    }

    private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath, Vector3 at)
    {
      GameObject gameObject = await _asset.Instantiate(path: prefabPath, at: at);
      RegisterProgressWatchers(gameObject);

      return gameObject;
    }

    private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath)
    {
      GameObject gameObject = await _asset.Instantiate(path: prefabPath);
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