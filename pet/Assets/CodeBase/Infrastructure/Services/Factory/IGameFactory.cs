using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
  public interface IGameFactory
  {
    Task<GameObject> CreateSanta(Vector3 at);
    Task<GameObject> CreateHud();
    Task<GameObject> CreateMonster(MonsterTypeId typeId, Transform parent);
    Task<LootPiece> CreateLoot();
    
    Task CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId);
    void Cleanup();
    
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    Task WarmUp();
  }
}