using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public interface IGameFactory
  {
    GameObject CreateSanta(GameObject at);
    GameObject CreateHud();
    GameObject CreateMonster(MonsterTypeId typeId, Transform parent);
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    void Cleanup();
    void Register(ISavedProgressReader savedProgress);
  }
}