using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic;
using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssetProvider _assets;
    private readonly IStaticDataService _staticData;
    
    private GameObject _santaGameObject;

    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

    public GameFactory(IAssetProvider assets, IStaticDataService staticData)
    {
      _assets = assets;
      _staticData = staticData;
    }

    public GameObject CreateSanta(GameObject at) => 
      _santaGameObject = InstantiateRegistered(AssetPath.SantaPath, at.transform.position);

    public GameObject CreateHud() =>
      InstantiateRegistered(AssetPath.HudPath);
    
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

      Attack attack = monster.GetComponent<Attack>();
      attack.Construct(_santaGameObject.transform);
      attack.Damage = monsterData.Damage;
      attack.Cleavage = monsterData.Cleavage;
      attack.EffectiveDistance = monsterData.EffectiveDistance;


      return monster;
    }

    public void Cleanup()
    {
      ProgressReaders.Clear();
      ProgressWriters.Clear();
    }

    public void Register(ISavedProgressReader progressReader)
    {
      if(progressReader is ISavedProgress progressWriter)
        ProgressWriters.Add(progressWriter);
      
      ProgressReaders.Add(progressReader);
    }

    private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
    {
      GameObject gameObject = _assets.Instantiate(path: prefabPath, at: at);
      RegisterProgressWatchers(gameObject);
      
      return gameObject;
    }

    private GameObject InstantiateRegistered(string prefabPath)
    {
      GameObject gameObject = _assets.Instantiate(path: prefabPath);
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