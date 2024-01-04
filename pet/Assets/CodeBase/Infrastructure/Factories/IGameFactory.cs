using System;
using System.Collections.Generic;
using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
  public interface IGameFactory
  {
    GameObject CreateSanta(GameObject at);
    GameObject SantaGameObject { get; set; }
    event Action SantaCreated;
    
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    
    void Cleanup();
  }
}