using CodeBase.Data;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic
{
  public class EnemySpawner : MonoBehaviour, ISavedProgress
  {
    [SerializeField] private MonsterTypeId _monsterType;
    [SerializeField] private bool _slain;

    private string _id;

    private void Awake()
    {
      _id = GetComponent<UniqueId>().Id;
    }

    public void LoadProgress(PlayerProgress progress)
    {
      if (progress.KillData.ClearedSpawners.Contains(_id))
        _slain = true;
      else
        Spawn();
    }

    private void Spawn()
    {

    }

    public void UpdateProgress(PlayerProgress progress)
    {
      if(_slain)
        progress.KillData.ClearedSpawners.Add(_id);
    }
  }
}