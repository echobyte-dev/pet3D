using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
  public interface IGameFactory
  {
    GameObject CreateSanta(GameObject at);
    GameObject CreateHud();
    GameObject CreateMonster(MonsterTypeId typeId, Transform parent);
    LootPiece CreateLoot();
    
    void CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId);
    void Cleanup();
    
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
  }
}